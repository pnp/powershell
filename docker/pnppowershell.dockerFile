FROM mcr.microsoft.com/powershell:7.2.2-alpine-3.14-20220318

SHELL ["pwsh", "-command"]
ARG PNP_MODULE_VERSION
RUN Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_MODULE_VERSION -Force -AllowPrerelease
