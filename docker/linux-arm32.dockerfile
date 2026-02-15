# Build for linux/arm/v7
# Example:
#   docker buildx build --platform linux/arm/v7 -t yourorg/powershell-arm32:latest .

FROM arm32v7/debian:bookworm-slim

# ---- Base deps + ICU (needed for .NET/PowerShell on Linux) ----
RUN set -eux; \
    apt-get update; \
    DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
        ca-certificates \
        curl \
        tar \
        libicu-dev \
    && update-ca-certificates \
    && rm -rf /var/lib/apt/lists/*

# ---- Install PowerShell 7.4.2 (ARM32) ----
ARG PWSH_VERSION=7.4.2
RUN set -eux; \
    curl -sSL -o /tmp/pwsh.tar.gz \
      "https://github.com/PowerShell/PowerShell/releases/download/v${PWSH_VERSION}/powershell-${PWSH_VERSION}-linux-arm32.tar.gz"; \
    mkdir -p /opt/microsoft/powershell/7; \
    tar -xzf /tmp/pwsh.tar.gz -C /opt/microsoft/powershell/7; \
    ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh; \
    rm -f /tmp/pwsh.tar.gz

# ---- PnP.PowerShell version to install at first run ----
ARG PNP_VERSION=3.0.0
ENV PNP_VERSION=${PNP_VERSION}

# ---- Default timezone and certs (optional) ----
ENV TZ=Etc/UTC

# ---- Single-file ENTRYPOINT: install PnP.PowerShell at container start (once), then exec pwsh ----
# We use /bin/sh -c so we can 'exec' into pwsh with any user-provided args ($@).
ENTRYPOINT ["/bin/sh", "-c", "\
  set -eu; \
  # First-run module installation under real ARM32 (not QEMU) \
  pwsh -NoLogo -NoProfile -Command \"\
    \$m = Get-Module -ListAvailable PnP.PowerShell -ErrorAction SilentlyContinue; \
    if (-not \$m) { \
      Install-Module PnP.PowerShell -Scope AllUsers -Force -AllowPrerelease -SkipPublisherCheck -RequiredVersion \$env:PNP_VERSION; \
      Write-Host 'PnP.PowerShell installed.'; \
    } else { \
      Write-Host 'PnP.PowerShell already present.'; \
    }\"; \
  exec pwsh -NoLogo \"$@\" \
", "--"]

# Default: interactive shell
CMD []
