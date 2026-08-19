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

$baseUri = "https://pnp.github.io/powershell/cmdlets/"
# The documentation site publishes each page under its file name, so the page a link resolves to only
# exists when the file name, the command name and the link agree, case included.
$pageNames = @(Get-ChildItem -Path (Join-Path $DocumentationPath "*.md")).BaseName
$commands = [regex]::Matches((Get-Content -LiteralPath $helpFile -Raw), '(?s)<command:command[ >].*?</command:command>')
$problems = [System.Collections.Generic.List[string]]::new()

foreach ($command in $commands) {
	$name = [regex]::Match($command.Value, '<command:name>\s*([^<\s]+)\s*</command:name>').Groups[1].Value
	# Get-Help -Online resolves the first related link, so only the first one is worth asserting on
	$uri = [regex]::Match($command.Value, '<command:relatedLinks>\s*<maml:navigationLink>\s*<maml:linkText>[^<]*</maml:linkText>\s*<maml:uri>([^<]*)</maml:uri>').Groups[1].Value
	if ($uri -cne "$baseUri$name.html") {
		$problems.Add("  $name links to '$uri'")
	}
	elseif ($pageNames -cnotcontains $name) {
		$problems.Add("  $name links to a page that is not published: there is no documentation/$name.md with that exact casing")
	}
}

if ($commands.Count -ne $pageNames.Count) {
	$problems.Add("  external help holds $($commands.Count) commands for $($pageNames.Count) documentation pages")
}

if ($problems.Count -gt 0) {
	throw "Get-Help -Online will not open the cmdlet page. Every command must carry its own $($baseUri)<name>.html as the first related link. Check that the page carries an 'online version' value matching its file name and that Microsoft.PowerShell.PlatyPS 1.0.3 or later is used.`n$($problems -join "`n")"
}

Write-Host "Verified the online help link of all $($commands.Count) commands in the generated external help" -ForegroundColor Green
