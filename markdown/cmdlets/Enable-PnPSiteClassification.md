---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPSiteClassification.html
external help file: PnP.PowerShell.dll-Help.xml
title: Enable-PnPSiteClassification
---
  
# Enable-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Enables Site Classifications for the tenant

## SYNTAX

```powershell
Enable-PnPSiteClassification -Classifications <System.Collections.Generic.List`1[System.String]>
 -DefaultClassification <String> [-UsageGuidelinesUrl <String>] 
```

## DESCRIPTION

Allows to enable site classifications for the tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Enable-PnPSiteClassification -Classifications "HBI","LBI","Top Secret" -DefaultClassification "LBI"
```

Enables Site Classifications for your tenant and provides three classification values. The default value will be set to "LBI"

### EXAMPLE 2
```powershell
Enable-PnPSiteClassification -Classifications "HBI","LBI","Top Secret" -UsageGuidelinesUrl https://aka.ms/m365pnp
```

Enables Site Classifications for your tenant and provides three classification values. The usage guidelines will be set to the specified URL.

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

### -DefaultClassification

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UsageGuidelinesUrl

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


