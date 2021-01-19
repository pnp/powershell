---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/get-pnpexception
schema: 2.0.0
title: Get-PnPException
---

# Get-PnPException

## SYNOPSIS
Returns the last exception that occurred

## SYNTAX

```powershell
Get-PnPException [-All] [<CommonParameters>]
```

## DESCRIPTION
Returns the last exception which can be used while debugging PnP Cmdlets

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPException
```

Returns the last exception

### EXAMPLE 2
```powershell
Get-PnPException -All
```

Returns all exceptions that occurred

## PARAMETERS

### -All
Show all exceptions

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