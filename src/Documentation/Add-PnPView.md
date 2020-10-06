---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpview
schema: 2.0.0
title: Add-PnPView
---

# Add-PnPView

## SYNOPSIS
Adds a view to a list

## SYNTAX

```
Add-PnPView [-List] <ListPipeBind> -Title <String> [-Query <String>] -Fields <String[]> [-ViewType <ViewType>]
 [-RowLimit <UInt32>] [-Personal] [-SetAsDefault] [-Paged] [-Aggregations <String>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPView -List "Demo List" -Title "Demo View" -Fields "Title","Address"
```

Adds a view named "Demo view" to the "Demo List" list.

### EXAMPLE 2
```powershell
Add-PnPView -List "Demo List" -Title "Demo View" -Fields "Title","Address" -Paged
```

Adds a view named "Demo view" to the "Demo List" list and makes sure there's paging on this view.

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:
Accepted values: None, Html, Grid, Recurrence, Chart, Calendar, Gantt

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)