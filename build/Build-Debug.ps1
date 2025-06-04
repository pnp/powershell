#Requires -PSEdition Core
Param(
	[Parameter(Mandatory = $false,
		ValueFromPipeline = $false)]
	[switch]
	$NoIncremental,
	[Parameter(Mandatory = $false,
		ValueFromPipeline = $false)]
	[switch]
	$Force,
	[Parameter(Mandatory = $false,
		ValueFromPipeline = $false)]
	[switch]
	$LocalPnPFramework,
	[Parameter(Mandatory = $false,
		ValueFromPipeline = $false)]
	[switch]
	$LocalPnPCore
)

$localPnPCoreSdkPathValue = $env:PnPCoreSdkPath
$localPnPFrameworkPathValue = $env:PnPFrameworkPath

$env:PnPCoreSdkPath = ""
$env:PnPFrameworkPath = ""

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

# $versionFileContents = Get-Content "$PSScriptRoot/../version.txt" -Raw
# if ($versionFileContents.Contains("%")) {
# 	$versionString = $versionFileContents.Replace("%", "0");
# 	$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionString)
# 	$buildVersion = $versionObject.Patch;
# }
# else {	
# 	$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionFileContents)
# 	$buildVersion = $versionObject.Patch + 1;
# }

$configuration = "net8.0"

$version = "$($versionObject.Major).$($versionObject.Minor).$buildVersion"

Write-Host "Building PnP.PowerShell version $version-debug" -ForegroundColor Yellow

