---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFooter.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFooter
---
  
# Get-PnPFooter

## SYNOPSIS
Gets the configuration regarding the footer of the current web

## SYNTAX

```powershell
Get-PnPFooter [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows the current configuration of the footer in the current web to be retrieved. The footer currently only works on Modern Communication sites.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFooter
```

Returns the current footer configuration of the current web

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


