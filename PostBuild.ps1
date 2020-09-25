param($ProjectDir, $ConfigurationName, $TargetDir, $TargetFileName, $SolutionDir, $TargetFramework)

if($ConfigurationName -like "Debug*")
{
	$documentsFolder = [environment]::getfolderpath("mydocuments");
	if($TargetFramework -eq "netstandard20")
	{
		$DestinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"
	} else {
		$DestinationFolder = "$documentsFolder\WindowsPowerShell\Modules\PnP.PowerShell"
	}
	# Module folder there?
	if(Test-Path $DestinationFolder)
	{
		# Yes, empty it
		Remove-Item $DestinationFolder\*
	} else {
		# No, create it
		Write-Host "Creating target folder: $DestinationFolder"
		New-Item -Path $DestinationFolder -ItemType Directory -Force >$null # Suppress output
	}

	Write-Host "Copying files from $TargetDir to $DestinationFolder"
	Try {
		Copy-Item "$TargetDir\*.dll" -Destination "$DestinationFolder"
		Copy-Item "$TargetDir\*help.xml" -Destination "$DestinationFolder"
		Copy-Item "$TargetDir\ModuleFiles\*" -Destination "$DestinationFolder"
	}
	Catch
	{
		exit 1
	}
} elseif ($ConfigurationName -like "Release*")
{
    $documentsFolder = [environment]::getfolderpath("mydocuments");
	if($TargetFramework -eq "netstandard20")
	{
		$DestinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"
	} else {
		$DestinationFolder = "$documentsFolder\WindowsPowerShell\Modules\PnP.PowerShell"
	}

	# Module folder there?
	if(Test-Path $DestinationFolder)
	{
		# Yes, empty it
		Remove-Item $DestinationFolder\*
	} else {
		# No, create it
		Write-Host "Creating target folder: $DestinationFolder"
		New-Item -Path $DestinationFolder -ItemType Directory -Force >$null # Suppress output
	}

	Write-Host "Copying files from $TargetDir to $DestinationFolder"
	Try
	{
		Copy-Item "$TargetDir\*.dll" -Destination "$DestinationFolder"
		Copy-Item "$TargetDir\*help.xml" -Destination "$DestinationFolder"
		Copy-Item "$TargetDir\ModuleFiles\PnP.PowerShell.psd1" -Destination "$DestinationFolder"
		Copy-Item "$TargetDir\ModuleFiles\PnP.PowerShell.Format.ps1xml" -Destination "$DestinationFolder"
	} 
	Catch
	{
		exit 1
	}
}

	