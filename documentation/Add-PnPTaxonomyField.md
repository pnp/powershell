---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTaxonomyField.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTaxonomyField
---

# Add-PnPTaxonomyField

## SYNOPSIS

Add a taxonomy field

## SYNTAX

### Path

```
Add-PnPTaxonomyField -DisplayName <String> -InternalName <String> -TermSetPath <String>
 [-List <ListPipeBind>] [-TermPathDelimiter <String>] [-Group <String>] [-Id <Guid>]
 [-AddToDefaultView] [-MultiValue] [-Required] [-FieldOptions <AddFieldOptions>]
 [-Connection <PnPConnection>]
```

### Id

```
Add-PnPTaxonomyField -DisplayName <String> -InternalName <String> -TaxonomyItemId <Guid>
 [-List <ListPipeBind>] [-Group <String>] [-Id <Guid>] [-AddToDefaultView] [-MultiValue] [-Required]
 [-FieldOptions <AddFieldOptions>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Adds a taxonomy/managed metadata field to a list or as a site column.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTaxonomyField -DisplayName "Test" -InternalName "Test" -TermSetPath "TestTermGroup|TestTermSet"
```

Adds a new taxonomy field called "Test" that points to the TestTermSet which is located in the TestTermGroup

### EXAMPLE 2

```powershell
Add-PnPTaxonomyField -DisplayName "Test" -InternalName "Test" -TaxonomyItemId "0e5fe3c6-3e6a-4d25-9f48-82a655f15992"
```

Adds a new taxonomy field called "Test" that points to the Term set with the term set id of 0e5fe3c6-3e6a-4d25-9f48-82a655f15992

## PARAMETERS

### -AddToDefaultView

Switch Parameter if this field must be added to the default view

```yaml
Type: SwitchParameter
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
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -FieldOptions

Specifies the control settings while adding a field. See https://learn.microsoft.com/previous-versions/office/sharepoint-server/ee542202(v=office.15) for details

```yaml
Type: AddFieldOptions
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
AcceptedValues:
- DefaultValue
- AddToDefaultContentType
- AddToNoContentType
- AddToAllContentTypes
- AddFieldInternalNameHint
- AddFieldToDefaultView
- AddFieldCheckDisplayName
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

### -Id

The ID for the field, must be unique

```yaml
Type: Guid
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

### -InternalName

The internal name of the field

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
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

The list object or name where this field needs to be added

```yaml
Type: ListPipeBind
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

### -MultiValue

Switch Parameter if this Taxonomy field can hold multiple values

```yaml
Type: SwitchParameter
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

### -Required

Switch Parameter if the field is a required field

```yaml
Type: SwitchParameter
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

### -TaxonomyItemId

The ID of the Taxonomy item

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermPathDelimiter

The path delimiter to be used, by default this is '|'

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Path
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermSetPath

The path to the term that this needs to be bound

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Path
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
