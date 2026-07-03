---
Module Name: PnP.PowerShell
title: Set-PnPSiteClosure
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteClosure.html
---
 
# Set-PnPSiteClosure

## SYNOPSIS
Opens or closes a site which has a site policy applied

## SYNTAX

```powershell
Set-PnPSiteClosure -State <ClosureState> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to open or close a site which has a site policy applied.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteClosure -State Open
```

This opens a site which has been closed and has a site policy applied.

### EXAMPLE 2
```powershell
Set-PnPSiteClosure -State Closed
```

This closes a site which is open and has a site policy applied.

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

### -State
The state of the site

```yaml
Type: ClosureState
Parameter Sets: (All)
Accepted values: Open, Closed

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

