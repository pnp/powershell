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
Returns the access token

## SYNTAX

```powershell
Get-PnPAppAuthAccessToken [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Returns the access token from the current client context (only works with App-Only authentication)

## EXAMPLES

### EXAMPLE 1
```powershell
$accessToken = Get-PnPAppAuthAccessToken
```

This will put the access token from current context in the $accessToken variable. Will only work in App authentication flow (App+user or App-Only)

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


