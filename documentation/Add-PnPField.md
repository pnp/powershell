---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpfield
schema: 2.0.0
title: Add-PnPField
---

# Add-PnPField

## SYNOPSIS
Add a field

## SYNTAX

### Add field to list (Default)
```
Add-PnPField [-List <ListPipeBind>] -DisplayName <String> -InternalName <String> -Type <FieldType>
 [-Id <Guid>] [-AddToDefaultView] [-Required] [-Group <String>] [-ClientSideComponentId <Guid>]
 [-ClientSideComponentProperties <String>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Add field reference to list
```
Add-PnPField -List <ListPipeBind> -Field <FieldPipeBind> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Add field to web
```
Add-PnPField -DisplayName <String> -InternalName <String> -Type <FieldType> [-Id <Guid>]
 [-ClientSideComponentId <Guid>] [-ClientSideComponentProperties <String>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Add field by XML to list
```
Add-PnPField [-AddToDefaultView] [-Required] [-Group <String>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Adds a field (a column) to a list or as a site column. To add a column of type Managed Metadata use the Add-PnPTaxonomyField cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPField -Type Calculated -InternalName "C1" -DisplayName "C1" -Formula ="[Title]"
```

Adds a new calculated site column with the formula specified

### EXAMPLE 2
```powershell
Add-PnPField -List "Demo list" -DisplayName "Location" -InternalName "SPSLocation" -Type Choice -Group "Demo Group" -AddToDefaultView -Choices "Stockholm","Helsinki","Oslo"
```

This will add a field of type Choice to the list "Demo List".

### EXAMPLE 3
```powershell
PS:>Add-PnPField -List "Demo list" -DisplayName "Speakers" -InternalName "SPSSpeakers" -Type MultiChoice -Group "Demo Group" -AddToDefaultView -Choices "Obiwan Kenobi","Darth Vader", "Anakin Skywalker"
```

This will add a field of type Multiple Choice to the list "Demo List". (you can pick several choices for the same item)

## PARAMETERS

### -AddToDefaultView
Switch Parameter if this field must be added to the default view

```yaml
Type: SwitchParameter
Parameter Sets: Add field to list, Add field by XML to list
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentId
The Client Side Component Id to set to the field

Only applicable to: SharePoint Online

```yaml
Type: Guid
Parameter Sets: Add field to list, Add field to web
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentProperties
The Client Side Component Properties to set to the field

Only applicable to: SharePoint Online

```yaml
Type: String
Parameter Sets: Add field to list, Add field to web
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

### -DisplayName
The display name of the field

```yaml
Type: String
Parameter Sets: Add field to list, Add field to web
Aliases:

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
Aliases:

Required: True
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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

```yaml
Type: ListPipeBind
Parameter Sets: Add field reference to list
Aliases:

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
Parameter Sets: Add field to list, Add field by XML to list
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
The type of the field like Choice, Note, MultiChoice. For a complete list of field types visit https://docs.microsoft.com/dotnet/api/microsoft.sharepoint.client.fieldtype

```yaml
Type: FieldType
Parameter Sets: Add field to list, Add field to web
Aliases:
Accepted values: Invalid, Integer, Text, Note, DateTime, Counter, Choice, Lookup, Boolean, Number, Currency, URL, Computed, Threading, Guid, MultiChoice, GridChoice, Calculated, File, Attachments, User, Recurrence, CrossProjectLink, ModStat, Error, ContentTypeId, PageSeparator, ThreadIndex, WorkflowStatus, AllDayEvent, WorkflowEventType, Geolocation, OutcomeChoice, Location, Thumbnail, MaxItems

Required: True
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