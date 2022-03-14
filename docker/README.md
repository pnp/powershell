# Build module in Docker in Linux

```bash
docker run --rm -it -v $(pwd):/home/powershell mcr.microsoft.com/dotnet/sdk:6.0 pwsh
```

```powershell
/home/powershell/docker/build-module-in-linux.ps1
```

# Publish with prereleases manually

1. Set "DOCKER_USERNAME" and "DOCKER_PASSWORD" variables

2. Run

```bash
VERSION=$(cat ./version.txt)-nightly
docker build --build-arg "PNP_MODULE_VERSION=$VERSION" ./docker -f ./docker/pnppowershell-prerelease.dockerFile --tag $DOCKER_USERNAME/powershell:$VERSION
docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker push $DOCKER_USERNAME/powershell:$VERSION
docker push $DOCKER_USERNAME/powershell:nightly
```

# Publish automatically with Github Actions

Set "DOCKER_USERNAME" and "DOCKER_PASSWORD" variables in Github Actions Secrets
