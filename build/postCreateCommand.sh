dotnet tool update -g docfx

/usr/bin/pwsh -c 'Install-Module Microsoft.PowerShell.SecretStore,Microsoft.Powershell.SecretManagement,PlatyPS -Force'

# Execute PS1 file in repo
/usr/bin/pwsh -c '/workspaces/powershell/build/Build-Debug.ps1'