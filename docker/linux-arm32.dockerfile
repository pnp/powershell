# Doelplatform: linux/arm/v7 (arm32v7)
# Buildvoorbeeld:
#   docker buildx build --platform linux/arm/v7 -t jouworg/powershell-arm32:latest .

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
ARG PWSH_VERSION=7.4.2
RUN set -eux; \
    curl -sSL -o /tmp/pwsh-linux-arm32.tar.gz \
      "https://github.com/PowerShell/PowerShell/releases/download/v${PWSH_VERSION}/powershell-${PWSH_VERSION}-linux-arm32.tar.gz"; \
    mkdir -p /opt/microsoft/powershell/7; \
    tar -xzf /tmp/pwsh-linux-arm32.tar.gz -C /opt/microsoft/powershell/7; \
    ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh; \
    rm -f /tmp/pwsh-linux-arm32.tar.gz

# ---- Certificaten bijwerken (optioneel maar netjes) ----
ENV TZ=Etc/UTC
RUN set -eux; update-ca-certificates

# ---- PnP.PowerShell installeren ----
# 1) Build-arg -> ENV zodat PowerShell $env:PNP_VERSION ziet
ARG PNP_VERSION=3.0.0
ENV PNP_VERSION=${PNP_VERSION}

# 2) Gebruik single quotes zodat /bin/sh geen ${...} of $... expandeert
RUN set -eux; \
    pwsh -NoLogo -NoProfile -Command \
      'Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_VERSION -Force -Scope AllUsers -AllowPrerelease -SkipPublisherCheck'

# ---- Default shell ----
CMD ["pwsh", "-NoLogo"]
