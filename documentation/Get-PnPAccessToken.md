---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAccessToken.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAccessToken
---
  
# Get-PnPAccessToken

## SYNOPSIS
Returns the current OAuth Access token

## SYNTAX

```powershell
Get-PnPAccessToken [-Decoded] [<CommonParameters>]
```

## DESCRIPTION
Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API. Doesn't work with all Connect-PnPOnline options.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAccessToken
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API

## PARAMETERS

### -Decoded
Returns the details from the access token in a decoded manner

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


