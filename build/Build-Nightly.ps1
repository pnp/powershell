$runPublish = $false

$dependencies = Invoke-RestMethod -Method Get -Uri https://raw.githubusercontent.com/pnp/powershell/dev/dependencies.json

$pnppowershell_hash = git ls-files -s ./src | git hash-object --stdin
#$existing_pnppowershell_hash = Get-Content ./pnppowershell_hash.txt -Raw -ErrorAction SilentlyContinue

#$existing_pnpframework_hash = Get-Content ./pnpframework_hash.txt -Raw -ErrorAction SilentlyContinue
$pnpframework_response = Invoke-RestMethod -Method Get -Uri "$($env:GITHUB_API_URL)/repos/pnp/pnpframework/branches/dev" -SkipHttpErrorCheck
if ($null -ne $pnpframework_response) {
	if ($null -ne $pnpframework_response.commit) {
		$pnpframework_hash = $pnpframework_response.commit.sha
	}
}

#$existing_pnpcoresdk_hash = Get-Content ./pnpcoresdk_hash.txt -Raw -ErrorAction SilentlyContinue
$pnpcoresdk_response = Invoke-RestMethod -Method Get -Uri "$($env:GITHUB_API_URL)/repos/pnp/pnpcore/branches/dev" -SkipHttpErrorCheck
if ($null -ne $pnpcoresdk_response) {
	if ($null -ne $pnpcoresdk_response.commit) {
		$pnpcoresdk_hash = $pnpcoresdk_response.commit.sha
	}
}

if ($dependencies.PnPPowershell -ne $pnppowershell_hash) {
	Write-Host "PnP Powershell is newer"
	$runPublish = $true
}

if ($runPublish -eq $false -and $dependencies.PnPFramework -ne $pnpframework_hash) {
	Write-Host "PnP Framework is newer"
	$runPublish = $true
}

if ($runPublish -eq $false -and $dependencies.PnPCore -ne $pnpcoresdk_hash) {
	Write-Host "PnP Core SDK is newer"
	$runPublish = $true
}

