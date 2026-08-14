$runPublish = $false

$dependencies = Invoke-RestMethod -Method Get -Uri https://raw.githubusercontent.com/pnp/powershell/dev/dependencies.json

$pnppowershell_hash = git ls-files -s "$PSScriptRoot/../src" | git hash-object --stdin
#$existing_pnppowershell_hash = Get-Content "$PSScriptRoot/../pnppowershell_hash.txt" -Raw -ErrorAction SilentlyContinue

#$existing_pnpframework_hash = Get-Content "$PSScriptRoot/../pnpframework_hash.txt" -Raw -ErrorAction SilentlyContinue
$pnpframework_response = Invoke-RestMethod -Method Get -Uri "$($env:GITHUB_API_URL)/repos/pnp/pnpframework/branches/dev" -SkipHttpErrorCheck
if ($null -ne $pnpframework_response) {
	if ($null -ne $pnpframework_response.commit) {
		$pnpframework_hash = $pnpframework_response.commit.sha
	}
}

#$existing_pnpcoresdk_hash = Get-Content "$PSScriptRoot/../pnpcoresdk_hash.txt" -Raw -ErrorAction SilentlyContinue
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

	Set-Content "$PSScriptRoot/../dependencies.json" -Value $(ConvertTo-Json $dependencies) -Force

	$versionFileContents = Get-Content "$PSScriptRoot/../version.json" -Raw | ConvertFrom-Json

	if ($versionFileContents.Version.Contains("%")) {
		$versionString = $versionFileContents.Version.Replace("%", "0")
		$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionString)
		$buildVersion = $versionObject.Patch
	}
	else {	
		$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionFileContents.Version)
		$buildVersion = $versionObject.Patch + 1
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

	dotnet build "$PSScriptRoot/../src/Commands/PnP.PowerShell.csproj" --nologo --configuration Release --no-incremental -p:VersionPrefix=$version -p:VersionSuffix=nightly

	if ($IsLinux -or $IsMacOS) {
		$destinationFolder = "$HOME/.local/share/powershell/Modules/PnP.PowerShell"
		$helpfileDestinationFolder = "$HOME/.local/share/powershell/Modules"
	}
	else {
		$documentsFolder = [environment]::getfolderpath("mydocuments")
		$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
		$helpfileDestinationFolder = "$documentsFolder/PowerShell/Modules"
	}

	$corePath = "$destinationFolder/Core"
	$commonPath = "$destinationFolder/Common"
	# Native dependencies live alongside their managed assemblies in Common (the isolated ALC probe path).
	$commonRuntimePathWin64 = "$destinationFolder/Common/runtimes/win-x64/native"
	$commonRuntimePathArm64 = "$destinationFolder/Common/runtimes/win-arm64/native"
	$commonRuntimePathx86 = "$destinationFolder/Common/runtimes/win-x86/native"
	$commonRuntimePathLinx64 = "$destinationFolder/Common/runtimes/linux-x64/native"

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
        # The module assembly itself stays in Core (loaded into the default ALC so its cmdlets are discoverable).
        # The CSOM client libraries (Microsoft.SharePoint.Client.* / Microsoft.Online.SharePoint.Client.*) also
        # stay in Core: PowerShell probes the imported binary module's own directory (Core), so placing them there
        # loads them into the default ALC. That keeps their types resolvable by PowerShell's [TypeName] resolver in
        # user scripts (e.g. [Microsoft.SharePoint.Client.ScriptSafeDomainEntityData]), which only sees the default
        # context. CSOM has no Microsoft.Extensions.* dependency, so sharing it in the default context does not
        # weaken the isolation that fixes the Azure Functions dependency conflict (#5350).
        # Every other assembly is a private dependency and goes to Common, the module's isolated ALC probe path.
        $moduleAssemblies = @('PnP.PowerShell.dll', 'PnP.PowerShell.pdb')
        Copy-Item -Path "$PSscriptRoot/../resources/*.ps1xml" -Destination "$destinationFolder"
        # ScriptsToProcess bootstrap that registers the isolated-dependency resolver before the binary module loads.
        Copy-Item -Path "$PSscriptRoot/../resources/RegisterPnPAssemblyResolver.ps1" -Destination "$destinationFolder"
        Get-ChildItem -Path "$PSScriptRoot/../src/ALC/bin/Release/net8.0" | Where-Object { $_.Extension -in '.dll', '.pdb' } | Foreach-Object { [void]$commonFiles.Add($_.Name); Copy-Item -LiteralPath $_.FullName -Destination $commonPath }
        Get-ChildItem -Path "$PSScriptRoot/../src/Commands/bin/Release/net8.0" | Where-Object { $_.Extension -in '.dll', '.pdb' } | Foreach-Object {
            if ($moduleAssemblies -contains $_.Name -or $_.Name -like 'Microsoft.SharePoint.Client*' -or $_.Name -like 'Microsoft.Online.SharePoint.Client*') {
                Copy-Item -LiteralPath $_.FullName -Destination $corePath
            }
            elseif (-not $commonFiles.Contains($_.Name)) {
                [void]$commonFiles.Add($_.Name)
                Copy-Item -LiteralPath $_.FullName -Destination $commonPath
            }
        }
        
        # Check if runtime folders exist in source before copying
        $sourceRuntimeBase = "$PSScriptRoot/../src/Commands/bin/Release/net8.0/runtimes"
        if (Test-Path $sourceRuntimeBase) {
            Write-Host "Runtime folders found in source, creating destination runtime structure" -ForegroundColor Yellow
            New-Item -Path "$destinationFolder\Common\runtimes" -ItemType Directory -Force | Out-Null
            
            # Copy win-x64 runtime if exists
            $sourceRuntimePathWin64 = "$sourceRuntimeBase/win-x64/native"
            if (Test-Path $sourceRuntimePathWin64) {
                New-Item -Path "$destinationFolder\Common\runtimes\win-x64\native" -ItemType Directory -Force | Out-Null
                Get-ChildItem -Path $sourceRuntimePathWin64 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $commonRuntimePathWin64 }
                Write-Host "Copied win-x64 runtime files" -ForegroundColor Green
            }
            
            # Copy win-arm64 runtime if exists
            $sourceRuntimePathArm64 = "$sourceRuntimeBase/win-arm64/native"
            if (Test-Path $sourceRuntimePathArm64) {
                New-Item -Path "$destinationFolder\Common\runtimes\win-arm64\native" -ItemType Directory -Force | Out-Null
                Get-ChildItem -Path $sourceRuntimePathArm64 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $commonRuntimePathArm64 }
                Write-Host "Copied win-arm64 runtime files" -ForegroundColor Green
            }
            
            # Copy win-x86 runtime if exists
            $sourceRuntimePathx86 = "$sourceRuntimeBase/win-x86/native"
            if (Test-Path $sourceRuntimePathx86) {
                New-Item -Path "$destinationFolder\Common\runtimes\win-x86\native" -ItemType Directory -Force | Out-Null
                Get-ChildItem -Path $sourceRuntimePathx86 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $commonRuntimePathx86 }
                Write-Host "Copied win-x86 runtime files" -ForegroundColor Green
            }

			# Copy linux-x64 runtime if exists
			$sourceRuntimePathLinx64 = "$sourceRuntimeBase/linux-x64/native"
			if (Test-Path $sourceRuntimePathLinx64) {
				New-Item -Path "$destinationFolder\Common\runtimes\linux-x64\native" -ItemType Directory -Force | Out-Null
				Get-ChildItem -Path $sourceRuntimePathLinx64 -Recurse | Where-Object { $_.Extension -in '.dll', '.pdb', '.so' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $commonRuntimePathLinx64 }
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
		# Load the Module in a new PowerShell process so the DLL is released before signing
		$scriptBlock = {
			param([string] $modulePath)

			# Register the isolated-dependency resolver first (same bootstrap the manifest's ScriptsToProcess uses),
			# otherwise importing the raw DLL fails to resolve PnP.Framework/PnP.Core now that they live in Common.
			. (Join-Path $modulePath "RegisterPnPAssemblyResolver.ps1")
			$moduleAssemblyPath = Join-Path $modulePath "Core/PnP.PowerShell.dll"
			Import-Module -Name $moduleAssemblyPath -DisableNameChecking

			$cmdlets = Get-Command -Module PnP.PowerShell | ForEach-Object { "`"$_`"" }
			$cmdlets -Join ","
		}

		Write-Host "Starting job to retrieve cmdlet names" -ForegroundColor Yellow
		$cmdletJob = Start-Job -ScriptBlock $scriptBlock -ArgumentList (Resolve-Path -LiteralPath $destinationFolder).Path
		try {
			$cmdletsString = Receive-Job -Job $cmdletJob -Wait -ErrorAction Stop
			if ($cmdletJob.State -ne "Completed") {
				throw "Failed to retrieve cmdlet names. Job state: $($cmdletJob.State)"
			}
		}
		finally {
			Remove-Job -Job $cmdletJob -Force -ErrorAction SilentlyContinue
		}

		Write-Host "Writing PSD1" -ForegroundColor Yellow
		$manifest = "@{
	ScriptsToProcess = 'RegisterPnPAssemblyResolver.ps1'
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
	& "$PSScriptRoot/../build/Generate-PredictorCommands.ps1" -Version "nightly"

	Write-Host "Generating Documentation" -ForegroundColor Yellow
	Set-PSRepository PSGallery -InstallationPolicy Trusted
	Install-Module -Name Microsoft.PowerShell.PlatyPS -RequiredVersion 1.0.3
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path "$PSScriptRoot/../documentation/*.md"
	$mdFiles | Import-MarkdownCommandHelp -Path {$_.FilePath} | Export-MamlCommandHelp -OutputFolder $helpfileDestinationFolder -Force
	& "$PSScriptRoot/Assert-OnlineHelpLinks.ps1" -OutputFolder $helpfileDestinationFolder -DocumentationPath "$PSScriptRoot/../documentation"
	# Install-Module Microsoft.PlatyPS -ErrorAction Stop
	# New-ExternalHelp -Path "$PSScriptRoot/../documentation" -OutputPath $destinationFolder -Force

    # Sign all required DLLs
    function Invoke-ModuleFileSigning {
		param(
			[Parameter(Mandatory = $true)]
			[System.IO.FileInfo] $File
		)

		Write-Host "Signing $($File.FullName)"
		$signCliPath = $env:SIGN_CLI_PATH
		if ([string]::IsNullOrWhiteSpace($signCliPath)) {
			$signCliPath = Join-Path $PSScriptRoot "../sign"
			if ($IsWindows -and !(Test-Path -LiteralPath $signCliPath) -and (Test-Path -LiteralPath "$signCliPath.exe")) {
				$signCliPath = "$signCliPath.exe"
			}
		}

		if (!(Test-Path -LiteralPath $signCliPath)) {
			throw "Sign CLI not found at $signCliPath"
		}

		& $signCliPath code azure-key-vault $File.FullName `
			--publisher-name "Microsoft 365 Patterns and Practices" `
			--description "PnP PowerShell Module" `
			--description-url "https://pnp.github.io/powershell/" `
			--azure-key-vault-tenant-id $("$env:SIGNING_TENANTID") `
			--azure-key-vault-client-id $("$env:SIGNING_CLIENT_ID") `
			--azure-key-vault-certificate $("$env:SIGNING_CERTNAME") `
			--azure-key-vault-url $("$env:SIGNING_VAULTURL") `
			--timestamp-url "http://timestamp.digicert.com" `
			--verbosity Debug

		if ($LASTEXITCODE -ne 0) {
			throw "Signing failed for $($File.FullName)"
		}
	}

    Write-Host "Sign module assemblies"
    $assembliesToBeSigned = @(
		Get-Item -LiteralPath "$corePath/PnP.PowerShell.dll"
		Get-Item -LiteralPath "$commonPath/PnP.Core.dll"
		Get-Item -LiteralPath "$commonPath/PnP.Core.Admin.dll"
		Get-Item -LiteralPath "$commonPath/PnP.Core.Auth.dll"
		Get-Item -LiteralPath "$commonPath/PnP.Framework.dll"
		Get-Item -LiteralPath "$commonPath/PnP.PowerShell.ALC.dll"
    )

	foreach ($assemblyToBeSigned in $assembliesToBeSigned) {
		Invoke-ModuleFileSigning -File $assemblyToBeSigned
	}

	Write-Host "Sign PowerShell module files"
	$powerShellFilesToBeSigned = Get-ChildItem -LiteralPath $destinationFolder -Recurse -File | Where-Object { $_.Extension -in '.ps1', '.psm1', '.ps1xml', '.psd1' }

	foreach ($powerShellFileToBeSigned in $powerShellFilesToBeSigned) {
		Invoke-ModuleFileSigning -File $powerShellFileToBeSigned
	}

	$apiKey = $("$env:POWERSHELLGALLERY_API_KEY")

	Write-Host "Publishing Module version $version" -ForegroundColor Yellow
	Import-Module -Name PnP.PowerShell
	Publish-Module -Name PnP.PowerShell -AllowPrerelease -NuGetApiKey $apiKey

	# Write version back to version
	Set-Content "$PSScriptRoot/../version.txt" -Value $version -Force -NoNewline

	# Write version back to version.json
	$json = @{Version = "$version"; Message = "" } | ConvertTo-Json
	Set-Content "$PSScriptRoot/../version.json" -Value $json -Force -NoNewline
}
else {
	Write-Host "No changes in PnP PowerShell, PnP Framework or PnP Core SDK. Exiting." -ForegroundColor Green
}
