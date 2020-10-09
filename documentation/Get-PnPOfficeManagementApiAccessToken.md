---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpofficemanagementapiaccesstoken
schema: 2.0.0
title: Get-PnPOfficeManagementApiAccessToken
---

# Get-PnPOfficeManagementApiAccessToken

## SYNOPSIS
Gets an access token for the Microsoft Office 365 Management API from the current connection

## SYNTAX

```powershell
Get-PnPOfficeManagementApiAccessToken [-Decoded] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPManagementApiAccessToken
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Office 365 Management API

### EXAMPLE 2
```powershell
Get-PnPManagementApiAccessToken -Decoded
```

Gets the full OAuth 2.0 Token to consume the Microsoft Office 365 Management API

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

### -Decoded
Returns the access token in a decoded manner

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