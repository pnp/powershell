param($ProjectDir, $ConfigurationName, $TargetDir, $TargetFileName, $SolutionDir)

if($ConfigurationName -like "Debug*")
{
	$documentsFolder = [environment]::getfolderpath("mydocuments");
	$destinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"
	
	# Module folder there?
	if(Test-Path $destinationFolder)
	{
		# Yes, empty it
		Remove-Item $destinationFolder\*
	} else {
		# No, create it
		Write-Host "Creating target folder: $destinationFolder"
		New-Item -Path $destinationFolder -ItemType Directory -Force >$null # Suppress output
	}

	Write-Host "Copying files from $TargetDir to $destinationFolder"
	Try {
		Copy-Item "$TargetDir\*.dll" -Destination "$destinationFolder"
		Copy-Item "$TargetDir\ModuleFiles\*" -Destination "$destinationFolder"
	}
	Catch
	{
		Write-Host "Cannot copy files to $destinationFolder. Maybe a PowerShell session is still use the module?"
		exit 1
	}
} elseif ($ConfigurationName -like "Release*")
{
    $documentsFolder = [environment]::getfolderpath("mydocuments");
	$destinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"

	# Module folder there?
	if(Test-Path $destinationFolder)
	{
		# Yes, empty it
		Remove-Item $destinationFolder\*
	} else {
		# No, create it
		Write-Host "Creating target folder: $destinationFolder"
		New-Item -Path $destinationFolder -ItemType Directory -Force >$null # Suppress output
	}

	Write-Host "Copying files from $TargetDir to $destinationFolder"
	Try
	{
		Copy-Item "$TargetDir\*.dll" -Destination "$destinationFolder"
		Copy-Item "$TargetDir\ModuleFiles\PnP.PowerShell.psd1" -Destination "$destinationFolder"
		Copy-Item "$TargetDir\ModuleFiles\PnP.PowerShell.Format.ps1xml" -Destination "$destinationFolder"
	} 
	Catch
	{
		exit 1
	}
}

	