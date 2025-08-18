FROM mcr.microsoft.com/windows/nanoserver:ltsc2025

ENV POWERSHELL_VERSION=7.5.2

# Download and install PowerShell 7
RUN powershell -Command `
    Invoke-WebRequest -Uri "https://github.com/PowerShell/PowerShell/releases/download/v$env:POWERSHELL_VERSION/PowerShell-$env:POWERSHELL_VERSION-win-x64.zip" -OutFile "pwsh.zip"; `
    Expand-Archive -Path "pwsh.zip" -DestinationPath "C:\pwsh"; `
    Remove-Item -Path "pwsh.zip"

# Add PowerShell to PATH
ENV PATH="C:\\pwsh;${PATH}"

# Install PnP PowerShell module
RUN pwsh -Command "Install-Module -Name PnP.PowerShell -AlowPrerelease -SkipPublisherCheck -Force -Scope AllUsers"

# Start PowerShell
CMD ["pwsh"]

