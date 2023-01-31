$runPublish = $false

$pnppowershell_hash = git ls-files -s ./src | git hash-object --stdin
$existing_pnppowershell_hash = Get-Content ./pnppowershell_hash.txt -Raw -ErrorAction SilentlyContinue

$existing_pnpframework_hash = Get-Content ./pnpframework_hash.txt -Raw -ErrorAction SilentlyContinue
$pnpframework_response = Invoke-RestMethod -Method Get -Uri "$($env:GITHUB_API_URL)/repos/pnp/pnpframework/branches/dev" -SkipHttpErrorCheck
if($null -ne $pnpframework_response)
{
	if($null -ne $pnpframework_response.commit)
	{
		$pnpframework_hash = $pnpframework_response.commit.sha
	}
}

$existing_pnpcoresdk_hash = Get-Content ./pnpcoresdk_hash.txt -Raw -ErrorAction SilentlyContinue
$pnpcoresdk_response = Invoke-RestMethod -Method Get -Uri "$($env:GITHUB_API_URL)/repos/pnp/pnpcore/branches/dev" -SkipHttpErrorCheck
if($null -ne $pnpcoresdk_response)
{
	if($null -ne $pnpcoresdk_response.commit)
	{
		$pnpcoresdk_hash = $pnpcoresdk_response.commit.sha
	}
}

#Write-host "Latest PnP PowerShell Commit hash $pnppowershell_hash" -ForegroundColor Yellow
#Write-Host "Stored PnP PowerShell Commit hash: $existing_pnppowershell_hash" -ForegroundColor Yellow
#Write-host "Latest PnP Framework Commit hash $pnpframework_hash" -ForegroundColor Yellow
#Write-Host "Stored PnP Framework Commit hash: $existing_pnpframework_hash" -ForegroundColor Yellow

if ($existing_pnppowershell_hash -ne $pnppowershell_hash)
{
	Write-Host "PnP PowerShell is newer"
	Set-Content ./pnppowershell_hash.txt -Value $pnppowershell_hash -NoNewline -Force
	$runPublish = $true
}

if($runPublish -eq $false -and $existing_pnpframework_hash -ne $pnpframework_hash)
{
	Write-Host "PnP Framework is newer"
	Set-Content ./pnpframework_hash.txt -Value $pnpframework_hash -NoNewline -Force
	$runPublish = $true
}

if($runPublic -eq $false -and $existing_pnpcoresdk_hash -ne $pnpcoresdk_hash)
{
	Write-Host "PnP Core SDK is newer"
	Set-Content ./pnpcoresdk_hash.txt -Value $pnpcoresdk_hash -NoNewLine -Force
	$runPublish = $true
}

if ($runPublish -eq $true) {

	$versionFileContents = (Get-Content "$PSScriptRoot/../version.txt" -Raw).Trim()
	if ($versionFileContents.Contains("%")) {
		$versionString = $versionFileContents.Replace("%", "0");
		$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionString)
		$buildVersion = $versionObject.Patch;
	}
	else {	
		$versionObject = [System.Management.Automation.SemanticVersion]::Parse($versionFileContents)
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
		Get-ChildItem -Path "$PSScriptRoot/../src/Commands/bin/Release/net6.0-windows" | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $corePath }
		if (!$IsLinux -and !$IsMacOs) {
			Get-ChildItem -Path "$PSScriptRoot/../src/Commands/bin/Release/net462" | Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $frameworkPath }
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

			if ($IsLinux -or $isMacOS) {
				$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
			}
			else {
				$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
			}
			if($PSVersionTable.PSVersion.Major -eq 5)
			{
				Write-Host "Importing Framework version of assembly" -ForegroundColor Yellow
				Import-Module -Name "$destinationFolder/Framework/PnP.PowerShell.dll" -DisableNameChecking
			} else {
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
	NestedModules =  'Core/PnP.PowerShell.dll'
	ModuleVersion = '$version'
	Description = 'Microsoft 365 Patterns and Practices PowerShell Cmdlets'
	GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
	Author = 'Microsoft 365 Patterns and Practices'
	CompanyName = 'Microsoft 365 Patterns and Practices'
	CompatiblePSEditions = @(`"Core`",`"Desktop`")
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
	Install-Module PlatyPS -ErrorAction Stop
	New-ExternalHelp -Path ./documentation -OutputPath $destinationFolder -Force

	$apiKey = $("$env:POWERSHELLGALLERY_API_KEY")

	Write-Host "Publishing Module version $version" -ForegroundColor Yellow
	Import-Module -Name PnP.PowerShell
	Publish-Module -Name PnP.PowerShell -AllowPrerelease -NuGetApiKey $apiKey

	# Write version back to version
	Set-Content ./version.txt -Value $version -Force -NoNewline
}
