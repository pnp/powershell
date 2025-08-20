FROM mcr.microsoft.com/windows/nanoserver:ltsc2025

# Define build argument for the module version
ARG PNP_VERSION=""

RUN mkdir C:\\pwsh
WORKDIR C:\\pwsh

# Download and install PowerShell 7
RUN curl.exe -L -o pwsh.zip https://github.com/PowerShell/PowerShell/releases/download/v7.5.2/PowerShell-7.5.2-win-x64.zip
RUN tar -xf pwsh.zip -C C:\pwsh
RUN del pwsh.zip

# Add PowerShell to PATH
ENV PATH="C:\\pwsh;${PATH}"

# Install PnP PowerShell module
RUN pwsh -Command "Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_VERSION -Force -AllowPrerelease -Scope CurrentUser"

# Start PowerShell
CMD ["pwsh"]





