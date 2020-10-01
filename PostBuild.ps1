param($ProjectDir, $ConfigurationName, $TargetDir, $TargetFileName, $SolutionDir)

$documentsFolder = [environment]::getfolderpath("mydocuments");
$destinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"
$corePath = "$destinationFolder/Core"
$commonPath = "$destinationFolder/Common"
$frameworkPath = "$destinationFolder/Framework"

Try {
	# Module folder there?
	if(Test-Path $destinationFolder)
	{
		# Yes, empty it
		Remove-Item $destinationFolder\* -Recurse -Force -ErrorAction Stop
	}
		# No, create it
	Write-Host "-- Creating target folders: $destinationFolder" -ForegroundColor Yellow
	New-Item -Path $destinationFolder -ItemType Directory -Force | Out-Null
	New-Item -Path "$destinationFolder\Core" -ItemType Directory -Force | Out-Null
	New-Item -Path "$destinationFolder\Common" -ItemType Directory -Force | Out-Null
	New-Item -Path "$destinationFolder\Framework" -ItemType Directory -Force | Out-Null
	
	Write-Host "Copying files to $destinationFolder" -ForegroundColor Yellow

	$commonFiles = [System.Collections.Generic.Hashset[string]]::new()
	Copy-Item -Path "$TargetDir..\ModuleFiles\*.psd1" -Destination "$destinationFolder"
	Copy-Item -Path "$TargetDir\ModuleFiles\*.ps1xml" -Destination "$destinationFolder"
	Get-ChildItem -Path "$PSScriptRoot/PnP.PowerShell.ALC/bin/$ConfigurationName/netstandard2.0" | Where-Object {$_.Extension -in '.dll','.pdb' } | Foreach-Object { [void]$commonFiles.Add($_.Name); Copy-Item -LiteralPath $_.FullName -Destination $commonPath }
	Get-ChildItem -Path "$PSScriptRoot/Commands/bin/$ConfigurationName/netcoreapp3.1" | Where-Object {$_.Extension -in '.dll','.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $corePath }
	Get-ChildItem -Path "$PSScriptRoot/Commands/bin/$ConfigurationName/net461" | Where-Object {$_.Extension -in '.dll','.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $frameworkPath }
}
Catch
{
	Write-Host "Error: Cannot copy files to $destinationFolder. Maybe a PowerShell session is still using the module?"
	exit 1
}

Try {
	Write-Host "-- Generating PnP.PowerShell.psd1" -ForegroundColor Yellow
	Import-Module -Name "$destinationFolder\Core\PnP.PowerShell.dll" -DisableNameChecking
	$cmdlets = get-command -Module PnP.PowerShell | %{"`"$_`""}
	$cmdletsString = $cmdlets -Join ","
	$manifest = "@{
	NestedModules =  if (`$PSEdition -eq 'Core')
	{
		'Core/PnP.PowerShell.dll'
	}
	else
	{
		'Framework/PnP.PowerShell.dll'
	}
	ModuleVersion = '0.1.0'
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
			ProjectUri = 'https://aka.ms/sppnp'
			IconUri = 'https://github.com/pnp/media/raw/e62d267575c81bda81485111ec52714033141e62/parker/pnp/300w/parker.png'
		}
	}
}"
	$manifest | Out-File "$destinationFolder\PnP.PowerShell.psd1"
}
Catch 
{
	Write-Host "Error: Cannot generate PnP.PowerShell.psd1. Maybe a PowerShell session is still using the module?"
	exit 1
}
	