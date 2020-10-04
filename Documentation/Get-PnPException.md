---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpexception
schema: 2.0.0
title: Get-PnPException
---

# Get-PnPException

## SYNOPSIS
Returns the last exception that occurred

## SYNTAX

```
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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)