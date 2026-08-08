#Requires -PSEdition Core
<#
.SYNOPSIS
Regenerates the property tables in the extract and apply configuration articles.

.DESCRIPTION
The tables in pages/articles/extract-configuration.md and pages/articles/apply-configuration.md are
generated from PnP.Framework's ExtractConfiguration and ApplyConfiguration model classes, reading the
JSON property names off their JsonPropertyName attributes, so that the articles follow the code rather
than being kept in step by hand.

Only the region between the BEGIN GENERATED PROPERTIES and END GENERATED PROPERTIES markers is
replaced. Everything else in the articles is written by hand and is left alone.

Run this after changing the configuration model, or after taking a new PnP.Framework, and commit the
result. The module has to be built and importable first, see Build-Debug.ps1.

.EXAMPLE
./build/Generate-ConfigurationDocs.ps1

.EXAMPLE
./build/Generate-ConfigurationDocs.ps1 -WhatIf
Reports whether either article is out of date without writing to it.
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(
    # Folder holding the articles. Defaults to pages/articles next to this script.
    [Parameter(Mandatory = $false)]
    [string]$ArticlesPath = (Join-Path $PSScriptRoot '../pages/articles')
)

$ErrorActionPreference = 'Stop'

$beginMarker = '<!-- BEGIN GENERATED PROPERTIES -->'
$endMarker = '<!-- END GENERATED PROPERTIES -->'
$configurationNamespace = 'PnP.Framework.Provisioning.Model.Configuration'

Import-Module PnP.PowerShell -DisableNameChecking -Force
# Touching a cmdlet makes the module load its private dependencies, PnP.Framework among them.
Get-Command Get-PnPSiteTemplate | Out-Null
$framework = [AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.GetName().Name -eq 'PnP.Framework' } | Select-Object -First 1
if (-not $framework) {
    throw 'PnP.Framework is not loaded. Build the module with build/Build-Debug.ps1 first.'
}

function Get-JsonPropertyName($property) {
    $attribute = $property.GetCustomAttributes($true) | Where-Object { $_.GetType().Name -eq 'JsonPropertyNameAttribute' } | Select-Object -First 1
    if ($attribute) { return $attribute.Name }
    return $null
}

function Test-JsonIgnored($property) {
    return [bool]($property.GetCustomAttributes($true) | Where-Object { $_.GetType().Name -eq 'JsonIgnoreAttribute' })
}

function Format-TypeText($type, [ref]$nestedType, $enumTypes) {
    $nestedType.Value = $null

    if ($type.IsGenericType) {
        $definition = $type.GetGenericTypeDefinition().Name
        $argument = $type.GetGenericArguments()[0]
        if ($definition -like 'List``1*') {
            if ($argument.IsEnum) {
                $enumTypes[$argument.Name] = $argument
                return "array of strings, see [$($argument.Name) values](#$($argument.Name.ToLower())-values)"
            }
            if ($argument.FullName -like "$configurationNamespace*") {
                $nestedType.Value = $argument
                return 'array of objects'
            }
            if ($argument.Name -eq 'String') { return 'array of strings' }
            return "array of ``$($argument.Name)`` objects"
        }
        if ($definition -like 'Dictionary``2*') { return 'object with free-form string keys and string values' }
    }

    if ($type.IsEnum) { return (($type.GetEnumNames() | ForEach-Object { "``$_``" }) -join ' \| ') }

    switch ($type.Name) {
        'Boolean' { return 'boolean' }
        'String' { return 'string' }
        'Int32' { return 'integer' }
        'Guid' { return 'string (GUID)' }
    }

    if ($type.FullName -like "$configurationNamespace*") {
        $nestedType.Value = $type
        return 'object'
    }
    return $type.Name
}

function Get-DefaultText($instance, $property) {
    if (-not $instance) { return '' }
    try { $value = $property.GetValue($instance) } catch { return '' }
    if ($null -eq $value) { return '' }
    if ($value -is [bool]) { return "``$($value.ToString().ToLower())``" }
    if ($value -is [int]) { return "``$value``" }
    if ($value -is [string]) {
        if ($value -eq '') { return '' }
        return "``$value``"
    }
    if ($value.GetType().IsEnum) { return "``$value``" }
    return ''
}

