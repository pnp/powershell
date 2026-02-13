---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchVerticalOrder.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPSearchVerticalOrder
---

# Set-PnPSearchVerticalOrder

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Reorders custom Microsoft Search verticals on the currently connected site or at the organization level.

## SYNTAX

```powershell
Set-PnPSearchVerticalOrder -Identity <String[]> [-Scope <SearchVerticalScope>] [-WhatIf] [-Confirm] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet reorders custom Microsoft Search verticals by deleting and recreating them in the specified order. Built-in verticals (All, Files, Sites, News, Images) always appear first and cannot be reordered. You must provide all custom vertical logical IDs in the desired order. It uses the Graph Connector Service (GCS) API at gcs.office.com.

> **Warning:** This cmdlet works by deleting and recreating verticals in the desired order. It is optimized to only delete and recreate verticals from the first position change onward — verticals already in the correct position at the start are skipped. Each delete and create is verified before proceeding to the next step. If an error occurs during recreation, the cmdlet reports which verticals were successfully recreated and which failed. It is strongly recommended to save your current vertical configuration before reordering, so you can manually restore verticals if needed.

> **Tip:** Use `-Verbose` to see detailed progress of each delete and create operation. Use `-WhatIf` to preview the operation without making changes.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

## EXAMPLES

### EXAMPLE 1
```powershell
# Get the current order of custom verticals
Get-PnPSearchVertical | Where-Object { $_.Payload.VerticalType -eq 1 } | Select-Object LogicalId, @{N='Name';E={$_.Payload.DisplayName}}

# Reorder them
Set-PnPSearchVerticalOrder -Identity "1627986949869_XP4E83ZYU", "1610383262385_H0RPZO96M", "1720524198280_58PYYU8S9"
```

Lists the current custom verticals and their order, then reorders them so that the vertical with ID `1627986949869_XP4E83ZYU` appears first.

### EXAMPLE 2
```powershell
Set-PnPSearchVerticalOrder -Identity "1627986949869_XP4E83ZYU", "1610383262385_H0RPZO96M" -Scope Organization
```

Reorders organization-level custom verticals.

### EXAMPLE 3
```powershell
Set-PnPSearchVerticalOrder -Identity "1627986949869_XP4E83ZYU", "1610383262385_H0RPZO96M" -Verbose
```

Reorders verticals with verbose output showing each delete/verify/create step.

### EXAMPLE 4
```powershell
# Save current vertical configuration before reordering
$verticals = Get-PnPSearchVertical | Where-Object { $_.Payload.VerticalType -eq 1 }
$verticals | ConvertTo-Json -Depth 10 | Out-File "verticals-backup.json"

# Reorder
Set-PnPSearchVerticalOrder -Identity "1627986949869_XP4E83ZYU", "1610383262385_H0RPZO96M" -Verbose
```

Saves the current custom verticals to a JSON file before reordering, allowing manual restoration if needed.

### EXAMPLE 5
```powershell
Set-PnPSearchVerticalOrder -Identity "1627986949869_XP4E83ZYU", "1610383262385_H0RPZO96M" -WhatIf
```

Shows what would happen without actually making changes.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
An ordered array of custom vertical logical IDs specifying the desired display order. All custom vertical IDs must be included. Built-in vertical IDs are not allowed. Use `Get-PnPSearchVertical` to discover the current verticals and their logical IDs.

```yaml
Type: String[]
Parameter Sets: (All)
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Specifies whether to reorder site-level or organization-level verticals. Defaults to Site.

```yaml
Type: SearchVerticalScope
Parameter Sets: (All)
Accepted values: Site, Organization
Required: False
Position: Named
Default value: Site
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, shows detailed progress for each step: deleting verticals, verifying deletion, creating verticals, and verifying creation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
