Docker with Linux/Mac OS/WSL:

Build documentation

```bash
docker run --rm -it -v $(pwd):/home mono:6.12.0.182 sh -c "apt update && apt upgrade -y && apt install wget unzip -y; \
    wget https://github.com/dotnet/docfx/releases/download/v2.51/docfx.zip; \
    unzip docfx.zip -d /usr/local/lib/docfx; \
    chmod +x /usr/local/lib/docfx/docfx.exe; \
    /usr/bin/mono /usr/local/lib/docfx/docfx.exe build /home/pages/docfx.json;"
```

Clean documentation

```bash
rm -rf ./pages/_site
```

Docker with Windows CMD

```bat
docker run --rm -it -v %cd%:C:\workplace mcr.microsoft.com/windows/servercore:10.0.17763.2366-amd64 powershell -c $ProgressPreference = 'SilentlyContinue'; ^
    Invoke-WebRequest https://github.com/dotnet/docfx/releases/download/v2.51/docfx.zip -OutFile $env:TEMP\docfx.zip; ^
    Expand-Archive $env:TEMP\docfx.zip $env:TEMP\docfx; ^
    Start-Process -NoNewWindow -FilePath $env:TEMP\docfx\docfx.exe -ArgumentList build, C:\workplace\pages\docfx.json -Wait
```

Docker with Windows PowerShell

```powershell
docker run --rm -it -v ${pwd}:C:\workplace mcr.microsoft.com/windows/servercore:10.0.17763.2366-amd64 powershell -c "`$ProgressPreference = 'SilentlyContinue'; `
    Invoke-WebRequest https://github.com/dotnet/docfx/releases/download/v2.51/docfx.zip -OutFile `$env:TEMP\docfx.zip; `
    Expand-Archive `$env:TEMP\docfx.zip `$env:TEMP\docfx; `
    Start-Process -NoNewWindow -FilePath `$env:TEMP\docfx\docfx.exe -ArgumentList build, C:\workplace\pages\docfx.json -Wait"
```
