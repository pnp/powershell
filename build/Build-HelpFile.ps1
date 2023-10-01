$documentsFolder = [environment]::getfolderpath("mydocuments")
if($IsLinux -or $isMacOS)
{
	$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
} else {
	$destinationFolder = "$documentsFolder/PowerShell/Modules/PnP.PowerShell"
}

Try {
	Write-Host "Generating documentation files for alias cmdlets" -ForegroundColor Yellow
	# Load the Module in a new PowerShell session
	$scriptBlock = {
		Write-Host "Installing latest nightly of PnP PowerShell"
  		Install-Module PnP.PowerShell -AllowPrerelease -Force
Get-ChildItem -Recurse
  		Write-Host "Retrieving PnP PowerShell alias cmdlets"
		$cmdlets = Get-Command -Module PnP.PowerShell | Where-Object CommandType -eq "Alias" | Select-Object -Property @{N="Alias";E={$_.Name}}, @{N="ReferencedCommand";E={$_.ReferencedCommand.Name}}
		$cmdlets
  		Write-Host "Retrieved alias cmdlets successfully"
	}
	$aliasCmdlets = Start-ThreadJob -ScriptBlock $scriptBlock | Receive-Job -Wait

	Write-Host "  - $($aliasCmdlets.Length) found" -ForegroundColor Yellow

	$aliasTemplatePageContent = Get-Content -Path "./pnppowershell/pages/cmdlets/alias.md" -Raw

	ForEach($aliasCmdlet in $aliasCmdlets)
	{
		$destinationFileName = "./documentation/$($aliasCmdlet.Alias).md"

		Write-Host "    - Creating page for $($aliasCmdlet.Alias) being an alias for $($aliasCmdlet.ReferencedCommand) as $destinationFileName" -ForegroundColor Yellow
		$aliasTemplatePageContent.Replace("%%cmdletname%%", $aliasCmdlet.Alias).Replace("%%referencedcmdletname%%", $aliasCmdlet.ReferencedCommand) | Out-File $destinationFileName -Force
	}
}
Catch {
	Write-Host "Error: Cannot generate alias documentation files"
	Write-Host $_
}

$tempFolder = [System.IO.Path]::GetTempPath()

$runsInAction = $("$env:RUNSINACTION")
if($runsInAction -ne [String]::Empty)
{
	# We are running in a GitHub Action
	Write-Host "Installing PlatyPS"
	Set-PSRepository PSGallery -InstallationPolicy Trusted
	Install-Module PlatyPS -ErrorAction Stop
	Write-Host "Generating external help"
	New-ExternalHelp -Path ./documentation -OutputPath $tempFolder -Force
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
