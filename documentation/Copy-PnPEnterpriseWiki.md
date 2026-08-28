---
Module Name: PnP.PowerShell
title: Copy-PnPEnterpriseWiki
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Copy-PnPEnterpriseWiki.html
---

# Copy-PnPEnterpriseWiki

## SYNOPSIS

**Required Permissions**

* SharePoint: Sites.FullControl.All (application) or AllSites.FullControl (delegated)

Creates an Enterprise Wiki page from an approved sealed migration package.

## SYNTAX

### Approved

```powershell
Copy-PnPEnterpriseWiki [-PackagePath] <String> -ApprovedPlanDigest <String> `
    [-ReceiptPath <String>] [-Force] [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

### AutoApprove

```powershell
Copy-PnPEnterpriseWiki [-PackagePath] <String> -AutoApprove `
    [-ReceiptPath <String>] [-Force] [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

## DESCRIPTION

Validates the snapshot and plan SHA-256 digests, performs a fresh target preflight, and creates the target page using only the approved package. It does not reread or replan from the source.

The copy is create-only: an existing target page or planned dependency path blocks execution. Captured SharePoint resources are materialized, source web and tenant references are rewritten to the approved target, selected publishing metadata is applied, and shared Web Parts are imported at their captured zone positions. The page is published unless the package was captured with `Get-PnPEnterpriseWiki -Draft`.

After writing, the command creates a new target context and independently reads back the file identity, Enterprise Wiki content type, version, page content hash, and Web Part count. SharePoint may normalize `PublishingPageContent` storage bytes; browser DOM and screenshot acceptance remain a separate required fidelity gate.

## EXAMPLES

### EXAMPLE 1

```powershell
$package = Get-Content .\enterprise-wiki\architecture\enterprise-wiki-package.json -Raw | ConvertFrom-Json

Copy-PnPEnterpriseWiki `
    -PackagePath .\enterprise-wiki\architecture `
    -ApprovedPlanDigest $package.planDigest `
    -Connection $target
```

Copies the page only when the supplied digest exactly matches the sealed migration plan.

### EXAMPLE 2

```powershell
Copy-PnPEnterpriseWiki `
    -PackagePath .\enterprise-wiki\architecture `
    -AutoApprove `
    -WhatIf `
    -Connection $target
```

Shows the create operation that would be performed. `-AutoApprove` is explicit and uses the digest embedded in the validated package.

### EXAMPLE 3

```powershell
Copy-PnPEnterpriseWiki `
    -PackagePath .\enterprise-wiki\architecture `
    -ApprovedPlanDigest $approvedDigest `
    -ReceiptPath .\evidence\architecture-copy.json `
    -Connection $target
```

Creates the page and writes the fresh-readback receipt to the requested local path.

## PARAMETERS

### -ApprovedPlanDigest

SHA-256 digest reviewed and approved from the package's `planDigest` property.

```yaml
Type: String
Parameter Sets: Approved
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AutoApprove

Explicitly approves the plan digest embedded in a valid package. Omit this switch when approval is performed out of band and supply `-ApprovedPlanDigest` instead.

```yaml
Type: SwitchParameter
Parameter Sets: AutoApprove
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Connection to the exact target web recorded in the approved plan.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: Current connection
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Overwrites an existing local receipt file. It never permits overwriting a target SharePoint page or dependency.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PackagePath

Path to `enterprise-wiki-package.json` or its containing directory.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Path
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ReceiptPath

Local receipt file or directory. Defaults to `enterprise-wiki-copy-receipt.json` beside the package.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Default value: Package directory
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Get-PnPEnterpriseWiki](Get-PnPEnterpriseWiki.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
