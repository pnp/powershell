---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPField.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPField
---

# Add-PnPField

## SYNOPSIS

Add a field

## SYNTAX

### Add field to list (Default)

```
Add-PnPField -DisplayName <String> -InternalName <String> -Type <FieldType> [-List <ListPipeBind>]
 [-Id <Guid>] [-Formula <String>] [-Choices <String>] [-AddToDefaultView] [-Required]
 [-Group <String>] [-ClientSideComponentId <Guid>] [-ClientSideComponentProperties <String>]
 [-AddToAllContentTypes] [-Connection <PnPConnection>]
```

### Add field reference to list

```
Add-PnPField -List <ListPipeBind> -Field <FieldPipeBind> [-Connection <PnPConnection>]
```

### Add field to web

```
Add-PnPField -DisplayName <String> -InternalName <String> -Type <FieldType> [-Id <Guid>]
 [-Formula <String>] [-Choices <String>] [-ClientSideComponentId <Guid>]
 [-ClientSideComponentProperties <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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

### -AddToAllContentTypes

Switch Parameter if this field must be added to all content types

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AddToDefaultView

Switch Parameter if this field must be added to the default view

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Choices

The Choice values when field type is Choice or MultiChoice

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list (Choice or MultiChoice Field Chosen)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web (Choice or MultiChoice Field Chosen)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ClientSideComponentId

The Client Side Component Id to set to the field

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ClientSideComponentProperties

The Client Side Component Properties to set to the field

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DisplayName

The display name of the field

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Field

The name of the field, its ID or an actual field object that needs to be added

```yaml
Type: FieldPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field reference to list
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Formula

The Formula when Calculated column is chosen.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list (Calculated Field)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web  (Calculated Field)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Group

The group name to where this field belongs to

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field by XML to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Id

The ID of the field, must be unique

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -InternalName

The internal name of the field

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -List

The name of the list, its ID or an actual list object where this field needs to be added

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Required

Switch Parameter if the field is a required field

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Type

The type of the field like Choice, Note, Calculate, MultiChoice. For a complete list of field types visit https://learn.microsoft.com/dotnet/api/microsoft.sharepoint.client.fieldtype

```yaml
Type: FieldType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Add field to list
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Add field to web
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Invalid
- Integer
- Text
- Note
- DateTime
- Counter
- Choice
- Lookup
- Boolean
- Number
- Currency
- URL
- Computed
- Threading
- Guid
- MultiChoice
- GridChoice
- Calculated
- File
- Attachments
- User
- Recurrence
- CrossProjectLink
- ModStat
- Error
- ContentTypeId
- PageSeparator
- ThreadIndex
- WorkflowStatus
- AllDayEvent
- WorkflowEventType
- Geolocation
- OutcomeChoice
- Location
- Thumbnail
- MaxItems
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
