---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPLabel.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPLabel
---
  
# Get-PnPLabel

## SYNOPSIS
Gets the Office 365 retention label/tag of the specified list or library (if applicable)

## SYNTAX

```powershell
Get-PnPLabel [-List <ListPipeBind>] [-Raw] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to retrieve all retention labels for current site or list. Use `Raw` option if you want will include more detailed information regarding labels.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPLabel
```

Returns all retention labels for the current web

### EXAMPLE 2
```powershell
Get-PnPLabel -List "Demo List" -ValuesOnly
```

This gets the retention label which is set to a list or a library

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

### -List
The ID or Url of the list

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Raw
If provided, the results will be returned as values instead of in written text and will include more detailed information

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


