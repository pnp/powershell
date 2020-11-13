$modules = @(
    "PnP.PowerShell",
    "Microsoft.PowerShell.SecretManagement",
    "Microsoft.PowerShell.SecretStore"
)

Set-PSRepository -Name "PSGallery" -InstallationPolicy Trusted
$ProgressPreference = "SilentlyContinue"

foreach($module in $modules)
{
    Write-Host "Installing $module"
    Install-Module -Name $module -AllowPrerelease | Out-Null
}

Register-SecretVault -Name "SecretStore" -ModuleName "Microsoft.PowerShell.SecretStore" -DefaultVault
Set-SecretStoreConfiguration -Authentation None

$userProfile = "Import-Module -Name PnP.PowerShell"
Set-Content -Path $PROFILE.AllUsersAllHosts -Value $userProfile -Force