Import-Module -Name platyPS

$documentsFolder = [environment]::getfolderpath("mydocuments");
if($IsLinux -or $isMacOS)
{
	$destinationFolder = "$documentsFolder/.local/share/powershell/Modules/PnP.PowerShell"
} else {
	$destinationFolder = "$documentsFolder\PowerShell\Modules\PnP.PowerShell"
}

New-ExternalHelp -Path .\Documentation -OutputPath $destinationFolder -Force