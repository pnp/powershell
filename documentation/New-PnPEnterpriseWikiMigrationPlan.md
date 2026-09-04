---
Module Name: PnP.PowerShell
title: New-PnPEnterpriseWikiMigrationPlan
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPEnterpriseWikiMigrationPlan.html
---

# New-PnPEnterpriseWikiMigrationPlan

## SYNOPSIS

**Required Permissions**

* SharePoint: Sites.Read.All (application) or AllSites.Read (delegated)

Creates a target-specific, digest-sealed Enterprise Wiki migration plan and complete review report.

## SYNTAX

```powershell
New-PnPEnterpriseWikiMigrationPlan [-ExportPath] <String> [-OutputPath <String>]
    [-TargetPageName <String>] [-AllowUniquePermissions] [-AllowManagedMetadataSubstitution]
    [-BlockExternalResources] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Reads a source-only export, probes the target web represented by `-Connection`, and writes `enterprise-wiki-package.json` plus `enterprise-wiki-report.md`. It does not modify SharePoint.

Every source field remains in the snapshot. Only recognized, writable, type-compatible fields with supported values receive `Apply`. Unrecognized fields receive `EvidenceOnly`; user, lookup, and taxonomy values receive `RequiresMapping`; every other skip has an explicit disposition and reason.

Lifecycle has no publish Boolean. Source `Level = Published` maps to `Published` when checkout and moderation evidence do not conflict. Every other or contradictory state maps conservatively to `Draft`.

The report covers all envelope, source, policy, fence, lifecycle, content, field, Web Part, dependency, security, target-probe, replacement, assertion, blocker, and warning data. Large payloads are shown by length, SHA-256, and preview while the full value remains in JSON.

Real captured examples: R11 had version `1.1`, checkout `Online`, level `Draft`, and moderation status `3`, so its new target lifecycle is `Draft`. E05 had checkout `None`, level `Published`, and moderation status `0`, so its target lifecycle is `Published`. An unknown `OOCLReference` value remains in the snapshot with `EvidenceOnly` and can be recovered by a future mapper.

## EXAMPLES

### EXAMPLE 1

```powershell
$target = Connect-PnPOnline https://contoso.sharepoint.com/sites/new -Interactive -ReturnConnection
New-PnPEnterpriseWikiMigrationPlan .\R11\enterprise-wiki-export.json -TargetPageName R11.aspx -Connection $target
```

Creates the package and report next to the export.

### EXAMPLE 2

```powershell
Export-PnPEnterpriseWikiPackage Pages/R11.aspx -OutputPath .\R11 -Connection $source |
    New-PnPEnterpriseWikiMigrationPlan -TargetPageName R11.aspx -Connection $target
```

Uses pipeline binding through `EnterpriseWikiExportResult.ExportPath`.

### EXAMPLE 3

```powershell
$plan = New-PnPEnterpriseWikiMigrationPlan .\R11\enterprise-wiki-export.json -Connection $target
$plan.Plan.FieldActions | Group-Object Disposition | Select-Object Name, Count
$plan.ReportPath
$plan.PlanDigest
```

Reviews field decisions, the report path, and the approval digest.

## PARAMETERS

### -AllowManagedMetadataSubstitution

Allows planning to continue without a reviewed taxonomy mapping. Those values remain `RequiresMapping` and are not written.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowUniquePermissions

Allows planning to continue for a source page with unique permissions. Permissions remain evidence-only.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockExternalResources

Makes external renderable resources blockers instead of preserving their original external URLs.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Optional connection to the target web. Retrieve it with `Connect-PnPOnline -ReturnConnection` or `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExportPath

Path to `enterprise-wiki-export.json` or its directory. Accepts an export result through `ExportPath`.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Path

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue, ByPropertyName)
Accept wildcard characters: False
```

### -Force

Overwrites existing local package and report files. It does not bypass blockers or overwrite target content.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputPath

Local file or directory for the migration package. By default, files are written next to the export.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetPageName

Target page filename, server-relative path, or absolute URL. The source filename is used by default.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose

Shows detailed information about the operation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: vb

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Export-PnPEnterpriseWikiPackage](Export-PnPEnterpriseWikiPackage.md)

[Import-PnPEnterpriseWikiMigrationPackage](Import-PnPEnterpriseWikiMigrationPackage.md)
