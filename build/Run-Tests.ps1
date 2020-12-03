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
    $Blame,
    [Parameter(Mandatory = $false,
        ValueFromPipeline = $false)]
    [switch]
    $LocalPnPFramework
)

$env:PnPTests_CredentialManagerLabel = $CredentialManagerLabel
$env:PnPTests_SiteUrl = $SiteUrl
$env:PnPTests_Username = $Username
$currentTelemetrySetting = $env:PNPPOWERSHELL_DISABLETELEMETRY
$env:PNPPOWERSHELL_DISABLETELEMETRY = $true
if ($null -ne $Password) {
    $env:PnPTests_Password = $Password | ConvertFrom-SecureString
}

$testCmd = "dotnet test `"$PSScriptRoot/../src/Tests/PnP.PowerShell.Tests.csproj`""
if($Blame)
{
    $testCmd += " --blame" 
}
if($LocalPnPFramework)
{
    $testCmd += " -p:LocalPnPFramework=true"
}
Write-Host "Executing $testCmd" -ForegroundColor Yellow

Invoke-Expression $testCmd

$env:PnPTests_CredentialManagerLabel = $null
$env:PnPTests_SiteUrl = $null
$env:PnPTests_Username = $null
$env:PnPTests_Password = $null
$env:PNPPOWERSHELL_DISABLETELEMETRY = $currentTelemetrySetting