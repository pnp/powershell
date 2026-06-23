---
Module Name: PnP.PowerShell
title: Remove-PnPUserInfo
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPUserInfo.html
---
 
# Remove-PnPUserInfo

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a user from the user information list of a specific site collection.

## SYNTAX

```powershell
Remove-PnPUserInfo -LoginName <String> [-Site <String>]
```

## DESCRIPTION

Removes user information from the site user information list.


## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPUserInfo -LoginName user@domain.com -Site "https://yoursite.sharepoint.com/sites/team"
```

This removes a user who has the e-mail address user@domain.com from the user information list of https://contoso.sharepoint.com/sites/team site collection.


## PARAMETERS

### -LoginName
Specifies the login name of the user to remove.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Specifies the URL of the site collection.

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

