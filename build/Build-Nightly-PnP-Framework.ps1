# Script used to build and sign PnP.Framework package
$ErrorActionPreference = "Stop"
Set-StrictMode -Version 2.0

$versionIncrement = Get-Content ./pnpframework/build/version.debug.increment -Raw
$versionIncrement = $versionIncrement -as [int]
$versionIncrement++

$version = Get-Content ./pnpframework/build/version.debug -Raw

$version = $version.Replace("{incremental}", $versionIncrement)

Write-Host "1. Building PnP.Framework version $version"
dotnet build ./pnpframework/src/lib/PnP.Framework/PnP.Framework.csproj --configuration Release --no-incremental --force /p:Version=$version

# TODO: uncomment in production, for testing signing will not work on my fork
# Write-Host "2. Signing PnP.Framework assemblies"
# $frameworkOutputRoot = "pnpframework/src/lib/PnP.Framework/bin/Release"
# $frameworkDlls = @(Get-ChildItem -LiteralPath $frameworkOutputRoot -Directory | ForEach-Object {
#     $dllPath = Join-Path $_.FullName "PnP.Framework.dll"
#     if (Test-Path -LiteralPath $dllPath) {
#       Get-Item -LiteralPath $dllPath
#     }
#   })

# if ($frameworkDlls.Count -eq 0) {
#   throw "No PnP.Framework.dll files were found to sign."
# }

# foreach ($frameworkDll in $frameworkDlls) {
#   Write-Host "Signing $($frameworkDll.FullName)"

#   & $env:SIGN_CLI_PATH code azure-key-vault $frameworkDll.FullName `
#     --publisher-name "Microsoft 365 Patterns and Practices" `
#     --description "PnP Framework" `
#     --description-url "https://pnp.github.io/pnpframework/" `
#     --azure-key-vault-tenant-id $env:SIGNING_TENANTID `
#     --azure-key-vault-client-id $env:SIGNING_CLIENT_ID `
#     --azure-key-vault-certificate $env:SIGNING_CERTNAME `
#     --azure-key-vault-url $env:SIGNING_VAULTURL `
#     --timestamp-url "http://timestamp.digicert.com" `
#     --verbosity Debug

#   if ($LASTEXITCODE -ne 0) {
#     throw "Signing failed for $($frameworkDll.FullName)"
#   }
# }

Write-Host "3. Packinging PnP.Framework version $version"
$packageOutput = Join-Path $env:GITHUB_WORKSPACE "output/packages"
New-Item -Path $packageOutput -ItemType Directory -Force | Out-Null
dotnet pack ./pnpframework/src/lib/PnP.Framework/PnP.Framework.csproj --configuration Release --no-build /p:PackageVersion=$version --output $packageOutput

Write-Host "4. Creating zip file with PnP.Framework packages"
$zipRoot = Join-Path $env:GITHUB_WORKSPACE "output/zips"
$zipPath = Join-Path $zipRoot "PnP.Framework-packages.zip"

New-Item -Path $zipRoot -ItemType Directory -Force | Out-Null

if (Test-Path -LiteralPath $zipPath) {
  Remove-Item -LiteralPath $zipPath -Force
}

$packages = @(Get-ChildItem -LiteralPath $packageOutput -File | Where-Object { $_.Extension -in ".nupkg", ".snupkg" })
if ($packages.Count -eq 0) {
  throw "No NuGet packages were found to zip."
}

Compress-Archive -Path $packages.FullName -DestinationPath $zipPath -CompressionLevel Optimal
Write-Host "Created $zipPath"