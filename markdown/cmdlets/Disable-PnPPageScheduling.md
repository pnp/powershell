---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disable-PnPPageScheduling.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disable-PnPPageScheduling
---
  
# Disable-PnPPageScheduling

## SYNOPSIS

Disables the modern page schedule feature

## SYNTAX

```powershell
Disable-PnPPageScheduling [-Connection <PnPConnection>] 
```

## DESCRIPTION

This will disable page publishing scheduling on modern sites

## EXAMPLES

### EXAMPLE 1
```powershell
Disable-PnPPageScheduling
```

This will disable page publishing scheduling on the current site

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