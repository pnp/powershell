---
Module Name: PnP.PowerShell
title: Import-PnPEnterpriseWikiMigrationPackage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Import-PnPEnterpriseWikiMigrationPackage.html
---

# Import-PnPEnterpriseWikiMigrationPackage

## SYNOPSIS

**Required Permissions**

* SharePoint: Sites.FullControl.All (application) or AllSites.FullControl (delegated)

Imports an Enterprise Wiki page from an explicitly approved migration package.

## SYNTAX

### Approved

```powershell
Import-PnPEnterpriseWikiMigrationPackage [-PackagePath] <String> -ApprovedPlanDigest <String>
    [-ReceiptPath <String>] [-Force] [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

### AutoApprove

```powershell
Import-PnPEnterpriseWikiMigrationPackage [-PackagePath] <String> -AutoApprove
    [-ReceiptPath <String>] [-Force] [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

## DESCRIPTION

Validates both digests, verifies the target connection, repeats target preflight, and executes only the sealed actions. It does not reconnect to the source or silently reinterpret fields.

Only field actions marked `Apply` are written. The receipt lists every field action, whether it was attempted, whether it succeeded, and its message. A page planned as `Published` is published only when planned fields succeed. All other source lifecycle states are checked in as `Draft`. Import is create-only; `-Force` only controls local receipt overwrite.

## EXAMPLES

### EXAMPLE 1

```powershell
$plan = New-PnPEnterpriseWikiMigrationPlan .\R11\enterprise-wiki-export.json -Connection $target
Import-PnPEnterpriseWikiMigrationPackage $plan.PackagePath -ApprovedPlanDigest $plan.PlanDigest -Connection $target
```

Imports exactly the plan whose digest was reviewed.

### EXAMPLE 2

```powershell
Export-PnPEnterpriseWikiPackage Pages/R11.aspx -OutputPath .\R11 -Connection $source |
    New-PnPEnterpriseWikiMigrationPlan -Connection $target |
    Import-PnPEnterpriseWikiMigrationPackage -AutoApprove -Connection $target
```

Runs the three-stage pipeline. `-AutoApprove` skips separate human digest entry, but not validation.

### EXAMPLE 3

```powershell
$receipt = Import-PnPEnterpriseWikiMigrationPackage .\R11\enterprise-wiki-package.json -ApprovedPlanDigest $digest -Connection $target
$receipt.FieldResults | Format-Table InternalName, PlannedDisposition, Attempted, Succeeded, Message
$receipt | Select-Object ExpectedLifecycle, ActualFileLevel, LifecycleMatched
```

Reviews field execution and fresh lifecycle readback.

## PARAMETERS

### -ApprovedPlanDigest

Exact SHA-256 plan digest that was reviewed.

```yaml
Type: String
Parameter Sets: Approved
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AutoApprove

Uses the digest contained in the package. It does not bypass digest validation or blockers.

```yaml
Type: SwitchParameter
Parameter Sets: AutoApprove
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm

Prompts before importing.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Optional connection to the exact target web in the plan.

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

Overwrites an existing local receipt. It does not overwrite target content or bypass blockers.

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

### -PackagePath

Path to `enterprise-wiki-package.json` or its directory. Accepts a plan result through `PackagePath`.

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

### -ReceiptPath

Local file or directory for `enterprise-wiki-import-receipt.json`. The default is next to the package.

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

### -WhatIf

Shows what would be imported without writing to SharePoint.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[New-PnPEnterpriseWikiMigrationPlan](New-PnPEnterpriseWikiMigrationPlan.md)
