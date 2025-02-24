ARG BASE_IMAGE_SUFFIX=alpine-3.20
ARG BASE_IMAGE=mcr.microsoft.com/powershell:$BASE_IMAGE_SUFFIX
FROM $BASE_IMAGE

RUN if [[ "$BASE_IMAGE_SUFFIX" = "ubuntu-20.04-arm32" ]] ; then echo Installing libc6; apt-get update && apt-get -y install libc6; else echo Skipping libc6 installation; fi

SHELL ["pwsh", "-command"]
ARG INSTALL_USER=ContainerAdministrator
USER $INSTALL_USER
ARG PNP_MODULE_VERSION
ARG SKIP_PUBLISHER_CHECK=TRUE
RUN if ( $env:SKIP_PUBLISHER_CHECK -eq $true ) { \
        Write-Host "SKIP_PUBLISHER_CHECK"; \
        Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_MODULE_VERSION -Force -AllowPrerelease -Scope AllUsers -SkipPublisherCheck; \
    } else { \
        Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_MODULE_VERSION -Force -AllowPrerelease -Scope AllUsers; \
    }
