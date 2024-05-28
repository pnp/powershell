# Publish manually in Windows

1. Set `DOCKER_USERNAME`, `DOCKER_ORG` and `DOCKER_PASSWORD` variables

2. Run

```powershell
$securedPassword = ConvertTo-SecureString $DOCKER_PASSWORD -AsPlainText -Force
./docker/Publish-UnpublishedImage.ps1 PnP.PowerShell $DOCKER_USERNAME $DOCKER_ORG powershell $securedPassword "ContainerAdministrator" $true "nanoserver-1809"
```

# Publish manually in Linux

1. Set `DOCKER_USERNAME`, `DOCKER_ORG` and `DOCKER_PASSWORD` variables

2. Run

```powershell
$securedPassword = ConvertTo-SecureString $DOCKER_PASSWORD -AsPlainText -Force
./docker/Publish-UnpublishedImage.ps1 PnP.PowerShell $DOCKER_USERNAME $DOCKER_ORG powershell $securedPassword $false "root" "alpine-3.16.5"
```

# Publish with prereleases manually in Windows

1. Set `DOCKER_USERNAME`, `DOCKER_ORG` and `DOCKER_PASSWORD` variables

2. Run

```PowerShell
$VERSION="$(cat ./version.txt)-nightly"
docker build --build-arg "PNP_MODULE_VERSION=$VERSION" --build-arg "BASE_IMAGE_SUFFIX=nanoserver-ltsc2022" --build-arg "INSTALL_USER=ContainerAdministrator" --build-arg "SKIP_PUBLISHER_CHECK=True" ./docker -f ./docker/pnppowershell.dockerFile --tag "$DOCKER_ORG/powershell:$VERSION-nanoserver-ltsc2022";
$VERSION="$(cat ./version.txt)-nightly"
docker login -u $DOCKER_USERNAME -p "$DOCKER_PASSWORD"
docker push "$DOCKER_ORG/powershell:$VERSION-nanoserver-ltsc2022"
```

or

```PowerShell
$VERSION="$(cat ./version.txt)-nightly"
docker build --build-arg "PNP_MODULE_VERSION=$VERSION" --build-arg "BASE_IMAGE_SUFFIX=nanoserver-1809" --build-arg "INSTALL_USER=ContainerAdministrator" --build-arg "SKIP_PUBLISHER_CHECK=True" ./docker -f ./docker/pnppowershell.dockerFile --tag "$DOCKER_ORG/powershell:$VERSION-nanoserver-1809";
$VERSION="$(cat ./version.txt)-nightly"
docker login -u $DOCKER_USERNAME -p "$DOCKER_PASSWORD"
docker push "$DOCKER_ORG/powershell:$VERSION-nanoserver-1809"
```

# Publish with prereleases manually in Linux

1. Set `DOCKER_USERNAME`, `DOCKER_ORG` and `DOCKER_PASSWORD` variables

2. Run

```bash
VERSION=$(cat ./version.txt)-nightly
docker build --build-arg "PNP_MODULE_VERSION=$VERSION" --build-arg "BASE_IMAGE_SUFFIX=alpine-3.16.5" --build-arg "INSTALL_USER=root" --build-arg "SKIP_PUBLISHER_CHECK=False" ./docker -f ./docker/pnppowershell.dockerFile --tag $DOCKER_ORG/powershell:$VERSION-alpine-3.16.5;
docker image tag $DOCKER_ORG/powershell:$VERSION-alpine-3.16.5 $DOCKER_ORG/powershell:nightly
docker login -u $DOCKER_USERNAME -p "$DOCKER_PASSWORD"
docker push $DOCKER_ORG/powershell:$VERSION-alpine-3.16.5
docker push $DOCKER_ORG/powershell:nightly
```

# Publish automatically with Github Actions

Set `DOCKER_USERNAME`, `DOCKER_ORG` and `DOCKER_PASSWORD` variables in Github Actions Secrets
