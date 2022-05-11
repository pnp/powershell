ARG BASE_IMAGE_SUFFIX=alpine-3.14
ARG BASE_IMAGE=mcr.microsoft.com/powershell:lts-7.2-$BASE_IMAGE_SUFFIX
FROM $BASE_IMAGE

SHELL ["pwsh", "-command"]
ARG SKIP_PUBLISHER_CHECK = "TRUE"
ARG PNP_MODULE_VERSION
RUN if ( $SKIP_PUBLISHER_CHECK ) { \
        Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_MODULE_VERSION -Force -AllowPrerelease -SkipPublisherCheck \
    } else { \
        Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_MODULE_VERSION -Force -AllowPrerelease \
    }
