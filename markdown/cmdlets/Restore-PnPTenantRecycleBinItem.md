---
Module Name: PnP.PowerShell
title: Restore-PnPTenantRecycleBinItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Restore-PnPTenantRecycleBinItem.html
---
 
# Restore-PnPTenantRecycleBinItem

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Restores a site collection from the tenant scoped recycle bin

## SYNTAX

```powershell
Restore-PnPTenantRecycleBinItem -Url <String> [-Wait] [-Force] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
The Restore-PnPTenantRecycleBinItem cmdlet allows a site collection that has been deleted and still exists in the tenant recycle bin to be restored to its original location.

## EXAMPLES

### EXAMPLE 1
```powershell
Restore-PnPTenantRecycleBinItem -Url "https://tenant.sharepoint.com/sites/contoso"
```

This will restore the deleted site collection with the url 'https://tenant.sharepoint.com/sites/contoso' to its original location

### EXAMPLE 2
```powershell
Restore-PnPTenantRecycleBinItem -Url "https://tenant.sharepoint.com/sites/contoso" -Wait
```

This will restore the deleted site collection with the url 'https://tenant.sharepoint.com/sites/contoso' to its original location and will wait with executing further PowerShell commands until the site collection restore has completed

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

### -Force
If provided, no confirmation will be asked to restore the site collection from the tenant recycle bin

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
Url of the site collection to restore from the tenant recycle bin

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Wait
If provided, the PowerShell execution will halt until the site restore process has completed

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

