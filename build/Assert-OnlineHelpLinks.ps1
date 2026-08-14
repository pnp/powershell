<#
.SYNOPSIS
Verifies that the generated external help carries one online help link per documentation page.

.DESCRIPTION
Get-Help -Online resolves the first entry in the Related Links section of the help topic. When that
link is missing, every cmdlet falls back to the same generic link and -Online stops opening the page
for the cmdlet the user asked about. That regression is invisible at runtime, so it is asserted here
at the point where the help file is generated.
#>
param(
	[Parameter(Mandatory = $true)][string]$OutputFolder,
	[Parameter(Mandatory = $true)][string]$DocumentationPath
)

$helpFile = Join-Path $OutputFolder "PnP.PowerShell/PnP.PowerShell.dll-Help.xml"
if (!(Test-Path -LiteralPath $helpFile)) {
	throw "External help was not generated at $helpFile"
}

$expected = @(Get-ChildItem -Path (Join-Path $DocumentationPath "*.md")).Count
$actual = ([regex]::Matches((Get-Content -LiteralPath $helpFile -Raw), '<maml:linkText>Online Version</maml:linkText>\s*<maml:uri>\S+</maml:uri>')).Count

if ($actual -ne $expected) {
	throw "External help contains $actual online help links for $expected documentation pages. Get-Help -Online will not open the cmdlet page. Check that every page carries an 'online version' value and that Microsoft.PowerShell.PlatyPS 1.0.3 or later is used."
}

Write-Host "Verified $actual online help links in the generated external help" -ForegroundColor Green
