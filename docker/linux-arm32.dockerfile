FROM arm32v7/debian:bookworm-slim

# ---- Base deps + ICU (needed for .NET/PowerShell on Linux) ----
RUN set -eux; \
    apt-get update; \
    DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
        ca-certificates curl tar libicu-dev \
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
ARG PNP_VERSION=3.1.306-nightly
ENV PNP_VERSION=${PNP_VERSION} TZ=Etc/UTC

# ---- Write entrypoint script via heredoc (no semicolon/backslash after <<'SH') ----
RUN set -eux; \
    install -d -m 0755 /usr/local/bin; \
    cat > /usr/local/bin/docker-entrypoint.sh <<'SH'
#!/bin/sh
set -eu

# First-run install on real ARM32 (avoid QEMU build-time crashes)
pwsh -NoLogo -NoProfile -Command "
  $m = Get-Module -ListAvailable PnP.PowerShell -ErrorAction SilentlyContinue;
  if (-not $m) {
    Install-Module PnP.PowerShell -Scope AllUsers -Force -AllowPrerelease -SkipPublisherCheck -RequiredVersion $env:PNP_VERSION;
    Write-Host 'PnP.PowerShell installed.';
  } else {
    Write-Host 'PnP.PowerShell already present.';
  }"
# Hand off to PowerShell; forward all container args
exec pwsh -NoLogo "$@"
SH

# mark executable in a separate RUN (simplest, avoids heredoc chaining quirks)
RUN chmod +x /usr/local/bin/docker-entrypoint.sh

# Use exec-form ENTRYPOINT so signals are delivered to pwsh
ENTRYPOINT ["/usr/local/bin/docker-entrypoint.sh"]
CMD []
