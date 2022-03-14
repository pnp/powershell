cd /home/powershell

$versionFileContents = (Get-Content ./version.txt -Raw).Trim()
$versionFileContents
$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionFileContents)
$buildVersion = $versionObject.Patch + 1;
$version = "$($versionObject.Major).$($versionObject.Minor).$buildVersion"
dotnet build ./src/Commands/PnP.PowerShell.csproj --nologo --configuration Release --no-incremental -p:VersionPrefix=$version -p:VersionSuffix=nightly

$documentsFolder = [environment]::getfolderpath("mydocuments");
$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
$corePath = "$destinationFolder/Core"
$commonPath = "$destinationFolder/Common"
$frameworkPath = "$destinationFolder/Framework"
$assemblyExceptions = @("System.Memory.dll");
Write-Host "Creating target folders: $destinationFolder" -ForegroundColor Yellow
New-Item -Path $destinationFolder -ItemType Directory -Force | Out-Null
New-Item -Path "$destinationFolder\Core" -ItemType Directory -Force | Out-Null
New-Item -Path "$destinationFolder\Common" -ItemType Directory -Force | Out-Null
Write-Host "Copying files to $destinationFolder" -ForegroundColor Yellow

$commonFiles = [System.Collections.Generic.Hashset[string]]::new()
Copy-Item -Path "./resources/*.ps1xml" -Destination "$destinationFolder"
Get-ChildItem -Path "./src/ALC/bin/Release/netstandard2.0" | Where-Object { $_.Extension -in '.dll', '.pdb' } | Foreach-Object { if (!$assemblyExceptions.Contains($_.Name)) { [void]$commonFiles.Add($_.Name) }; Copy-Item -LiteralPath $_.FullName -Destination $commonPath }
Get-ChildItem -Path "./src/Commands/bin/Release/netcoreapp3.1" | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $corePath }

Write-Host "Generating PnP.PowerShell.psd1" -ForegroundColor Yellow
# Load the Module in a new PowerShell session
$scriptBlock = {
    $documentsFolder = [environment]::getfolderpath("mydocuments");
    $destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
    Import-Module -Name "$destinationFolder/Core/PnP.PowerShell.dll" -DisableNameChecking
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
			Prerelease = 'nightly'
			ProjectUri = 'https://aka.ms/sppnp'
			IconUri = 'https://raw.githubusercontent.com/pnp/media/40e7cd8952a9347ea44e5572bb0e49622a102a12/parker/ms/300w/parker-ms-300.png'
		}
	}
}"
$manifest | Out-File "$destinationFolder/PnP.PowerShell.psd1" -Force
Import-Module -Name PnP.PowerShell
