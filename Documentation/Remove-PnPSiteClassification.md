---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpsiteclassification
schema: 2.0.0
title: Remove-PnPSiteClassification
---

# Remove-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Removes one or more existing site classification values from the list of available values

## SYNTAX

```
Remove-PnPSiteClassification -Classifications <System.Collections.Generic.List`1[System.String]> [-Confirm]
 [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteClassification -Classifications "HBI"
```

Removes the "HBI" site classification from the list of available values.

### EXAMPLE 2
```powershell
Remove-PnPSiteClassification -Classifications "HBI", "Top Secret"
```

Removes the "HBI" site classification from the list of available values.

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

### -Classifications

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Specifying the Confirm parameter will allow the confirmation question to be skipped

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