param($ProjectDir, $ConfigurationName, $TargetDir, $TargetFileName, $SolutionDir)

if($ConfigurationName -like "Debug*")
{
	$documentsFolder = [environment]::getfolderpath("mydocuments");
	$destinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"
	$corePath = "$destinationFolder/Core"
	$commonPath = "$destinationFolder/Common"
	$frameworkPath = "$destinationFolder/Framework"
	# Module folder there?
	if(Test-Path $destinationFolder)
	{
		# Yes, empty it
		Remove-Item $destinationFolder\* -Recurse -Force
	}
		# No, create it
	Write-Host "Creating target folders: $destinationFolder"
	New-Item -Path $destinationFolder -ItemType Directory -Force | Out-Null
	New-Item -Path "$destinationFolder\Core" -ItemType Directory -Force | Out-Null
	New-Item -Path "$destinationFolder\Common" -ItemType Directory -Force | Out-Null
	New-Item -Path "$destinationFolder\Framework" -ItemType Directory -Force | Out-Null
	
	Write-Host "Copying files to $destinationFolder"
	Try {
		$commonFiles = [System.Collections.Generic.Hashset[string]]::new()
		Copy-Item -Path "$TargetDir..\ModuleFiles\*.psd1" -Destination "$destinationFolder"
		Copy-Item -Path "$TargetDir\ModuleFiles\*.ps1xml" -Destination "$destinationFolder"
		Get-ChildItem -Path "$PSScriptRoot/PnP.PowerShell.ALC/bin/$ConfigurationName/netstandard2.0" | Where-Object {$_.Extension -in '.dll','.pdb' } | Foreach-Object { [void]$commonFiles.Add($_.Name); Copy-Item -LiteralPath $_.FullName -Destination $commonPath }
		Get-ChildItem -Path "$PSScriptRoot/Commands/bin/$ConfigurationName/netcoreapp3.1" | Where-Object {$_.Extension -in '.dll','.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $corePath }
		Get-ChildItem -Path "$PSScriptRoot/Commands/bin/$ConfigurationName/net461" | Where-Object {$_.Extension -in '.dll','.pdb' -and -not $commonFiles.Contains($_.Name) } | Foreach-Object { Copy-Item -LiteralPath $_.FullName -Destination $frameworkPath }
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

	