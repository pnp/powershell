---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPView.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPView
---
  
# Add-PnPView

## SYNOPSIS
Adds a view to a list

## SYNTAX

```powershell
Add-PnPView [-List] <ListPipeBind> -Title <String> [-Query <String>] -Fields <String[]> [-ViewType <ViewType>]
 [-RowLimit <UInt32>] [-Personal] [-SetAsDefault] [-Paged] [-Aggregations <String>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a new view to a list.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPView -List "Demo List" -Title "Demo View" -Fields "Title","Address"
```

Adds a view named "Demo view" to the "Demo List" list.

### EXAMPLE 2
```powershell
Add-PnPView -List "Demo List" -Title "Demo View" -Fields "Title","Address" -Paged -RowLimit 100
```

Adds a view named "Demo view" to the "Demo List" list and makes sure there's paging on this view, setting a RowLimit different than the default (30 items).

### EXAMPLE 3
```powershell
Add-PnPView -List "Demo List" -Title "Demo View" -Fields "Title","Address" -Aggregations "<FieldRef Name='Title' Type='COUNT'/>"
```

Adds a view named "Demo view" to the "Demo List" list and sets the totals (aggregations) to Count on the Title field.

## PARAMETERS

### -Aggregations
A valid XML fragment containing one or more Aggregations

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Fields
A list of fields to add.

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
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

### -Paged
If specified, the view will have paging.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Personal
If specified, a personal view will be created.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Query
A valid CAML Query.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RowLimit
The row limit for the view. Defaults to 30.

```yaml
Type: UInt32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetAsDefault
If specified, the view will be set as the default view for the list.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the view.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ViewType
The type of view to add.

```yaml
Type: ViewType
Parameter Sets: (All)
Accepted values: None, Html, Grid, Recurrence, Chart, Calendar, Gantt

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


