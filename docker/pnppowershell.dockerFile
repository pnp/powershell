FROM mcr.microsoft.com/powershell:lts-debian-10
COPY ./powershell/ powershell
RUN /usr/bin/pwsh -File ./powershell/installModules.ps1 && rm -rf ./powershell