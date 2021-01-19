Param(
    [parameter(Mandatory = $true)]
    [String]$ReleaseVersion
)
$version = $ReleaseVersion

Write-Host "Building PnP.PowerShell $version on PowerShell $($PSVersionTable.PSVersion.ToString())" -ForegroundColor Yellow

dotnet restore $PSScriptRoot/../src/Commands/PnP.PowerShell.csproj
dotnet build $PSScriptRoot/../src/Commands/PnP.PowerShell.csproj --nologo --configuration Release --no-incremental -p:Version=$version

$documentsFolder = [environment]::getfolderpath("mydocuments");

if ($IsLinux -or $isMacOS) {
	$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
}
else {
	$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
}

$corePath = "$destinationFolder/Core"
$commonPath = "$destinationFolder/Common"
$frameworkPath = "$destinationFolder/Framework"

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
	if (!$IsLinux -and !$IsMacOs) {
		New-Item -Path "$destinationFolder\Framework" -ItemType Directory -Force | Out-Null
	}

	Write-Host "Copying files to $destinationFolder" -ForegroundColor Yellow

	$commonFiles = [System.Collections.Generic.Hashset[string]]::new()
	Copy-Item -Path "$PSscriptRoot/../resources/*.ps1xml" -Destination "$destinationFolder"
	Get-ChildItem -Path "$PSScriptRoot/../src/ALC/bin/Release/netstandard2.0" | Where-Object { $_.Extension -in '.dll', '.pdb' } | Foreach-Object { if (!$assemblyExceptions.Contains($_.Name)) { [void]$commonFiles.Add($_.Name) }; Copy-Item -LiteralPath $_.FullName -Destination $commonPath }
	Get-ChildItem -Path "$PSScriptRoot/../src/Commands/bin/Release/netcoreapp3.1" | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $corePath }
	if (!$IsLinux -and !$IsMacOs) {
		Get-ChildItem -Path "$PSScriptRoot/../src/Commands/bin/Release/net461" | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $frameworkPath }
	}
}
Catch {
	Write-Host "Error: Cannot copy files to $destinationFolder. Maybe a PowerShell session is still using the module?"
	exit 1
}

Try {
	Write-Host "Generating PnP.PowerShell.psd1" -ForegroundColor Yellow
	# Load the Module in a new PowerShell session
	$scriptBlock = {
		$documentsFolder = [environment]::getfolderpath("mydocuments");

		if ($IsLinux -or $isMacOS) {
			$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
		}
		else {
			$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
		}
		if ($PSVersionTable.PSVersion.Major -eq 5) {
			Write-Host "Importing Framework version of assembly" -ForegroundColor Yellow
			Import-Module -Name "$destinationFolder/Framework/PnP.PowerShell.dll" -DisableNameChecking
		}
		else {
			Write-Host "Importing dotnet core version of assembly" -ForegroundColor Yellow
			Import-Module -Name "$destinationFolder/Core/PnP.PowerShell.dll" -DisableNameChecking
		}
		Write-Host "Getting cmdlet info" -ForegroundColor Yellow
		$cmdlets = Get-Command -Module PnP.PowerShell | ForEach-Object { "`"$_`"" }
		$cmdlets -Join ","
	}

	Write-Host "Starting job to retrieve cmdlet names" -ForegroundColor Yellow
	$cmdletsString = Start-ThreadJob -ScriptBlock $scriptBlock | Receive-Job -Wait

	Write-Host "Writing PSD1" -ForegroundColor Yellow
	$manifest = "@{
	NestedModules =  if (`$PSEdition -eq 'Core')
	{
		'Core/PnP.PowerShell.dll'
	}
	else
	{
		'Framework/PnP.PowerShell.dll'
	}
	ModuleVersion = '$version'
	Description = 'Microsoft 365 Patterns and Practices PowerShell Cmdlets'
	GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
	Author = 'Microsoft 365 Patterns and Practices'
	CompanyName = 'Microsoft 365 Patterns and Practices'
	CompatiblePSEditions = @(`"Core`",`"Desktop`")
	PowerShellVersion = '5.1'
	DotNetFrameworkVersion = '4.6.1'
	ProcessorArchitecture = 'None'
	FunctionsToExport = '*'  
	CmdletsToExport = @($cmdletsString)
	VariablesToExport = '*'
	AliasesToExport = '*'
	FormatsToProcess = 'PnP.PowerShell.Format.ps1xml' 
	PrivateData = @{
		PSData = @{
			Tags = 'SharePoint','PnP','Teams','Planner'
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

$documentationJob = {
	Write-Host "Generating Documentation" -ForegroundColor Yellow
	Set-PSRepository PSGallery -InstallationPolicy Trusted
	#Install-Module PlatyPS -ErrorAction Stop
	Import-Module PlatyPS
	New-ExternalHelp -Path $args[1] -OutputPath $args[0] -Force
}

Start-ThreadJob -ScriptBlock $documentationJob -ArgumentList $destinationFolder, "$PSScriptRoot/../documentation" | Receive-Job -Wait