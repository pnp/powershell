# Docker with Linux/Mac OS/WSL

Use this guidance if you plan on using docker containers to work with PnP PowerShell.

## Build a Docker Image

```bash
docker build . -f ./pages/Dockerfile-Linux -t pnp.powershell-pages-build
```

## Build Documentation

```bash
docker run --rm -it -v $(pwd)/pages:/home pnp.powershell-pages-build /usr/bin/mono /usr/local/lib/docfx/docfx.exe build /home/docfx.json
```

## Clean documentation

```bash
sudo rm -rf ./pages/_site
sudo rm -rf ./pages/obj
```

# Docker with Windows CMD

## Build a Docker Image

```bash
docker build . -f ./pages/Dockerfile-Windows -t pnp.powershell-pages-build
```

## Build Documentation

```bat
docker run --rm -it -v %cd%\pages:C:\workplace pnp.powershell-pages-build powershell -c Start-Process -NoNewWindow -FilePath $env:TEMP\docfx\docfx.exe -ArgumentList build, C:\workplace\docfx.json -Wait
```

## Clean documentation

```bat
rmdir /s /q pages\_site
rmdir /s /q pages\obj
```

# Docker with Windows PowerShell

## Build a Docker Image

```bash
docker build . -f ./pages/Dockerfile-Windows -t pnp.powershell-pages-build
```

## Build Documentation

```powershell
docker run --rm -it -v ${pwd}\pages:C:\workplace pnp.powershell-pages-build powershell -c "Start-Process -NoNewWindow -FilePath `$env:TEMP\docfx\docfx.exe -ArgumentList build, C:\workplace\docfx.json -Wait"
```

## Clean documentation

```powershell
Remove-Item pages\_site -Recurse
Remove-Item pages\obj -Recurse
```