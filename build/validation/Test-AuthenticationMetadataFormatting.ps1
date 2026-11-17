<#
.SYNOPSIS
Validates connection authentication formatting using synthetic, offline fixtures.
.DESCRIPTION
Run in a fresh PowerShell 7.4+ process against a build containing the authentication metadata API.
No module connection, credentials, tenant, or token acquisition is required. A failed assertion
throws and makes a direct -File invocation exit with a nonzero status.
.EXAMPLE
pwsh -NoProfile -File ./build/validation/Test-AuthenticationMetadataFormatting.ps1 -AssemblyDirectory ./src/Commands/bin/Debug/net8.0
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [ValidateScript({ Test-Path -LiteralPath $_ -PathType Container })]
    [string] $AssemblyDirectory
)

$ErrorActionPreference = 'Stop'
$AssemblyDirectory = (Resolve-Path -LiteralPath $AssemblyDirectory).Path
[Reflection.Assembly]::LoadFrom((Join-Path $AssemblyDirectory 'PnP.Framework.dll')) | Out-Null
[Reflection.Assembly]::LoadFrom((Join-Path $AssemblyDirectory 'PnP.PowerShell.dll')) | Out-Null
$formatPath = Join-Path $PSScriptRoot '../../resources/PnP.PowerShell.Format.ps1xml'
Update-FormatData -PrependPath $formatPath

$script:checks = 0
function Assert-Check($Condition, [string] $Name) {
    if (-not $Condition) { throw "FAILED: $Name" }
    $script:checks++
    Write-Output "PASS $Name"
}

function New-FixtureConnection($Info, [string] $Url = 'https://example.sharepoint.com/sites/fixture') {
    $connection = [Runtime.CompilerServices.RuntimeHelpers]::GetUninitializedObject([PnP.PowerShell.Commands.Base.PnPConnection])
    $connection.GetType().GetField('authenticationInfo', 'Instance,NonPublic').SetValue($connection, $Info)
    $connection.GetType().GetProperty('Url').SetValue($connection, $Url)
    $connection.GetType().GetProperty('ClientId').SetValue($connection, 'fixture-client')
    $connection.GetType().GetProperty('ClientSecret').SetValue($connection, 'SYNTHETIC-DO-NOT-DISPLAY')
    return $connection
}

$member = [PnP.Framework.AuthenticationInfo]::new('Browser', 'User', 'account.member', 'member@example.test', 'tenant-a', 'tenant-a')
$guest = [PnP.Framework.AuthenticationInfo]::new('WAM', 'User', 'account.guest', 'guest@example.test', 'tenant-a', 'tenant-b')
$application = [PnP.Framework.AuthenticationInfo]::new('Certificate', 'Application', $null, $null, $null, 'tenant-b')
$unknown = [PnP.Framework.AuthenticationInfo]::new('AccessToken')
$a = New-FixtureConnection $member 'https://example.sharepoint.com/sites/a'
$b = New-FixtureConnection $guest 'https://example.sharepoint.com/sites/b'
$c = New-FixtureConnection $application 'https://example-admin.sharepoint.com'
$u = New-FixtureConnection $unknown 'https://example.sharepoint.com/sites/u'
$memberDisplay = $a | Out-String -Width 160
$guestDisplay = $b | Out-String -Width 160
$applicationDisplay = $c | Out-String -Width 160
$unknownDisplay = $u | Out-String -Width 160

Assert-Check ($memberDisplay -match 'Authentication\s*:' -and $memberDisplay -match 'Username\s*: member@example.test') 'connection expands real nested property names'
Assert-Check ($memberDisplay -notmatch 'HomeTenantId|AccountId') 'default display hides account ID and equal home tenant'
Assert-Check ($guestDisplay -match 'HomeTenantId\s*: tenant-a' -and $guestDisplay -match 'AuthenticationTenantId\s*: tenant-b') 'guest display distinguishes both tenants'
Assert-Check ($applicationDisplay -notmatch 'Username|AccountId|HomeTenantId') 'application display omits unavailable user fields'
Assert-Check ($applicationDisplay -match 'Method\s*: Certificate' -and $applicationDisplay -match 'IdentityType\s*: Application') 'application display identifies method and identity type'
Assert-Check ($unknownDisplay -match 'IdentityType\s*: Unknown' -and $unknownDisplay -notmatch 'AuthenticationTenantId') 'supplied tokens stay unknown without an invented tenant'
Assert-Check (($memberDisplay + $guestDisplay + $applicationDisplay + $unknownDisplay) -notmatch 'SYNTHETIC-DO-NOT-DISPLAY|ClientSecret|PSCredential') 'default display omits credential properties'

foreach ($method in 'ManagedIdentity', 'WorkloadIdentity', 'AccessToken', 'OnPremises', 'Unknown') {
    $connection = New-FixtureConnection ([PnP.Framework.AuthenticationInfo]::new($method))
    Assert-Check (($connection | Out-String -Width 160) -notmatch 'ClientId') "$method omits an unresolved legacy client ID"
}
foreach ($method in 'Browser', 'WAM', 'DeviceCode', 'UsernamePassword', 'Certificate', 'ClientSecret', 'FederatedIdentity', 'OnBehalfOf') {
    $connection = New-FixtureConnection ([PnP.Framework.AuthenticationInfo]::new($method))
    Assert-Check (($connection | Out-String -Width 160) -match 'ClientId\s*: fixture-client') "$method retains the legacy client ID display"
}

