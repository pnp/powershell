# --platform linux/arm/v7
FROM mcr.microsoft.com/dotnet/sdk

# Install PnP.PowerShell module
SHELL ["pwsh", "-command"]
ARG PNP_VERSION
RUN Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_VERSION -Force -Scope AllUsers -AllowPrerelease -SkipPublisherCheck

ENTRYPOINT ["pwsh"]
