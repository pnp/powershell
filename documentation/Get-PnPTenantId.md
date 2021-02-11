---
Module Name: PnP.PowerShell
title: Get-PnPTenantId
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantId.html
---
 
# Get-PnPTenantId

## SYNOPSIS
Returns the Tenant ID

## SYNTAX

```powershell
Get-PnPTenantId [-TenantUrl <String>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantId
```

Returns the current Tenant Id. A valid connection with Connect-PnPOnline is required.

### EXAMPLE 2
```powershell
Get-PnPTenantId -TenantUrl "https://contoso.sharepoint.com"
```

Returns the Tenant ID for the specified tenant. Can be executed without a connecting first with Connect-PnPOnline

## PARAMETERS

### -TenantUrl

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

