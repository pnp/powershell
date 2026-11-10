$documentsFolder = [environment]::getfolderpath("mydocuments")
if($IsLinux -or $IsMacOS)
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
	Install-Module -Name Microsoft.PowerShell.PlatyPS -RequiredVersion 1.0.3
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path ./documentation/*.md
	$mdFiles | Import-MarkdownCommandHelp -Path {$_.FilePath} | Export-MamlCommandHelp -OutputFolder $tempFolder -Force
	& "$PSScriptRoot/Assert-OnlineHelpLinks.ps1" -OutputFolder $tempFolder -DocumentationPath "$PSScriptRoot/../documentation"
} else {
	# We are running locally, check if platyps is installed. Versions before 1.0.3 do not emit the online
	# version link that Get-Help -Online resolves, so an older copy is treated as not installed.
	$modules = Get-Module -Name Microsoft.PowerShell.PlatyPS -ListAvailable | Where-Object { $_.Version -ge [Version]"1.0.3" }
	if($modules.Count -eq 0)
	{
		# Not installed
		$choices = '&Yes','&No'
		$install = $Host.UI.PromptForChoice("Install Microsoft.PowerShell.PlatyPS","We need the PowerShell Microsoft.PowerShell.PlatyPS module 1.0.3 or later to generate documentation. Install this?",$choices, 1)
		if($install -eq 0)
		{
			Install-Module -Name Microsoft.PowerShell.PlatyPS -RequiredVersion 1.0.3 -ErrorAction Stop
		} else {
			exit
		}
	}
	Write-Host "Generating external help"
	$mdFiles = Measure-PlatyPSMarkdown -Path ./../documentation/*.md
	$mdFiles | Import-MarkdownCommandHelp -Path {$_.FilePath} | Export-MamlCommandHelp -OutputFolder $destinationFolder -Force
	& "$PSScriptRoot/Assert-OnlineHelpLinks.ps1" -OutputFolder $destinationFolder -DocumentationPath "$PSScriptRoot/../documentation"
}
