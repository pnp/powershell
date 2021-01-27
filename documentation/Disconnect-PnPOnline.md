---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disconnect-PnPOnline.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disconnect-PnPOnline
---
  
# Disconnect-PnPOnline

## SYNOPSIS
Disconnects the context.

## SYNTAX

```powershell
Disconnect-PnPOnline [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

>Note: in general it is not recommended nor needed to use this cmdlet.

Disconnects the current context and requires you to build up a new connection in order to use the Cmdlets again. Using Connect-PnPOnline to connect to a different site has the same effect and it's rarely needed to use Disconnect-PnPOnline. Notice that if you use Disconnect-PnPOnline the internal access token cache will be cleared too. This means that if you loop through many Connect-PnPOnline and subsequent Disconnect-PnPOnline statements a full request to the Azure AD is being made to acquire a token. This will cause throttling to occur and the script will stop. 

## EXAMPLES

### EXAMPLE 1
```powershell
Disconnect-PnPOnline
```

This will clear out all active tokens

## PARAMETERS

### -Connection
Connection to be used by cmdlet

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


