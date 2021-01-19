---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/add-pnpsiteclassification
schema: 2.0.0
title: Add-PnPSiteClassification
---

# Add-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Adds one or more site classification values to the list of possible values

## SYNTAX

```powershell
Add-PnPSiteClassification -Classifications <System.Collections.Generic.List`1[System.String]> [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteClassification -Classifications "Top Secret"
```

Adds the "Top Secret" classification to the already existing classification values.

### EXAMPLE 2
```powershell
Add-PnPSiteClassification -Classifications "Top Secret","HBI"
```

Adds the "Top Secret" and the "For Your Eyes Only" classification to the already existing classification values.

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