function Get-PropertyTables($rootTypeName) {
    $rootType = $framework.GetType($rootTypeName)
    if (-not $rootType) { throw "Type not found: $rootTypeName" }

    $builder = [System.Text.StringBuilder]::new()
    $emitted = [System.Collections.Generic.HashSet[string]]::new()
    $pending = [System.Collections.Generic.List[object]]::new()
    $enumTypes = @{}

    $pending.Add([pscustomobject]@{ Type = $rootType; Path = '$' })

    while ($pending.Count -gt 0) {
        $current = $pending[0]
        $pending.RemoveAt(0)
        $type = $current.Type

        if (-not $emitted.Add($type.FullName)) { continue }

        $instance = $null
        try { $instance = [Activator]::CreateInstance($type) } catch { }

        $rows = @()
        foreach ($property in ($type.GetProperties('Instance,Public') | Sort-Object Name)) {
            if (Test-JsonIgnored $property) { continue }
            $jsonName = Get-JsonPropertyName $property
            if (-not $jsonName) { continue }

            $nestedType = $null
            $typeText = Format-TypeText $property.PropertyType ([ref]$nestedType) $enumTypes
            $rows += [pscustomobject]@{ Name = $jsonName; Type = $typeText; Default = (Get-DefaultText $instance $property) }
            if ($nestedType) { $pending.Add([pscustomobject]@{ Type = $nestedType; Path = "$($current.Path).$jsonName" }) }
        }
        if ($rows.Count -eq 0) { continue }

        [void]$builder.AppendLine("### ``$($current.Path)``")
        [void]$builder.AppendLine()
        [void]$builder.AppendLine('| Property | Type | Default |')
        [void]$builder.AppendLine('| -------- | ---- | ------- |')
        foreach ($row in $rows) {
            [void]$builder.AppendLine("| ``$($row.Name)`` | $($row.Type) | $($row.Default) |")
        }
        [void]$builder.AppendLine()
    }

    foreach ($name in ($enumTypes.Keys | Sort-Object)) {
        [void]$builder.AppendLine("### $name values")
        [void]$builder.AppendLine()
        [void]$builder.AppendLine('These names are case sensitive.')
        [void]$builder.AppendLine()
        foreach ($value in $enumTypes[$name].GetEnumNames()) { [void]$builder.AppendLine("- ``$value``") }
        [void]$builder.AppendLine()
    }

    return $builder.ToString().TrimEnd()
}

$articles = @(
    @{ File = 'extract-configuration.md'; Type = "$configurationNamespace.ExtractConfiguration" }
    @{ File = 'apply-configuration.md'; Type = "$configurationNamespace.ApplyConfiguration" }
)

$outOfDate = 0
foreach ($article in $articles) {
    $path = Join-Path $ArticlesPath $article.File
    if (-not (Test-Path $path)) { throw "Article not found: $path" }

    $content = Get-Content -LiteralPath $path -Raw
    $beginIndex = $content.IndexOf($beginMarker)
    $endIndex = $content.IndexOf($endMarker)
    if ($beginIndex -lt 0 -or $endIndex -lt $beginIndex) {
        throw "$($article.File) does not hold the generated properties markers."
    }

    # AppendLine writes the newline of whichever platform this runs on, so the generated tables are put
    # onto the newline the article already uses. Without this the whole region is rewritten, and shows up
    # as a diff, purely because the generator moved between Windows and Linux.
    $newline = if ($content.Contains("`r`n")) { "`r`n" } else { "`n" }
    $tables = (Get-PropertyTables $article.Type) -replace "`r`n", "`n"
    if ($newline -eq "`r`n") { $tables = $tables -replace "`n", "`r`n" }

    $newContent = $content.Substring(0, $beginIndex + $beginMarker.Length) +
        $newline + $newline + $tables + $newline + $newline +
        $content.Substring($endIndex)

    if ($newContent -eq $content) {
        Write-Host "$($article.File) is up to date" -ForegroundColor Green
        continue
    }

    $outOfDate++
    if ($PSCmdlet.ShouldProcess($path, 'Update generated properties')) {
        [System.IO.File]::WriteAllText($path, $newContent)
        Write-Host "Updated $($article.File)" -ForegroundColor Yellow
    }
}

if ($outOfDate -gt 0 -and $WhatIfPreference) {
    Write-Host "$outOfDate article(s) are out of date" -ForegroundColor Yellow
}
