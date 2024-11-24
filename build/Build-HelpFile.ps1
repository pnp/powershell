$documentsFolder = [environment]::getfolderpath("mydocuments")
if($IsLinux -or $isMacOS)
{
	$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
} else {
	$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
}

$tempFolder = [System.IO.Path]::GetTempPath()

$runsInAction = $("$env:RUNSINACTION")
if($runsInAction -ne [String]::Empty)
{
	# We are running in a GitHub Action
	Write-Host "Installing Microsoft.PowerShell.PlatyPS"
	Set-PSRepository PSGallery -InstallationPolicy Trusted
	Install-Module -Name Microsoft.PowerShell.PlatyPS -AllowPrerelease -ErrorAction Stop
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path ./documentation/*.md
	$commandHelp = $mdFiles | Where-Object { $_.FileType -match 'CommandHelp'} | Import-MarkdownCommandHelp -Path {$_.FilePath}
	Export-MamlCommandHelp -CommandHelp $commandHelp -OutputFolder $destinationFolder -Force

} else {
	# We are running locally, check if Microsoft.PowerShell.PlatyPS is installed
	$modules = Get-Module -Name Microsoft.PowerShell.PlatyPS -ListAvailable
	if($modules.Count -eq 0)
	{
		# Not installed
		$choices = '&Yes','&No'
		$install = $Host.UI.PromptForChoice("Install Microsoft.PowerShell.PlatyPS","We need the PowerShell Microsoft.PowerShell.PlatyPS module to generate documentation. Install this?",$choices, 1)
		if($install -eq 0)
		{
			Install-Module -Name Microsoft.PowerShell.PlatyPS -AllowPrerelease -ErrorAction Stop
		} else {
			exit
		}
	}
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path ./../documentation/*.md
	$commandHelp = $mdFiles | Where-Object { $_.FileType -match 'CommandHelp'} | Import-MarkdownCommandHelp -Path {$_.FilePath}
	Export-MamlCommandHelp -CommandHelp $commandHelp -OutputFolder $destinationFolder -Force
}	
