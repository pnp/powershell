---
Module Name: PnP.PowerShell
title: Update-PnPAvailableSiteClassification
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Update-PnPAvailableSiteClassification.html
---
 
# Update-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Updates available classic Site Classifications for the tenant

## SYNTAX

### Settings
```powershell
Update-PnPAvailableSiteClassification -Settings <SiteClassificationsSettings> 
 [<CommonParameters>]
```

### Specific
```powershell
Update-PnPAvailableSiteClassification [-Classifications <System.Collections.Generic.List`1[System.String]>]
 [-DefaultClassification <String>] [-UsageGuidelinesUrl <String>]  [<CommonParameters>]
```

## DESCRIPTION
This cmdlet allows for updating the configuration of the classic site classifications configured within the tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Update-PnPAvailableSiteClassification -Classifications "HBI","Top Secret"
```

Replaces the existing values of the site classification settings

### EXAMPLE 2
```powershell
Update-PnPAvailableSiteClassification -DefaultClassification "LBI"
```

Sets the default classification value to "LBI". This value needs to be present in the list of classification values.

### EXAMPLE 3
```powershell
Update-PnPAvailableSiteClassification -UsageGuidelinesUrl https://aka.ms/m365pnp
```

sets the usage guidelines URL to the specified URL

## PARAMETERS

### -Classifications
A list of classifications, separated by commas. E.g. "HBI","LBI","Top Secret"

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: Specific

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultClassification
The default classification to be used. The value needs to be present in the list of possible classifications

```yaml
Type: String
Parameter Sets: Specific

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Settings
A settings object retrieved by Get-PnPSiteClassification

```yaml
Type: SiteClassificationsSettings
Parameter Sets: Settings

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UsageGuidelinesUrl
The UsageGuidelinesUrl. Set to "" to clear.

```yaml
Type: String
Parameter Sets: Specific

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)