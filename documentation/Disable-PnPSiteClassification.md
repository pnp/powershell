---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/disable-pnpsiteclassification
schema: 2.0.0
title: Disable-PnPSiteClassification
---

# Disable-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Disables Site Classifications for the tenant

## SYNTAX

```
Disable-PnPSiteClassification [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Disable-PnPSiteClassification
```

Disables Site Classifications for your tenant.

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)