---
Module Name: PnP.PowerShell
title: Remove-PnPAvailableSiteClassification
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAvailableSiteClassification.html
---
 
# Remove-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Removes one or more existing classic site classification values from the list of available values on the tenant

## SYNTAX

```powershell
Remove-PnPAvailableSiteClassification -Classifications <System.Collections.Generic.List`1[System.String]> 
```

## DESCRIPTION

Allows to remove existing classic site classification values.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPAvailableSiteClassification -Classifications "HBI"
```

Removes the "HBI" site classification from the list of available values.

### EXAMPLE 2
```powershell
Remove-PnPAvailableSiteClassification -Classifications "HBI","Top Secret"
```

Removes the "HBI" and "Top Secret" site classification from the list of available values.

## PARAMETERS

### -Classifications

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
If provided or set to $true, a confirmation will be asked before the actual remove takes place. If omitted or set to $false, it will remove the site classification(s) without asking for confirmation.

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