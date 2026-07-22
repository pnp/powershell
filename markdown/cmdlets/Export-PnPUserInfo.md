---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPUserInfo.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPUserInfo
---
  
# Export-PnPUserInfo

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Export user information from site user information list.

## SYNTAX

```powershell
Export-PnPUserInfo -LoginName <String> [-Site <String>]
```

## DESCRIPTION

Export user information from the site user information list. If the Site parameter has not been specified, the current connect to site will be used.


## EXAMPLES

### EXAMPLE 1
```powershell
Export-PnPUserInfo -LoginName user@domain.com -Site "https://yoursite.sharepoint.com/sites/team"
```

This exports user data with the email address user@domain.com from the site collection specified.

### EXAMPLE 2
```powershell
Export-PnPUserInfo -LoginName user@domain.com -Site "https://yoursite.sharepoint.com/sites/team" | ConvertTo-Csv | Out-File MyFile.csv
```

This exports user data with the email address user@domain.com from the site collection specified, converts it to a CSV format and writes the result to the file MyFile.csv.

## PARAMETERS

### -LoginName
Specifies the login name of the user to export.

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
Specifies the URL of the site collection to which you want to export the user.

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


