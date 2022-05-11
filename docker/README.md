# Publish manually in Windows

1. Set "DOCKER_USERNAME" and "DOCKER_PASSWORD" variables

2. Run

```powershell
$securedPassword = ConvertTo-SecureString $DOCKER_PASSWORD -AsPlainText -Force
./docker/Publish-UnpublishedImage.ps1 PnP.PowerShell $DOCKER_USERNAME powershell $securedPassword $true "nanoserver-1809,nanoserver-20h2,nanoserver-ltsc2022"
```

# Publish manually in Linux

1. Set "DOCKER_USERNAME" and "DOCKER_PASSWORD" variables

2. Run

```powershell
$securedPassword = ConvertTo-SecureString $DOCKER_PASSWORD -AsPlainText -Force
./docker/Publish-UnpublishedImage.ps1 PnP.PowerShell $DOCKER_USERNAME powershell $securedPassword $false "alpine-3.14,arm32v7-ubuntu-bionic"
```

# Publish with prereleases manually

1. Set "DOCKER_USERNAME" and "DOCKER_PASSWORD" variables

2. Run

```bash
VERSION=$(cat ./version.txt)-nightly
docker build --build-arg "PNP_MODULE_VERSION=$VERSION" ./docker -f ./docker/pnppowershell-prerelease.dockerFile --tag $DOCKER_USERNAME/powershell:$VERSION
docker image tag $DOCKER_USERNAME/powershell:$VERSION $DOCKER_USERNAME/powershell:nightly
docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker push $DOCKER_USERNAME/powershell:$VERSION
docker push $DOCKER_USERNAME/powershell:nightly
```

# Publish automatically with Github Actions

Set "DOCKER_USERNAME" and "DOCKER_PASSWORD" variables in Github Actions Secrets
