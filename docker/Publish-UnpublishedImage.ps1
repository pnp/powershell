Param(
    [Parameter(Position = 0,
    Mandatory = $true,
    ValueFromPipeline = $false)]
    [String]
    $PS_MODULE_NAME,
    [Parameter(Position = 1,
    Mandatory = $true,
    ValueFromPipeline = $false)]
    [String]
    $DOCKER_USERNAME,
    [Parameter(Position = 2,
    Mandatory = $true,
    ValueFromPipeline = $false)]
    [String]
    $DOCKER_IMAGE_NAME,
    [Parameter(Position = 3,
    Mandatory = $true,
    ValueFromPipeline = $false)]
    [Security.SecureString]
    $DOCKER_PASSWORD
)
$publishedImageVersions = (Invoke-RestMethod https://registry.hub.docker.com/v2/repositories/$DOCKER_USERNAME/$DOCKER_IMAGE_NAME/tags).results | % {
    $_.name
}
$moduleVersions = Find-Module $PS_MODULE_NAME -AllVersions;
[array]::Reverse($moduleVersions);
$moduleVersions | % {
    $moduleVersion = $_.Version;
    if ( !( $publishedImageVersions -contains $moduleVersion ) ) {
        docker build --build-arg "PNP_MODULE_VERSION=$moduleVersion" ./docker -f ./docker/pnppowershell.dockerFile --tag $DOCKER_USERNAME/$DOCKER_IMAGE_NAME`:$moduleVersion;
        docker image tag $DOCKER_USERNAME/$DOCKER_IMAGE_NAME`:$moduleVersion $DOCKER_USERNAME/$DOCKER_IMAGE_NAME`:latest;
        docker login -u $DOCKER_USERNAME -p "$([System.Net.NetworkCredential]::new("", $DOCKER_PASSWORD).Password)";
        docker push $DOCKER_USERNAME/$DOCKER_IMAGE_NAME`:$moduleVersion;
        docker push $DOCKER_USERNAME/$DOCKER_IMAGE_NAME`:latest;
    }
}