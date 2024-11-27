$documentsFolder = [environment]::getfolderpath("mydocuments")
if($IsLinux -or $isMacOS)
{
	$destinationFolder = "$HOME/.local/share/powershell/Modules"
} else {
	$destinationFolder = "$documentsFolder/PowerShell/Modules"
}

$tempFolder = [System.IO.Path]::GetTempPath()

$runsInAction = $("$env:RUNSINACTION")
if($runsInAction -ne [String]::Empty)
{
	# We are running in a GitHub Action
	Write-Host "Installing PlatyPS"
	Set-PSRepository PSGallery -InstallationPolicy Trusted
	Install-Module -Name Microsoft.PowerShell.PlatyPS -AllowPrerelease -RequiredVersion 1.0.0-preview1
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path ./documentation/*.md
	$mdFiles | Import-MarkdownCommandHelp -Path {$_.FilePath} | Export-MamlCommandHelp -OutputFolder $tempFolder -Force
} else {
	# We are running locally, check if platyps is installed
	$modules = Get-Module -Name Microsoft.PowerShell.PlatyPS -ListAvailable
	if($modules.Count -eq 0)
	{
		# Not installed
		$choices = '&Yes','&No'
		$install = $Host.UI.PromptForChoice("Install Microsoft.PowerShell.PlatyPS","We need the PowerShell Microsoft.PowerShell.PlatyPS module to generate documentation. Install this?",$choices, 1)
		if($install -eq 0)
		{
			Install-Module -Name Microsoft.PowerShell.PlatyPS -AllowPrerelease -RequiredVersion 1.0.0-preview1 -ErrorAction Stop
		} else {
			exit
		}
	}
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path ./../documentation/*.md
	$mdFiles | Import-MarkdownCommandHelp -Path {$_.FilePath} | Export-MamlCommandHelp -OutputFolder $destinationFolder -Force
}	