$buildCmd = "dotnet build `"$PSScriptRoot/../src/Commands/PnP.PowerShell.csproj`" --nologo --configuration Debug -p:VersionPrefix=$version -p:VersionSuffix=debug";
if ($NoIncremental) {
	$buildCmd += " --no-incremental";
}
if ($Force) {
	$buildCmd += " --force"
}
if ($LocalPnPFramework) {
	# Check if available
	$pnpFrameworkAssembly = Join-Path $PSScriptRoot -ChildPath "..\..\pnpframework\src\lib\PnP.Framework\bin\Debug\$configuration\PnP.Framework.dll"
	$pnpFrameworkAssembly = [System.IO.Path]::GetFullPath($pnpFrameworkAssembly)
	if (Test-Path $pnpFrameworkAssembly -PathType Leaf) {
		$pnpFrameworkAssemblyItem = Get-ChildItem -Path $pnpFrameworkAssembly
		Write-Host "  Using local PnP Framework build located at $($pnpFrameworkAssemblyItem.FullName) compiled at $($pnpFrameworkAssemblyItem.LastWritetime)" -ForegroundColor Yellow
		$buildCmd += " -p:PnPFrameworkPath=`"..\..\..\pnpframework\src\lib\`""
	}
 	else {
		$localFolder = Join-Path $PSScriptRoot -ChildPath "..\..\pnpframework"
		$localFolder = [System.IO.Path]::GetFullPath($localFolder)
		Write-Error -Message "Please make sure you have a local copy of the PnP.Framework repository installed at $localFolder"
	}
}
if ($LocalPnPCore) {
	$pnpCoreAssembly = Join-Path $PSScriptRoot -ChildPath "..\..\pnpcore\src\sdk\PnP.Core\bin\Debug\$configuration\PnP.Core.dll"
	$pnpCoreAssembly = [System.IO.Path]::GetFullPath($pnpCoreAssembly)
	if (Test-Path $pnpCoreAssembly -PathType Leaf) {
		$pnpCoreAssemblyItem = Get-ChildItem -Path $pnpCoreAssembly
		Write-Host "  Using local PnP.Core SDK build located at $($pnpCoreAssemblyItem.FullName) compiled at $($pnpCoreAssemblyItem.LastWritetime)" -ForegroundColor Yellow
		$buildCmd += " -p:PnPCoreSdkPath=`"..\..\..\pnpcore\src\sdk\`""
	}
 	else {
		$localFolder = Join-Path $PSScriptRoot -ChildPath "..\..\pnpcore"
		$localFolder = [System.IO.Path]::GetFullPath($localFolder)
		Write-Error -Message "Please make sure you have a local copy of the PnP.Core repository installed at $localFolder"
	}
}

Write-Host "Executing $buildCmd" -ForegroundColor Yellow

Invoke-Expression $buildCmd

if ($LASTEXITCODE -eq 0) {
	$documentsFolder = [environment]::getfolderpath("mydocuments");

	if ($IsLinux -or $isMacOS) {
		$destinationFolder = "$HOME/.local/share/powershell/Modules/PnP.PowerShell"
	}
	else {
		$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
	}

	$corePath = "$destinationFolder/Core"
	$commonPath = "$destinationFolder/Common"

	$assemblyExceptions = @("System.Memory.dll");
	
	Try {
		# Module folder there?
		if (Test-Path $destinationFolder) {
			# Yes, empty it. Do it per folder as that seems the only way to delete the PS modules from a ODB synced folder
			Remove-Item $destinationFolder\common\* -Recurse -Force -ErrorAction SilentlyContinue
			Remove-Item $destinationFolder\core\* -Recurse -Force -ErrorAction SilentlyContinue
			Remove-Item $destinationFolder\framework\* -Recurse -Force -ErrorAction SilentlyContinue
			Remove-Item $destinationFolder\* -Recurse -Force -ErrorAction SilentlyContinue
		}
		# No, create it
		Write-Host "Creating target folders: $destinationFolder" -ForegroundColor Yellow
		New-Item -Path $destinationFolder -ItemType Directory -Force | Out-Null
		New-Item -Path "$destinationFolder\Core" -ItemType Directory -Force | Out-Null
		New-Item -Path "$destinationFolder\Common" -ItemType Directory -Force | Out-Null

		Write-Host "Copying files to $destinationFolder" -ForegroundColor Yellow

		$commonFiles = [System.Collections.Generic.Hashset[string]]::new()
		Copy-Item -Path "$PSScriptRoot/../resources/*.ps1xml" -Destination "$destinationFolder"
		Get-ChildItem -Path "$PSScriptRoot/../src/ALC/bin/Debug/net8.0" | Where-Object { $_.Extension -in '.dll', '.pdb' } | Foreach-Object { if (!$assemblyExceptions.Contains($_.Name)) { [void]$commonFiles.Add($_.Name) }; Copy-Item -LiteralPath $_.FullName -Destination $commonPath }
		Get-ChildItem -Path "$PSScriptRoot/../src/Commands/bin/Debug/$configuration" | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $corePath }

		if ($LocalPnPCore) {
			# Ensure the local PnP.Core SDK is copied to the module folder or else debugging will not work. This assembly otherwises comes in through PnP Framework and will still use the NuGet version instead of the local build.
			Write-Host "  Copying local PnP.Core SDK assembly" -ForegroundColor Yellow
			Copy-Item -Path $pnpCoreAssembly -Destination "$destinationFolder\Core" -Force
		}
	}
	Catch {
		Write-Error "Cannot copy files to $destinationFolder. Maybe a PowerShell session is still using the module or PS modules are hosted in a OneDrive synced location. In the latter case, manually delete $destinationFolder and try again."
		exit 1
	}

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
			Write-Host "Importing dotnet core version of assembly"
			Import-Module -Name "$destinationFolder/Core/PnP.PowerShell.dll" -DisableNameChecking
			$cmdlets = Get-Command -Module PnP.PowerShell | ForEach-Object { "`"$_`"" }
			$cmdlets -Join ","
		}
		$cmdletsString = Start-ThreadJob -ScriptBlock $scriptBlock | Receive-Job -Wait

		$manifest = "@{
	NestedModules =  'Core/PnP.PowerShell.dll'
	ModuleVersion = '$version'
	Description = 'Microsoft 365 Patterns and Practices PowerShell Cmdlets'
	GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
	Author = 'Microsoft 365 Patterns and Practices'
	CompanyName = 'Microsoft 365 Patterns and Practices'
	CompatiblePSEditions = @('Core')
	PowerShellVersion = '7.4.0'
	ProcessorArchitecture = 'None'
	FunctionsToExport = '*'  
	CmdletsToExport = @($cmdletsString)
	VariablesToExport = '*'
	AliasesToExport = '*'
	FormatsToProcess = 'PnP.PowerShell.Format.ps1xml' 
	PrivateData = @{
		PSData = @{
			Prerelease = 'debug'
			ProjectUri = 'https://aka.ms/sppnp'
			IconUri = 'https://raw.githubusercontent.com/pnp/media/40e7cd8952a9347ea44e5572bb0e49622a102a12/parker/ms/300w/parker-ms-300.png'
		}
	}
}"
		$manifest | Out-File "$destinationFolder/PnP.PowerShell.psd1"
	}
	Catch {
		Write-Host "Error: Cannot generate PnP.PowerShell.psd1. Maybe a PowerShell session is still using the module?"
		exit 1
	}
	Write-Host "`n`n Build and provisioning succeeded`n Version: $version" -ForegroundColor Green
}

$env:PnPCoreSdkPath = $localPnPCoreSdkPathValue
$env:PnPFrameworkPath = $localPnPFrameworkPathValue
