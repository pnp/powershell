# --platform linux/amd64
FROM amd64/alpine

# Install dependencies
RUN apk add --no-cache \
    ca-certificates \
    less \
    ncurses-terminfo-base \
    krb5-libs \
    libgcc \
    libintl \
    libssl3 \
    libstdc++ \
    tzdata \
    userspace-rcu \
    zlib \
    icu-libs \
    curl

RUN apk -X https://dl-cdn.alpinelinux.org/alpine/edge/main add --no-cache \
    lttng-ust \
    openssh-client 

# Download and install PowerShell
RUN curl -L -o powershell.tar.gz https://github.com/PowerShell/PowerShell/releases/download/v7.5.2/powershell-7.5.2-linux-amd64.tar.gz \
    && mkdir -p /opt/microsoft/powershell/7 \
    && tar -xvf powershell.tar.gz -C /opt/microsoft/powershell/7 \
    && rm powershell.tar.gz \
    && chmod +x /opt/microsoft/powershell/7/pwsh \
    && ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh

# Install PnP.PowerShell module
SHELL ["pwsh", "-command"]
ARG PNP_VERSION
RUN Install-Module -Name PnP.PowerShell -RequiredVersion $env:PNP_VERSION -Force -Scope AllUsers -AllowPrerelease -SkipPublisherCheck

ENTRYPOINT ["pwsh"]


