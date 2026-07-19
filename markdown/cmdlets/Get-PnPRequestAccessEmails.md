---
Module Name: PnP.PowerShell
title: Get-PnPRequestAccessEmails
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPRequestAccessEmails.html
---
 
# Get-PnPRequestAccessEmails

## SYNOPSIS
Returns the request access e-mail addresses

## SYNTAX

```powershell
Get-PnPRequestAccessEmails [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve request access e-mail addresses.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPRequestAccessEmails
```

This will return all the request access e-mail addresses for the current web

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

