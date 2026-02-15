FROM arm32v7/debian:bookworm-slim

# Base deps + ICU (needed for .NET/PowerShell on Linux)
RUN set -eux; \
    apt-get update; \
    DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
        ca-certificates curl tar libicu-dev \
    && update-ca-certificates \
    && rm -rf /var/lib/apt/lists/*

# Install PowerShell 7.4.2 (ARM32)
ARG PWSH_VERSION=7.4.2
RUN set -eux; \
    curl -sSL -o /tmp/pwsh.tar.gz \
      "https://github.com/PowerShell/PowerShell/releases/download/v${PWSH_VERSION}/powershell-${PWSH_VERSION}-linux-arm32.tar.gz"; \
    mkdir -p /opt/microsoft/powershell/7; \
    tar -xzf /tmp/pwsh.tar.gz -C /opt/microsoft/powershell/7; \
    ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh; \
    rm -f /tmp/pwsh.tar.gz

# PnP version to install at first container start (on real ARM32, not QEMU)
ARG PNP_VERSION=3.0.0
ENV PNP_VERSION=${PNP_VERSION} TZ=Etc/UTC

# Create a robust entrypoint script
# - Installs PnP.PowerShell once on first run (real ARM32 hardware)
# - Then execs pwsh, forwarding any arguments
RUN set -eux; \
  install -d -m 0755 /usr/local/bin; \
  cat > /usr/local/bin/docker-entrypoint.sh <<'SH'; \
#!/bin/sh
set -eu

# First-run install on real ARM CPU only; avoid QEMU-build crashes
pwsh -NoLogo -NoProfile -Command "
  \$m = Get-Module -ListAvailable PnP.PowerShell -ErrorAction SilentlyContinue;
  if (-not \$m) {
    Install-Module PnP.PowerShell -Scope AllUsers -Force -AllowPrerelease -SkipPublisherCheck -RequiredVersion \$env:PNP_VERSION;
    Write-Host 'PnP.PowerShell installed.';
  } else {
    Write-Host 'PnP.PowerShell already present.';
  }"
# Hand off to PowerShell; forward all args
exec pwsh -NoLogo "$@"
SH
  chmod +x /usr/local/bin/docker-entrypoint.sh

# Use exec-form ENTRYPOINT; CMD can supply default args if you like
ENTRYPOINT ["/usr/local/bin/docker-entrypoint.sh"]
CMD []
