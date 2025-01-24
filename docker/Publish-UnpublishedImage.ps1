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
    $DOCKER_ORG,
    [Parameter(Position = 3,
        Mandatory = $true,
        ValueFromPipeline = $false)]
    [String]
    $DOCKER_IMAGE_NAME,
    [Parameter(Position = 4,
        Mandatory = $true,
        ValueFromPipeline = $false)]
    [Security.SecureString]
    $DOCKER_PASSWORD,
    [Parameter(Position = 5,
        Mandatory = $false,
        ValueFromPipeline = $false)]
    [String]
    $DOCKER_INSTALL_USER = "ContainerAdministrator",
    [Parameter(Position = 6,
        Mandatory = $false,
        ValueFromPipeline = $false)]
    [bool]
    $SKIP_PUBLISHER_CHECK = $false,
    [Parameter(Position = 7,
        Mandatory = $false,
        ValueFromPipeline = $false)]
    [String]
    $DOCKER_IMAGE_SUFFIX_ARRAY = "nanoserver-ltsc2022"
)
Write-Host "Checking for PnP PowerShell docker images... " -NoNewLine
$publishedImageVersions = (Invoke-RestMethod https://registry.hub.docker.com/v2/repositories/$DOCKER_ORG/$DOCKER_IMAGE_NAME/tags?page_size=10240).results | % {
    $_.name
}
Write-Host "$($publishedImageVersions.Length) found"

Write-Host "Checking for PnP PowerShell versions... " -NoNewLine
$moduleVersions = @(Find-Module $PS_MODULE_NAME -AllVersions)
Write-Host "$($moduleVersions.Length) found"

[array]::Reverse($moduleVersions)
$moduleVersions | % {
    $moduleVersion = $_.Version
    
    Write-Host "Validating docker image for PnP PowerShell version $moduleVersion"
    
    $DOCKER_IMAGE_SUFFIX_ARRAY.Split( "," ) | % {
        $baseImageSuffix = $_
        $imageVersion = "$moduleVersion-$baseImageSuffix"
        
        Write-Host "- Validating build $imageVersion... " -NoNewLine
        
        if (!($publishedImageVersions -contains $imageVersion))
        {
            Write-Host "not found, building new docker image... " -NoNewLine
            
            docker build --build-arg "PNP_MODULE_VERSION=$moduleVersion" --build-arg "BASE_IMAGE_SUFFIX=$baseImageSuffix" --build-arg "INSTALL_USER=$DOCKER_INSTALL_USER" --build-arg "SKIP_PUBLISHER_CHECK=$SKIP_PUBLISHER_CHECK" ./docker -f ./docker/pnppowershell.dockerFile --tag $DOCKER_ORG/$DOCKER_IMAGE_NAME`:$imageVersion;
            $plainStringPassword = [System.Net.NetworkCredential]::new("", $DOCKER_PASSWORD).Password;
            docker login -u $DOCKER_USERNAME -p "$plainStringPassword";
            docker push $DOCKER_ORG/$DOCKER_IMAGE_NAME`:$imageVersion;
            if ($baseImageSuffix -eq "alpine-3.20")
            {
                Write-Host "assigning latest tag... " -NoNewLine
                
                docker image tag $DOCKER_ORG/$DOCKER_IMAGE_NAME`:$imageVersion $DOCKER_ORG/$DOCKER_IMAGE_NAME`:latest;
                docker push $DOCKER_ORG/$DOCKER_IMAGE_NAME`:latest;
            }

            Write-Host "done"
        }
        else
        {
            Write-Host "found, skipping"
        }
    }
}
