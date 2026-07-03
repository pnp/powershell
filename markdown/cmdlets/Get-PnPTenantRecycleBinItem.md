---
Module Name: PnP.PowerShell
title: Get-PnPTenantRecycleBinItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantRecycleBinItem.html
---
 
# Get-PnPTenantRecycleBinItem

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns all modern and classic site collections in the tenant scoped recycle bin

## SYNTAX

```powershell
Get-PnPTenantRecycleBinItem [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command will return all the items in the tenant recycle bin for the Office 365 tenant you are connected to. If you are not a SharePoint Tenant Admin connect to the site where you want to manage the recycle bin and use Get-PnPRecycleBinItem.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantRecycleBinItem
```

Returns all modern and classic site collections in the tenant scoped recycle bin

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

