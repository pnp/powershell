---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAppAuthAccessToken.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAppAuthAccessToken
---
  
# Get-PnPAppAuthAccessToken

## SYNOPSIS
Returns the access token for SharePoint Online

## SYNTAX

```powershell
Get-PnPAppAuthAccessToken [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns the SharePoint Online access token from the current client context. This will only work in the App authentication flow (App+user or App-Only). For the Microsoft Graph access token, use `Get-PnPAccessToken` instead.

## EXAMPLES

### EXAMPLE 1
```powershell
$accessToken = Get-PnPAppAuthAccessToken
```

This will put the SharePoint Online access token from current context in the $accessToken variable

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
