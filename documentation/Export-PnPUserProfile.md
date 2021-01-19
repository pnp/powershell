---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/export-pnpuserprofile
schema: 2.0.0
title: Export-PnPUserProfile
---

# Export-PnPUserProfile

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Export user profile data.

## SYNTAX

```powershell
Export-PnPUserProfile -LoginName <String> [<CommonParameters>]
```

## DESCRIPTION

Export user profile data.


## EXAMPLES

### EXAMPLE 1
```powershell
Export-PnPUserProfile -LoginName user@domain.com 
```

This exports user profile data with the email address user@domain.com.

### EXAMPLE 2
```powershell
Export-PnPUserProfile -LoginName user@domain.com | ConvertTo-Csv | Out-File MyFile.csv
```

This exports user profile data with the email address user@domain.com, converts it to a CSV format and writes the result to the file MyFile.csv.

## PARAMETERS

### -LoginName
Specifies the login name of the user to export.

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