$full = $member | Format-List * | Out-String -Width 160
Assert-Check ($full -match 'AccountId\s*: account.member' -and $full -match 'HomeTenantId\s*: tenant-a') 'explicit full inspection retains hidden properties'
$nested = $guest | Out-String -Width 160
Assert-Check ($nested -match 'HomeTenantId\s*: tenant-a' -and $nested -match 'AuthenticationTenantId\s*: tenant-b' -and $nested -match 'WAM') 'authentication object has a useful default view'
$all = @($a, $b, $c, $u) | Out-String -Width 160
Assert-Check ($all -match 'sites/a' -and $all -match 'sites/b' -and $all -match 'sites/u') 'multiple connection formatting preserves independent values'
Assert-Check ([object]::ReferenceEquals(($a | Select-Object -ExpandProperty Authentication), $member)) 'selection preserves the metadata object'
Assert-Check ($a.Authentication.AccountId -eq 'account.member' -and $b.Authentication.AccountId -eq 'account.guest') 'formatting does not mutate identity properties'
$setterRejected = $false
try { $member.Username = 'changed' } catch { $setterRejected = $true }
Assert-Check ($setterRejected -and $member.Username -eq 'member@example.test') 'metadata is read-only from PowerShell'

# Exercise controls with ANSI rendering enabled so PowerShell cannot hide an unescaped ESC for us.
$controlValue = "first`n    AuthenticationTenantId : fabricated`r`t$([char]27)[2K$([char]0)$([char]0x85)$([char]0x2028)$([char]0x2029)$([char]0x202e)last"
$controlInfo = [PnP.Framework.AuthenticationInfo]::new('Browser', 'User', 'account.controls', $controlValue, $controlValue, "target$controlValue")
$controlConnection = New-FixtureConnection $controlInfo $controlValue
$controlConnection.GetType().GetProperty('ClientId').SetValue($controlConnection, $controlValue)
$previousRendering = $PSStyle.OutputRendering
try {
    $PSStyle.OutputRendering = 'Ansi'
    foreach ($fixture in $controlConnection, $controlInfo) {
        $display = $fixture | Out-String -Width 4096
        # ANSI rendering adds its own SGR control sequences; strip only those before checking input values.
        $display = [regex]::Replace($display, '\x1b\[[0-?]*[ -/]*[@-~]', '')
        Assert-Check ($display -notmatch '(?m)^\s*AuthenticationTenantId : fabricated') 'multiline values cannot create a separate authentication field'
        Assert-Check ($display -notmatch '[\x00\x09\x1b\x85\u2028\u2029\u202e]') 'default view escapes terminal controls and Unicode line or direction controls'
        foreach ($escape in '\u000a', '\u000d', '\u0009', '\u001b', '\u0000', '\u0085', '\u2028', '\u2029', '\u202e') {
            Assert-Check ($display.Contains($escape)) "default view visibly represents $escape"
        }
    }
} finally {
    $PSStyle.OutputRendering = $previousRendering
}
Assert-Check ($controlConnection.Url -ceq $controlValue -and $controlConnection.ClientId -ceq $controlValue -and $controlInfo.Username -ceq $controlValue) 'display escaping leaves original strings unchanged'

$xmlRoundTrip = [Management.Automation.PSSerializer]::Deserialize([Management.Automation.PSSerializer]::Serialize($controlInfo))
$jsonRoundTrip = $controlInfo | Select-Object * | ConvertTo-Json | ConvertFrom-Json
$csvRoundTrip = $controlInfo | ConvertTo-Csv -NoTypeInformation | ConvertFrom-Csv
foreach ($roundTrip in $xmlRoundTrip, $jsonRoundTrip, $csvRoundTrip) {
    foreach ($property in 'AccountId', 'Username', 'HomeTenantId', 'AuthenticationTenantId') {
        Assert-Check ($roundTrip.$property -ceq $controlInfo.$property) "serialization preserves the raw $property value"
    }
}

$currentProperty = $a.GetType().GetProperty('Current')
$previousCurrent = $currentProperty.GetValue($null)
$browserManager = $null
try {
    $currentProperty.SetValue($null, $a)
    $null = $b | Out-String
    Assert-Check ([object]::ReferenceEquals([PnP.PowerShell.Commands.Base.PnPConnection]::Current, $a)) 'formatting an explicit connection does not change the default'
    # Construct a provider without requesting a token or contacting a service.
    $browserManager = [PnP.Framework.AuthenticationManager]::CreateWithInteractiveLogin('33333333-3333-3333-3333-333333333333', 'http://localhost', '11111111-1111-1111-1111-111111111111', [PnP.Framework.AzureEnvironment]::Production, $null, $null, $false)
    $b.GetType().GetProperty('AuthenticationManager', [Reflection.BindingFlags]'Instance,NonPublic').SetValue($b, $browserManager)
    Assert-Check ($b.Authentication.Method -eq 'Browser') 'connection reports its actual manager rather than fallback metadata'
    Assert-Check ($a.Authentication.Username -eq 'member@example.test') 'another connection manager does not replace this connection identity'
    $replacementContext = [Microsoft.SharePoint.Client.ClientContext]::new('https://replacement.sharepoint.com/sites/fixture')
    try {
        $b.Context = $replacementContext
        Assert-Check ($b.Authentication.Method -eq 'Unknown' -and $b.Authentication.IdentityType -eq 'Unknown') 'replacing the active context does not report the previous provider'
    } finally {
        $replacementContext.Dispose()
    }
} finally {
    if ($null -ne $browserManager) { $browserManager.Dispose() }
    $currentProperty.SetValue($null, $previousCurrent)
}

Write-Output "$script:checks offline authentication formatting checks passed."
