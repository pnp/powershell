# RPi 2 / 32-bit: build for --platform=linux/arm/v7
FROM debian:bookworm-slim

# Install prerequisites + Microsoft repo keyrings (minimal set for pwsh)
RUN apt-get update && apt-get install -y --no-install-recommends \
      ca-certificates curl tar gzip \
    && rm -rf /var/lib/apt/lists/*

# Install PowerShell 7 (arm32)
# (You can also use the official tarball method for arm32)
# See: https://learn.microsoft.com/powershell/scripting/install/powershell-on-arm
RUN set -eux; \
    curl -sSL -o /tmp/pwsh-linux-arm32.tar.gz \
      https://github.com/PowerShell/PowerShell/releases/download/v7.4.2/powershell-7.4.2-linux-arm32.tar.gz; \
    mkdir -p /opt/microsoft/powershell/7; \
    tar -xzf /tmp/pwsh-linux-arm32.tar.gz -C /opt/microsoft/powershell/7; \
    ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh; \
    rm -f /tmp/pwsh-linux-arm32.tar.gz

# Install PnP.PowerShell (requires PS 7.4+)
SHELL ["pwsh", "-command", "$ErrorActionPreference='Stop';"]
ARG PNP_VERSION
RUN Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_VERSION -Force -Scope AllUsers -AllowPrerelease -SkipPublisherCheck

ENTRYPOINT ["pwsh"]
