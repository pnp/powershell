---
Module Name: PnP.PowerShell
title: Get-PnPView
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPView.html
---
 
# Get-PnPView

## SYNOPSIS
Returns one or all views from a list

## SYNTAX

```powershell
Get-PnPView [-List] <ListPipeBind> [-Identity <ViewPipeBind>] 
 [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION

Allows to retrieve list of views from a list. By using `Identity` option it is possible to retrieve a specific view.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPView -List "Demo List"
```

Returns all views associated from the specified list

### EXAMPLE 2
```powershell
Get-PnPView -List "Demo List" -Identity "Demo View"
```

Returns the view called "Demo View" from the specified list

### EXAMPLE 3
```powershell
Get-PnPView -List "Demo List" -Identity "5275148a-6c6c-43d8-999a-d2186989a661"
```

Returns the view with the specified ID from the specified list

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

### -Identity
The ID or name of the view

```yaml
Type: ViewPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The ID or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

