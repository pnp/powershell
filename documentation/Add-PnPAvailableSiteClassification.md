---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPAvailableSiteClassification.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPAvailableSiteClassification
---
  
# Add-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Adds one or more classic site classification values to the list of possible values

## SYNTAX

```powershell
Add-PnPAvailableSiteClassification -Classifications <System.Collections.Generic.List`1[System.String]> 
```

## DESCRIPTION

Allows to add classic site classification values

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPAvailableSiteClassification -Classifications "Top Secret"
```

Adds the "Top Secret" classification to the already existing classification values.

### EXAMPLE 2
```powershell
Add-PnPAvailableSiteClassification -Classifications "Top Secret","HBI"
```

Adds the "Top Secret" and the "HBI" classifications to the already existing classification values.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)