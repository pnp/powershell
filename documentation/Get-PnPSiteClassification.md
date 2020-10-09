---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpsiteclassification
schema: 2.0.0
title: Get-PnPSiteClassification
---

# Get-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All

Returns the defined Site Classifications for the tenant

## SYNTAX

```powershell
Get-PnPSiteClassification [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteClassification
```

Returns the currently set site classifications for the tenant.

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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)