if ($runPublish -eq $true) {
	$dependencies.Updated = Get-Date -Format "yyyyMMdd-HHmmss"
	$dependencies.PnPCore = $pnpcoresdk_hash
	$dependencies.PnPFramework = $pnpframework_hash
	$dependencies.PnPPowershell = $pnppowershell_hash

	Set-Content ./dependencies.json -Value $(ConvertTo-Json $dependencies) -Force

	$versionFileContents = Get-Content "$PSScriptRoot/../version.json" -Raw | ConvertFrom-Json

	if ($versionFileContents.Version.Contains("%")) {
		$versionString = $versionFileContents.Version.Replace("%", "0");
		$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionString)
		$buildVersion = $versionObject.Patch;
	}
	else {	
		$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionFileContents.Version)
		$buildVersion = $versionObject.Patch + 1;
	}

	$version = "$($versionObject.Major).$($versionObject.Minor).$buildVersion"

	Write-Host "Building PnP.PowerShell $version on PowerShell $($PSVersionTable.PSVersion.ToString())" -ForegroundColor Yellow

	# Check if version has not been published yet

	$availableVersions = Find-Module -Name PnP.PowerShell -AllowPrerelease | Select-Object -First 1
	$availableVersion = $availableVersions.Version.Split('-')[0]

	if ($availableVersion -eq $version) {
		Write-Host "Build version is same as published version. Exiting."
		exit 1# Do not proceed.
	}

	dotnet build ./src/Commands/PnP.PowerShell.csproj --nologo --configuration Release --no-incremental -p:VersionPrefix=$version -p:VersionSuffix=nightly

	$documentsFolder = [environment]::getfolderpath("mydocuments");

	if ($IsLinux) {
		$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
		$helpfileDestinationFolder = "$documentsFolder/.local/share/powershell/Modules"
	}
	elseif ($IsMacOS) {
		$destinationFolder = "$HOME/.local/share/powershell/Modules/PnP.PowerShell"
		$helpfileDestinationFolder = "$HOME/.local/share/powershell/Modules"
	}
	else {
		$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
		$helpfileDestinationFolder = "$documentsFolder/PowerShell/Modules"
	}

	$corePath = "$destinationFolder/Core"
	$commonPath = "$destinationFolder/Common"
	$coreRuntimePathWin64 = "$destinationFolder/Core/runtimes/win-x64/native"
	$coreRuntimePathArm64 = "$destinationFolder/Core/runtimes/win-arm64/native"
	$coreRuntimePathx86 = "$destinationFolder/Core/runtimes/win-x86/native"
	$coreRuntimePathLinx64 = "$destinationFolder/Core/runtimes/linux-x64/native"

	$assemblyExceptions = @("System.Memory.dll");

	Try {
        # Module folder there?
        if (Test-Path $destinationFolder) {
            # Yes, empty it
            Remove-Item $destinationFolder\* -Recurse -Force -ErrorAction Stop
        }
        # No, create it
        Write-Host "Creating target folders: $destinationFolder" -ForegroundColor Yellow
        New-Item -Path $destinationFolder -ItemType Directory -Force | Out-Null
        New-Item -Path "$destinationFolder\Core" -ItemType Directory -Force | Out-Null
        New-Item -Path "$destinationFolder\Common" -ItemType Directory -Force | Out-Null

        Write-Host "Copying files to $destinationFolder" -ForegroundColor Yellow

        $commonFiles = [System.Collections.Generic.Hashset[string]]::new()
        Copy-Item -Path "$PSscriptRoot/../resources/*.ps1xml" -Destination "$destinationFolder"
        Get-ChildItem -Path "$PSScriptRoot/../src/ALC/bin/Release/net8.0" | Where-Object { $_.Extension -in '.dll', '.pdb' } | Foreach-Object { if (!$assemblyExceptions.Contains($_.Name)) { [void]$commonFiles.Add($_.Name) }; Copy-Item -LiteralPath $_.FullName -Destination $commonPath }
        Get-ChildItem -Path "$PSScriptRoot/../src/Commands/bin/Release/net8.0" | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $corePath }
        
        # Check if runtime folders exist in source before copying
        $sourceRuntimeBase = "$PSScriptRoot/../src/Commands/bin/Release/net8.0/runtimes"
        if (Test-Path $sourceRuntimeBase) {
            Write-Host "Runtime folders found in source, creating destination runtime structure" -ForegroundColor Yellow
            New-Item -Path "$destinationFolder\Core\runtimes" -ItemType Directory -Force | Out-Null
            
            # Copy win-x64 runtime if exists
            $sourceRuntimePathWin64 = "$sourceRuntimeBase/win-x64/native"
            if (Test-Path $sourceRuntimePathWin64) {
                New-Item -Path "$destinationFolder\Core\runtimes\win-x64\native" -ItemType Directory -Force | Out-Null
                Get-ChildItem -Path $sourceRuntimePathWin64 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $coreRuntimePathWin64 }
                Write-Host "Copied win-x64 runtime files" -ForegroundColor Green
            }
            
            # Copy win-arm64 runtime if exists
            $sourceRuntimePathArm64 = "$sourceRuntimeBase/win-arm64/native"
            if (Test-Path $sourceRuntimePathArm64) {
                New-Item -Path "$destinationFolder\Core\runtimes\win-arm64\native" -ItemType Directory -Force | Out-Null
                Get-ChildItem -Path $sourceRuntimePathArm64 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $coreRuntimePathArm64 }
                Write-Host "Copied win-arm64 runtime files" -ForegroundColor Green
            }
            
            # Copy win-x86 runtime if exists
            $sourceRuntimePathx86 = "$sourceRuntimeBase/win-x86/native"
            if (Test-Path $sourceRuntimePathx86) {
                New-Item -Path "$destinationFolder\Core\runtimes\win-x86\native" -ItemType Directory -Force | Out-Null
                Get-ChildItem -Path $sourceRuntimePathx86 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $coreRuntimePathx86 }
                Write-Host "Copied win-x86 runtime files" -ForegroundColor Green
            }

			# Copy linux-x64 runtime if exists
			$sourceRuntimePathLinx64 = "$sourceRuntimeBase/linux-x64/native"
			if (Test-Path $sourceRuntimePathLinx64) {
				New-Item -Path "$destinationFolder\Core\runtimes\linux-x64\native" -ItemType Directory -Force | Out-Null
				Get-ChildItem -Path $sourceRuntimePathLinx64 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb', '.so' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $coreRuntimePathLinx64 }
				Write-Host "Copied linux-x64 runtime files" -ForegroundColor Green
			}
        } else {
            Write-Host "No runtime folders found in build output - this is normal for projects without native dependencies" -ForegroundColor Yellow
        }
    }
	Catch {
		Write-Host "Error: Cannot copy files to $destinationFolder. Maybe a PowerShell session is still using the module?"
		exit 1
	}

	#Write-Host "Output tree" -ForegroundColor Yellow
	#Get-ChildItem $destinationFolder -Recurse

	Try {
		Write-Host "Generating PnP.PowerShell.psd1" -ForegroundColor Yellow
		# Load the Module in a new PowerShell session
		$scriptBlock = {
			$documentsFolder = [environment]::getfolderpath("mydocuments");

			if ($IsLinux) {
				$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
			}
			elseif ($IsMacOS) {
				$destinationFolder = "~/.local/share/powershell/Modules/PnP.PowerShell"
			}
			else {
				$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
			}
			Write-Host "Importing dotnet core version of assembly" -ForegroundColor Yellow
			Import-Module -Name "$destinationFolder/Core/PnP.PowerShell.dll" -DisableNameChecking

			Write-Host "Getting cmdlet info" -ForegroundColor Yellow
			$cmdlets = Get-Command -Module PnP.PowerShell | ForEach-Object { "`"$_`"" }
			$cmdlets -Join ","
		}

		Write-Host "Starting job to retrieve cmdlet names" -ForegroundColor Yellow
		$cmdletsString = Start-ThreadJob -ScriptBlock $scriptBlock | Receive-Job -Wait

		Write-Host "Writing PSD1" -ForegroundColor Yellow
		$manifest = "@{
	NestedModules =  'Core/PnP.PowerShell.dll'
	ModuleVersion = '$version'
	Description = 'Microsoft 365 Patterns and Practices PowerShell Cmdlets'
	GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
	Author = 'Microsoft 365 Patterns and Practices'
	CompanyName = 'Microsoft 365 Patterns and Practices'
	PowerShellVersion = '7.4.0'	
	ProcessorArchitecture = 'None'
	FunctionsToExport = '*'  
	CmdletsToExport = @($cmdletsString)
	VariablesToExport = '*'
	AliasesToExport = '*'
	FormatsToProcess = 'PnP.PowerShell.Format.ps1xml' 
	PrivateData = @{
		PSData = @{
			Tags = 'SharePoint','PnP','Teams','Planner'
			Prerelease = 'nightly'
			ProjectUri = 'https://aka.ms/sppnp'
			IconUri = 'https://raw.githubusercontent.com/pnp/media/40e7cd8952a9347ea44e5572bb0e49622a102a12/parker/ms/300w/parker-ms-300.png'
		}
	}
}"
		$manifest | Out-File "$destinationFolder/PnP.PowerShell.psd1" -Force
	}
	Catch {
		Write-Error $_.Exception.Message
		Write-Error "Error: Cannot generate PnP.PowerShell.psd1. Maybe a PowerShell session is still using the module?"
		exit 1
	}

	# Generate predictor commands
	./build/Generate-PredictorCommands.ps1 -Version "nightly"

	Write-Host "Generating Documentation" -ForegroundColor Yellow
	Set-PSRepository PSGallery -InstallationPolicy Trusted
	Install-Module -Name Microsoft.PowerShell.PlatyPS -AllowPrerelease -RequiredVersion 1.0.0-preview1
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path ./documentation/*.md
	$mdFiles | Import-MarkdownCommandHelp -Path {$_.FilePath} | Export-MamlCommandHelp -OutputFolder $helpfileDestinationFolder -Force
	# Install-Module Microsoft.PlatyPS -ErrorAction Stop
	# New-ExternalHelp -Path ./documentation -OutputPath $destinationFolder -Force

	$apiKey = $("$env:POWERSHELLGALLERY_API_KEY")

	Write-Host "Publishing Module version $version" -ForegroundColor Yellow
	Import-Module -Name PnP.PowerShell
	Publish-Module -Name PnP.PowerShell -AllowPrerelease -NuGetApiKey $apiKey

	# Write version back to version
	Set-Content ./version.txt -Value $version -Force -NoNewline

	# Write version back to version.json
	$json = @{Version = "$version"; Message = "" } | ConvertTo-Json
	Set-Content ./version.json -Value $json -Force -NoNewline
}
else {
	Write-Host "No changes in PnP PowerShell, PnP Framework or PnP Core SDK. Exiting." -ForegroundColor Green
}
