---
Module Name: PnP.PowerShell
title: Get-PnPEnterpriseWiki
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEnterpriseWiki.html
---

# Get-PnPEnterpriseWiki

## SYNOPSIS

**Required Permissions**

* SharePoint: Sites.Read.All (application) or AllSites.Read (delegated)

Captures an Enterprise Wiki page into a sealed, approval-ready migration package.

## SYNTAX

### Identity

```powershell
Get-PnPEnterpriseWiki [-Identity] <String> -TargetConnection <PnPConnection> -OutputPath <String> `
    [-TargetPageName <String>] [-Draft] [-NoWebParts] [-AllowUniquePermissions] `
    [-AllowManagedMetadataSubstitution] [-BlockExternalResources] [-MaximumDependencyBytes <Int64>] `
    [-Force] [-Connection <PnPConnection>]
```

### All

```powershell
Get-PnPEnterpriseWiki -All -TargetConnection <PnPConnection> -OutputPath <String> `
    [-TargetPagePrefix <String>] [-Draft] [-NoWebParts] [-AllowUniquePermissions] `
    [-AllowManagedMetadataSubstitution] [-BlockExternalResources] [-MaximumDependencyBytes <Int64>] `
    [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Captures the source page, analyzes its Enterprise Wiki ingredients, probes the target publishing environment, and writes a deterministic package containing a sealed source snapshot and migration plan. The command does not write to SharePoint.

The source must have an Enterprise Wiki Page content type. Project Page content types are deliberately excluded. The default exact profile requires the stock `EnterpriseWiki.aspx` layout, inherited page permissions, and no unresolved managed metadata mapping. Shared Web Parts and authored resource dependencies are captured when possible. Source-list-bound Web Parts, source `ErrorWebPart` instances, and legacy RSS Aggregator Web Parts are sealed as review evidence but block the v1 plan until they have an explicit replacement or target mapping. A source stability fence rejects a page that changes during capture.

The resulting `planDigest` must be explicitly supplied to `Copy-PnPEnterpriseWiki`, unless that command is invoked with `-AutoApprove`. A package with blockers is still written for review but cannot be copied.

## EXAMPLES

### EXAMPLE 1

```powershell
$source = Connect-PnPOnline -Url https://contoso.sharepoint.com/sites/source -Interactive -ReturnConnection
$target = Connect-PnPOnline -Url https://contoso.sharepoint.com/sites/communication -Interactive -ReturnConnection

$package = Get-PnPEnterpriseWiki `
    -Identity "/sites/source/Pages/Architecture.aspx" `
    -TargetConnection $target `
    -TargetPageName "Architecture-copy.aspx" `
    -OutputPath ".\enterprise-wiki\architecture" `
    -Connection $source
```

Captures one Enterprise Wiki page, performs target preflight, and writes `enterprise-wiki-package.json` plus a Markdown review report.

### EXAMPLE 2

```powershell
Get-PnPEnterpriseWiki `
    -All `
    -TargetConnection $target `
    -TargetPagePrefix "migration-2026" `
    -OutputPath ".\enterprise-wiki\batch" `
    -Connection $source
```

Captures every Enterprise Wiki page in the current web. Each page receives its own package directory and create-only target page name.

### EXAMPLE 3

```powershell
Get-PnPEnterpriseWiki `
    -Identity "Pages/Legacy.aspx" `
    -TargetConnection $target `
    -OutputPath ".\enterprise-wiki\legacy" `
    -AllowManagedMetadataSubstitution `
    -AllowUniquePermissions `
    -Connection $source
```

Captures a page while recording managed metadata and unique permissions as reviewed substitutions instead of blockers. These values are evidence-only in the v1 profile and are not applied by the copy command.

## PARAMETERS

### -All

Captures all Enterprise Wiki pages in the current web's publishing Pages library.

```yaml
Type: SwitchParameter
Parameter Sets: All
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowManagedMetadataSubstitution

Allows non-empty managed metadata to be recorded as an explicit substitution instead of blocking the plan. The v1 copy profile does not apply those values without a reviewed term mapping.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowUniquePermissions

Allows a source page with unique role assignments to produce an executable plan. Security is still captured as evidence; the v1 copy profile does not reproduce the unique assignments.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockExternalResources

Treats externally hosted renderable resources as blockers instead of preserving their URLs.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Optional connection used to read the source web. Retrieve it with `Connect-PnPOnline -ReturnConnection` or `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: Current connection
Accept pipeline input: False
Accept wildcard characters: False
```

### -Draft

Plans the target page as a draft instead of publishing it after copy.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Overwrites an existing local package and report. It never permits overwriting a target SharePoint page or dependency.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

Source page name, web-relative path, server-relative path, or absolute URL.

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

Maximum size in bytes of each authored SharePoint file dependency captured into the sealed package.

```yaml
Type: Int64
Parameter Sets: (All)
Required: False
Position: Named
Default value: 10485760
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWebParts

Skips shared Web Part export. Use only for an explicitly reviewed profile; the package cannot claim Web Part fidelity for skipped parts.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputPath

Local package file or directory. With `-All`, this is the parent directory for one package directory per page.

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetConnection

Connection used only for read-only target preflight. Capture requires a target so the sealed plan records the actual Pages library, Enterprise Wiki content type, stock layout, and create-only collision state.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetPageName

Target file name for a single page. Defaults to the source file name.

```yaml
Type: String
Parameter Sets: Identity
Required: False
Position: Named
Default value: Source file name
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetPagePrefix

Prefix used to generate create-only target file names when capturing with `-All`.

```yaml
Type: String
Parameter Sets: All
Required: False
Position: Named
Default value: pnp-ewiki
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Copy-PnPEnterpriseWiki](Copy-PnPEnterpriseWiki.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
