---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPField.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPField
---
  
# Add-PnPField

## SYNOPSIS
Add a field

## SYNTAX

### Add field to list (Default)
```powershell
Add-PnPField [-List <ListPipeBind>] -DisplayName <String> -InternalName <String> -Type <FieldType>
 [-Id <Guid>] [-Formula <String>] [-Choices <String>] [-AddToDefaultView] [-Required] [-Group <String>] [-ClientSideComponentId <Guid>]
 [-ClientSideComponentProperties <String>] [-AddToAllContentTypes] [-ReturnType <String>] [-Connection <PnPConnection>]
```

### Add field reference to list
```powershell
Add-PnPField -List <ListPipeBind> -Field <FieldPipeBind> [-Connection <PnPConnection>]
 
```

### Add field to web
```powershell
Add-PnPField -DisplayName <String> -InternalName <String> -Type <FieldType> [-Id <Guid>] [-Formula <String>] [-Choices <String>]
 [-ClientSideComponentId <Guid>] [-ClientSideComponentProperties <String>] [-ReturnType <String>]
 [-Connection <PnPConnection>]
```

## DESCRIPTION
Adds a field (a column) to a list or as a site column. To add a column of type Managed Metadata use the Add-PnPTaxonomyField cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPField -Type Calculated -InternalName "C1" -DisplayName "C1" -Formula "=[Title]"
```

Adds a new calculated site column with the formula specified

### EXAMPLE 2
```powershell
Add-PnPField -List "Demo list" -DisplayName "Location" -InternalName "SPSLocation" -Type Choice -Group "Demo Group" -AddToDefaultView -Choices "Stockholm","Helsinki","Oslo"
```

This will add a field of type Choice to the list "Demo List".

### EXAMPLE 3
```powershell
Add-PnPField -List "Demo list" -DisplayName "Speakers" -InternalName "SPSSpeakers" -Type MultiChoice -Group "Demo Group" -AddToDefaultView -Choices "Obiwan Kenobi","Darth Vader", "Anakin Skywalker"
```

This will add a field of type Multiple Choice to the list "Demo List". (you can pick several choices for the same item)

### EXAMPLE 4
```powershell
Add-PnPField -List "Demo List" -Field "MyTestCol"
```

This will add an existing site column called "MyTestCol" to the list "Demo List".

### EXAMPLE 5
```powershell
Add-PnPField -Type Choice -Choices "PnP","Parker","Sharing Is Caring" -DisplayName "My Test Column" -InternalName "MyTestCol"
```

This will add a site column of type Choice (only one choice value can be chosen at the same time) called "My Test Column" with three choice values.

### EXAMPLE 6
```powershell
Add-PnPField -Type Calculated -ResultType Number -DisplayName "My Calculated Column" -InternalName "MyCalcCol" -Formula "=Today()"
```

This will add a site column of type Calculated called "My Calculated Column" which contains todays date.

## PARAMETERS

### -AddToDefaultView
Switch Parameter if this field must be added to the default view

```yaml
Type: SwitchParameter
Parameter Sets: Add field to list, Add field to web

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AddToAllContentTypes
Switch Parameter if this field must be added to all content types

```yaml
Type: SwitchParameter
Parameter Sets: Add field to list, Add field to web

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```
### -Choices
The Choice values when field type is Choice or MultiChoice

```yaml
Type: String[]
Parameter Sets: Add field to list (Choice or MultiChoice Field Chosen), Add field to web (Choice or MultiChoice Field Chosen)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentId
The Client Side Component Id to set to the field

```yaml
Type: Guid
Parameter Sets: Add field to list, Add field to web

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentProperties
The Client Side Component Properties to set to the field

```yaml
Type: String
Parameter Sets: Add field to list, Add field to web

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

### -DisplayName
The display name of the field

```yaml
Type: String
Parameter Sets: Add field to list, Add field to web

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Field
The name of the field, its ID or an actual field object that needs to be added

```yaml
Type: FieldPipeBind
Parameter Sets: Add field reference to list

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Formula
The Formula when Calculated column is chosen.

```yaml
Type: String[]
Parameter Sets: Add field to list (Calculated Field), Add field to web  (Calculated Field)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
The group name to where this field belongs to

```yaml
Type: String
Parameter Sets: Add field to list, Add field by XML to list

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The ID of the field, must be unique

```yaml
Type: Guid
Parameter Sets: Add field to list, Add field to web

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InternalName
The internal name of the field

```yaml
Type: String
Parameter Sets: Add field to list, Add field to web

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The name of the list, its ID or an actual list object where this field needs to be added

```yaml
Type: ListPipeBind
Parameter Sets: Add field to list

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

```yaml
Type: ListPipeBind
Parameter Sets: Add field reference to list

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Required
Switch Parameter if the field is a required field

```yaml
Type: SwitchParameter
Parameter Sets: Add field to list, Add field to web

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
The type of the field like Choice, Note, Calculate, MultiChoice. For a complete list of field types visit https://learn.microsoft.com/dotnet/api/microsoft.sharepoint.client.fieldtype

```yaml
Type: FieldType
Parameter Sets: Add field to list, Add field to web
Accepted values: Invalid, Integer, Text, Note, DateTime, Counter, Choice, Lookup, Boolean, Number, Currency, URL, Computed, Threading, Guid, MultiChoice, GridChoice, Calculated, File, Attachments, User, Recurrence, CrossProjectLink, ModStat, Error, ContentTypeId, PageSeparator, ThreadIndex, WorkflowStatus, AllDayEvent, WorkflowEventType, Geolocation, OutcomeChoice, Location, Thumbnail, MaxItems

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReturnType
The return type of the calculated field. Only valid when Type Calculated is chosen.

```yaml
Type: FieldType
Parameter Sets: Add field to list, Add field to web
Accepted values: Integer, Text, DateTime, Boolean, Number, Currency

Required: False
Position: Named
Default value: Text
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)