# --platform linux/amd64
FROM debian:bullseye-slim

# Define build argument for the module version
ARG PNP_VERSION=""
ENV PNP_VERSION=${PNP_VERSION}

# Install dependencies
RUN apt-get update && apt-get install -y curl libicu67 libssl1.1 libunwind8

# Download and install PowerShell
RUN curl -L -o powershell.tar.gz https://github.com/PowerShell/PowerShell/releases/download/v7.5.2/powershell-7.5.2-linux-x64.tar.gz \
    && mkdir -p /opt/microsoft/powershell/7 \
    && tar -xvf powershell.tar.gz -C /opt/microsoft/powershell/7 \
    && rm powershell.tar.gz \
    && chmod +x /opt/microsoft/powershell/7/pwsh \
    && ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh

# Install PnP.PowerShell module
RUN pwsh -Command "Install-Module -Name PnP.PowerShell -RequiredVersion `"$env:PNP_VERSION`" -Force -Scope AllUsers -AllowPrerelease -SkipPublisherCheck"

ENTRYPOINT ["pwsh"]
