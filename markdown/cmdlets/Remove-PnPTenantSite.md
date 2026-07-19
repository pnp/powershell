---
Module Name: PnP.PowerShell
title: Remove-PnPTenantSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantSite.html
---
 
# Remove-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a site collection

## SYNTAX

```powershell
Remove-PnPTenantSite [-Url] <String> [-SkipRecycleBin] [-Force] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Removes a site collection which is listed in your tenant administration site.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTenantSite -Url "https://tenant.sharepoint.com/sites/contoso"
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso'  and put it in the recycle bin.

### EXAMPLE 2
```powershell
Remove-PnPTenantSite -Url "https://tenant.sharepoint.com/sites/contoso" -Force -SkipRecycleBin
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' with force and it will skip the recycle bin.

### EXAMPLE 3
```powershell
Remove-PnPTenantSite -Url "https://tenant.sharepoint.com/sites/contoso" -FromRecycleBin
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the recycle bin.

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
Do not ask for confirmation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipRecycleBin
Do not add to the tenant scoped recycle bin when selected.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: SkipTrash

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
Specifies the full URL of the site collection that needs to be deleted

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

