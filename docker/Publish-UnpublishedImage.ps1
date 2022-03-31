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
$publishedImageVersions = docker image list $DOCKER_USERNAME/$DOCKER_IMAGE_NAME --format "{{.Tag}}"
Write-Host $publishedImageVersions.Count;
Find-Module $PS_MODULE_NAME -AllVersions