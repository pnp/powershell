Param(
    [Parameter(Mandatory = $true,
        ValueFromPipeline = $false)]
    [String]
    $CredentialManagerLabel,

    [Parameter(Mandatory = $true,
        ValueFromPipeline = $false)]
    [String]
    $SiteUrl
)

# Specifies a path to one or more locations. Wildcards are permitted.

$env:PnPTests_CredentialManagerLabel = $CredentialManagerLabel;
$env:PnPTests_SiteUrl = $SiteUrl

dotnet test $PSScriptRoot/../src/Tests/PnP.PowerShell.Tests.csproj