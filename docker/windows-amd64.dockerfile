FROM mcr.microsoft.com/windows/nanoserver:ltsc2025

ENV POWERSHELL_VERSION=7.5.2

# Download and install PowerShell 7
RUN curl.exe -L -o pwsh.zip https://github.com/PowerShell/PowerShell/releases/download/v${POWERSHELL_VERSION}/PowerShell-${POWERSHELL_VERSION}-win-x64.zip
RUN mkdir C:\pwsh
RUN tar -xf pwsh.zip -C C:\pwsh
RUN del pwsh.zip

# Add PowerShell to PATH
ENV PATH="C:\\pwsh;${PATH}"

# Install PnP PowerShell module
RUN pwsh -Command "Install-Module -Name PnP.PowerShell -AlowPrerelease -SkipPublisherCheck -Force -Scope AllUsers"

# Start PowerShell
CMD ["pwsh"]
