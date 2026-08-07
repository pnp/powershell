---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMasterPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPMasterPage
---
  
# Get-PnPMasterPage

## SYNOPSIS
Returns the URLs of the default Master Page and the custom Master Page.

## SYNTAX

```powershell
Get-PnPMasterPage [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve urls of the default Master Page and custom Master Page of the current site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMasterPage
```

This will return the urls of the default Master Page and custom Master Page of the current site.


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


