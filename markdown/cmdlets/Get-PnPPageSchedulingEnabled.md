---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPageSchedulingEnabled.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPageSchedulingEnabled
---
  
# Get-PnPPageSchedulingEnabled

## SYNOPSIS

Return true of false, reflecting the state of the modern page schedule feature

## SYNTAX

```powershell
Get-PnPPageSchedulingEnabled [-Connection <PnPConnection>] 
```

## DESCRIPTION

This will return a boolean value stating if the modern page schedule feature has been enabled or not.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPageSchedulingEnabled
```

This will return a boolean value stating if the modern page schedule feature has been enabled or not.

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