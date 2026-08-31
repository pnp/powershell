---
Module Name: PnP.PowerShell
title: Export-PnPEnterpriseWikiPackage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPEnterpriseWikiPackage.html
---

# Export-PnPEnterpriseWikiPackage

## SYNOPSIS

**Required Permissions**

* SharePoint: Sites.Read.All (application) or AllSites.Read (delegated)

Exports a source-only Enterprise Wiki snapshot that can be planned later without reconnecting to the source.

## SYNTAX

### Identity

```powershell
Export-PnPEnterpriseWikiPackage [-Identity] <String> -OutputPath <String> [-NoWebParts]
    [-MaximumDependencyBytes <Int64>] [-Force] [-Connection <PnPConnection>]
```

### All

```powershell
Export-PnPEnterpriseWikiPackage -All -OutputPath <String> [-NoWebParts]
    [-MaximumDependencyBytes <Int64>] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Captures an Enterprise Wiki page without requiring or inspecting a target connection. The resulting `enterprise-wiki-export.json` contains a digest-sealed source snapshot.

The snapshot enumerates every field definition in the source Pages library. Each field receives an entry even when SharePoint did not return a value or the current importer does not understand its runtime type. Entries retain identity, title, type, full schema XML, flags, capture status, structured known values, and best-effort raw type/text/JSON evidence. A later planner can therefore recover newly supported fields without reading the source again.

Publishing HTML, shared Web Parts, dependencies, security, lifecycle evidence, and a before/after source stability fence are also captured. The returned `EnterpriseWikiExportResult.ExportPath` binds directly to `New-PnPEnterpriseWikiMigrationPlan`.

## EXAMPLES

### EXAMPLE 1

```powershell
$source = Connect-PnPOnline https://contoso.sharepoint.com/sites/legacy -Interactive -ReturnConnection
Export-PnPEnterpriseWikiPackage -Identity Pages/R11.aspx -OutputPath .\R11 -Connection $source
```

Writes `.\R11\enterprise-wiki-export.json`. No target connection is needed.

### EXAMPLE 2

```powershell
Export-PnPEnterpriseWikiPackage -All -OutputPath .\wiki-export -Connection $source
```

Exports every Enterprise Wiki Page into a separate numbered directory.

### EXAMPLE 3

```powershell
$export = Export-PnPEnterpriseWikiPackage Pages/R11.aspx -OutputPath .\R11 -Connection $source
$export.Snapshot.Fields | Select-Object InternalName, TypeAsString, CaptureStatus, HasValue, Kind, RawType
```

Reviews the complete source field inventory.

## PARAMETERS

### -All

Exports all pages whose content type derives from Enterprise Wiki Page. Project Page is excluded.

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Optional connection to the source web. Retrieve it with `Connect-PnPOnline -ReturnConnection` or `Get-PnPConnection`.

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

### -Force

Overwrites an existing local export file. It does not change migration eligibility or overwrite SharePoint content.

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

### -Identity

Source page name, relative path, server-relative path, or absolute URL.

```yaml
Type: String
Parameter Sets: Identity
Aliases: ServerRelativeUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -MaximumDependencyBytes

Maximum size of each referenced SharePoint resource embedded in the snapshot. The default is 10 MiB.

```yaml
Type: Int64
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 10485760
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWebParts

Skips shared Web Part export. Publishing HTML and all list-item fields are still captured.

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

Local file or directory for the source export. A directory produces `enterprise-wiki-export.json`.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
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

[New-PnPEnterpriseWikiMigrationPlan](New-PnPEnterpriseWikiMigrationPlan.md)
