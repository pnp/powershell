#Requires -RunAsAdministrator

Param(
    [Parameter(Mandatory = $true,
        ParameterSetName = "CredManager",
        ValueFromPipeline = $false)]
    [Parameter(Mandatory = $true,
        ParameterSetName = "Username and Password",
        ValueFromPipeline = $false)]
    [String]
    $SiteUrl,

    [Parameter(Mandatory = $true,
        ParameterSetName = "CredManager",
        ValueFromPipeline = $false)]
    [String]
    $CredentialManagerLabel,

    [Parameter(Mandatory = $true,
        ParameterSetname = "Username and Password")]
    [String]
    $Username,

    [Parameter(Mandatory = $true,
        ParameterSetName = "Username and Password")]
    [SecureString]
    $Password,

    [Parameter(Mandatory = $false,
        ParameterSetName = "CredManager",
        ValueFromPipeline = $false)]
        [Parameter(Mandatory = $false,
        ParameterSetName = "Username and Password",
        ValueFromPipeline = $false)]
    [switch]
    $Blame
)

$env:PnPTests_CredentialManagerLabel = $CredentialManagerLabel
$env:PnPTests_SiteUrl = $SiteUrl
$env:PnPTests_Username = $Username
if ($null -ne $Password) {
    $env:PnPTests_Password = $Password | ConvertFrom-SecureString
}

if($Blame -eq $true)
{
    dotnet test $PSScriptRoot/../src/Tests/PnP.PowerShell.Tests.csproj --blame
} else {
    dotnet test $PSScriptRoot/../src/Tests/PnP.PowerShell.Tests.csproj
}

$env:PnPTests_CredentialManagerLabel = $null
$env:PnPTests_SiteUrl = $null
$env:PnPTests_Username = $null
$env:PnPTests_Password = $null