---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpgraphaccesstoken
schema: 2.0.0
title: Get-PnPGraphAccessToken
---

# Get-PnPGraphAccessToken

## SYNOPSIS
Returns the current OAuth Access token for the Microsoft Graph API

## SYNTAX

```powershell
Get-PnPGraphAccessToken [-Decoded] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION
Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAccessToken
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API

### EXAMPLE 2
```powershell
Get-PnPAccessToken -Decoded
```

Gets the full OAuth 2.0 Token to consume the Microsoft Graph API

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

Only applicable to: SharePoint Online

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

### -Decoded
Returns the access token in a decoded manner

Only applicable to: SharePoint Online

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