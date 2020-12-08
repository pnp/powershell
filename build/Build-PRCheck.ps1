#
# DO NOT MODIFY THIS FILE. 
# WE WILL NOT ACCEPT PRs MODIFYING THIS FILE>
#

Write-Host "Cloning PnP.Framework" -ForegroundColor Yellow

git clone https://github.com/pnp/pnpframework --depth 1 "$PSScriptRoot/../pnpframework"

$version = "$($versionObject.Major).$($versionObject.Minor).$buildVersion"

Write-Host "Building PnP.PowerShell version $version-debug" -ForegroundColor Yellow

$buildCmd = "dotnet build `"$PSScriptRoot/../src/Commands/PnP.PowerShell.csproj`"" + "--nologo --configuration Debug -p:VersionPrefix=`"0.0.1`" -p:VersionSuffix=prcheck -p:LocalPnPFramework=true";
buildCmd += " -p:LocalPnPFramework=true"

Write-Host "Executing $buildCmd" -ForegroundColor Yellow

Invoke-Expression $buildCmd

