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
VERSION=$(cat ./version.txt)
docker build --build-arg "PNP_MODULE_VERSION=$VERSION" ./docker -f ./docker/pnppowershell-prerelease.dockerFile --tag asapozhkov/pnp-powershell:$VERSION
docker login -u asapozhkov -p $DOCKER_TOKEN
docker push asapozhkov/pnp-powershell:$VERSION
```

# Publish automatically with Github Actions

Set "DOCKER_TOKEN" variable in Github Actions Secrets
