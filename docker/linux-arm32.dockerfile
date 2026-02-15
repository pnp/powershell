FROM arm32v7/debian:bookworm-slim

# ---- Basis: tools + ICU (vereist voor .NET/PowerShell) ----
RUN set -eux; \
    apt-get update; \
    DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
        ca-certificates \
        curl \
        tar \
        libicu-dev \
    ; \
    rm -rf /var/lib/apt/lists/*

# ---- PowerShell 7.4.2 (arm32) installeren ----
# Zie PowerShell releases: gebruik de arm32 tarball
# https://github.com/PowerShell/PowerShell/releases
ARG PWSH_VERSION=7.4.2
RUN set -eux; \
    curl -sSL -o /tmp/pwsh-linux-arm32.tar.gz \
      "https://github.com/PowerShell/PowerShell/releases/download/v${PWSH_VERSION}/powershell-${PWSH_VERSION}-linux-arm32.tar.gz"; \
    mkdir -p /opt/microsoft/powershell/7; \
    tar -xzf /tmp/pwsh-linux-arm32.tar.gz -C /opt/microsoft/powershell/7; \
    ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh; \
    rm -f /tmp/pwsh-linux-arm32.tar.gz

# ---- Optioneel: TZ en certificaten netjes zetten ----
ENV TZ=Etc/UTC
RUN set -eux; \
    update-ca-certificates

# ---- PnP.PowerShell installeren ----
# PnP.PowerShell 3.x vereist PowerShell 7.4+ (.NET 8) – dat dekken we met 7.4.2
# https://github.com/pnp/powershell (README) en installatiepagina
ARG PNP_VERSION=3.0.0
RUN set -eux; \
    pwsh -NoLogo -NoProfile -Command \
      "Install-Module -Name PnP.PowerShell -RequiredVersion ${env:PNP_VERSION} -Force -Scope AllUsers -AllowPrerelease -SkipPublisherCheck"

# ---- Default shell ----
CMD ["pwsh", "-NoLogo"]
