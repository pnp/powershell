# Script used to build and sign PnP.Core packages
param(
	[ValidateSet('version.debug', 'version.release')]
	[string]$VersionFile = 'version.debug'
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 2.0

if ($VersionFile -eq 'version.release') {
	$incrementFilePath = "./pnpcore/build/version.release.increment"
	$versionIncrement = Get-Content $incrementFilePath -Raw
	$versionIncrement = $versionIncrement -as [int]
	$versionIncrement++

	$version = Get-Content ./pnpcore/build/version.release -Raw

	$version = $version.Replace("{minorrelease}", $versionIncrement)
} else {
	$incrementFilePath = "./pnpcore/build/version.debug.increment"
	$versionIncrement = Get-Content $incrementFilePath -Raw
	$versionIncrement = $versionIncrement -as [int]
	$versionIncrement++

	$version = Get-Content ./pnpcore/build/version.debug -Raw

	$version = $version.Replace("{incremental}", $versionIncrement)
}

Set-Content -Path $incrementFilePath -Value $versionIncrement

$projects = @(
	@{ Name = "PnP.Core"; ProjectPath = "./pnpcore/src/sdk/PnP.Core/PnP.Core.csproj"; OutputRoot = "pnpcore/src/sdk/PnP.Core/bin/Release"; DllName = "PnP.Core.dll" },
	@{ Name = "PnP.Core.Auth"; ProjectPath = "./pnpcore/src/sdk/PnP.Core.Auth/PnP.Core.Auth.csproj"; OutputRoot = "pnpcore/src/sdk/PnP.Core.Auth/bin/Release"; DllName = "PnP.Core.Auth.dll" },
	@{ Name = "PnP.Core.Admin"; ProjectPath = "./pnpcore/src/sdk/PnP.Core.Admin/PnP.Core.Admin.csproj"; OutputRoot = "pnpcore/src/sdk/PnP.Core.Admin/bin/Release"; DllName = "PnP.Core.Admin.dll" }
)

Write-Host "1. Building PnP.Core packages version $version"
foreach ($project in $projects) {
	Write-Host "Building $($project.Name) version $version"
	dotnet build $project.ProjectPath --configuration Release --no-incremental --force /p:Version=$version

	if ($LASTEXITCODE -ne 0) {
		throw "dotnet build failed for $($project.ProjectPath)"
	}
}

Write-Host "2. Signing PnP.Core assemblies"
$coreDlls = @($projects | ForEach-Object {
	$project = $_
	if (!(Test-Path -LiteralPath $project.OutputRoot)) {
		throw "Output folder was not found for $($project.Name): $($project.OutputRoot)"
	}

	$projectDlls = @(Get-ChildItem -LiteralPath $project.OutputRoot -Directory | ForEach-Object {
		$dllPath = Join-Path $_.FullName $project.DllName
		if (Test-Path -LiteralPath $dllPath) {
			Get-Item -LiteralPath $dllPath
		}
	})

	if ($projectDlls.Count -eq 0) {
		throw "No $($project.DllName) files were found to sign."
	}

	$projectDlls
})

if ($coreDlls.Count -eq 0) {
	throw "No PnP.Core DLL files were found to sign."
}

foreach ($coreDll in $coreDlls) {
	Write-Host "Signing $($coreDll.FullName)"

	& $env:SIGN_CLI_PATH code azure-key-vault $coreDll.FullName `
		--publisher-name "Microsoft 365 Patterns and Practices" `
		--description "PnP Core SDK" `
		--description-url "https://pnp.github.io/pnpcore/" `
		--azure-key-vault-tenant-id $env:SIGNING_TENANTID `
		--azure-key-vault-client-id $env:SIGNING_CLIENT_ID `
		--azure-key-vault-certificate $env:SIGNING_CERTNAME `
		--azure-key-vault-url $env:SIGNING_VAULTURL `
		--timestamp-url "http://timestamp.digicert.com" `
		--verbosity Debug

	if ($LASTEXITCODE -ne 0) {
		throw "Signing failed for $($coreDll.FullName)"
	}
}

Write-Host "3. Packing PnP.Core packages version $version"
$packageOutput = Join-Path $env:GITHUB_WORKSPACE "output/packages"
New-Item -Path $packageOutput -ItemType Directory -Force | Out-Null

foreach ($project in $projects) {
	dotnet pack $project.ProjectPath --configuration Release --no-build --no-restore /p:PackageVersion=$version --output $packageOutput

	if ($LASTEXITCODE -ne 0) {
		throw "dotnet pack failed for $($project.ProjectPath)"
	}
}

Write-Host "4. Creating zip file with PnP.Core packages"
$zipRoot = Join-Path $env:GITHUB_WORKSPACE "output/zips"
$zipPath = Join-Path $zipRoot "PnP.Core-packages.zip"

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

Write-Host "5. Copying package to PnP.Core repo"
$corePackageDir = "./pnpcore/build/package"
New-Item -Path $corePackageDir -ItemType Directory -Force | Out-Null
Copy-Item -Path $zipPath -Destination (Join-Path $corePackageDir "PnP.Core-packages.zip") -Force
Write-Host "Copied package to $corePackageDir"
