---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/enable-pnpsiteclassification
schema: 2.0.0
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
 -DefaultClassification <String> [-UsageGuidelinesUrl <String>] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-PnPOnline -Scopes "Directory.ReadWrite.All"
Enable-PnPSiteClassification -Classifications "HBI","LBI","Top Secret" -DefaultClassification "LBI"
```

Enables Site Classifications for your tenant and provides three classification values. The default value will be set to "LBI"

### EXAMPLE 2
```powershell
Connect-PnPOnline -Scopes "Directory.ReadWrite.All"
Enable-PnPSiteClassification -Classifications "HBI","LBI","Top Secret" -UsageGuidelinesUrl https://aka.ms/sppnp
```

Enables Site Classifications for your tenant and provides three classification values. The usage guideliness will be set to the specified URL.

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)