$documentsFolder = [environment]::getfolderpath("mydocuments");
if($IsLinux -or $isMacOS)
{
	$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
} else {
	$destinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"
}

$runsInAction = $("$env:RUNSINACTION")
if($runsInAction -ne [String]::Empty)
{
	# We are running in a GitHub Action
	Write-Host "Installing PlatyPS"
	Set-PSRepository PSGallery -InstallationPolicy Trusted
	Install-Module PlatyPS -ErrorAction Stop
	Write-Host "Generating external help"
	New-ExternalHelp -Path ./documentation -OutputPath $destinationFolder -Force
} else {
	# We are running locally, check if platyps is installed
	$modules = Get-Module -Name platyPS -ListAvailable
	if($modules.Count -eq 0)
	{
		# Not installed
		$choices = '&Yes','&No'
		$install = $Host.UI.PromptForChoice("Install PlatyPS","We need the PowerShell PlatyPS module to generate documentation. Install this?",$choices, 1)
		if($install -eq 0)
		{
			Install-Module -Name PlatyPS -ErrorAction Stop
		} else {
			exit
		}
	}
	Write-Host "Generating external help"
	New-ExternalHelp -Path ./../documentation -OutputPath $destinationFolder -Force
}	
