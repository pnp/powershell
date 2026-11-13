---
Module Name: PnP.PowerShell
title: Remove-PnPTenantDeletedSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantDeletedSite.html
---
 
# Remove-PnPTenantDeletedSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a site collection from the Tenant recycle bin.

## SYNTAX

```powershell
Remove-PnPTenantDeletedSite [-Identity] <String> [-Force] [-NoWait] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Removes a site collection which is listed in your tenant administration site from the tenant's recycle bin.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTenantDeletedSite -Identity "https://tenant.sharepoint.com/sites/contoso"
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the recycle bin.

### EXAMPLE 2
```powershell
Remove-PnPTenantDeletedSite -Identity "https://tenant.sharepoint.com/sites/contoso" -Force
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' with force from the recycle bin.

## PARAMETERS

### -Identity
Specifies the full URL of the site collection that needs to be deleted.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Url

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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

### -NoWait
If specified the task will return immediately after creating the delete site job.

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

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

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