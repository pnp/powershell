# Build the project
/usr/bin/pwsh -c '/workspaces/powershell/build/Build-Debug.ps1'

# Install optional modules
/usr/bin/pwsh -c 'Install-Module Microsoft.PowerShell.SecretStore,Microsoft.Powershell.SecretManagement,PlatyPS -Force'
# Add docfx package
dotnet tool install --global docfx