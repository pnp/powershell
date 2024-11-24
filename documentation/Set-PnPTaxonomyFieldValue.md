---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTaxonomyFieldValue.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTaxonomyFieldValue
---

# Set-PnPTaxonomyFieldValue

## SYNOPSIS

Sets a taxonomy term value in a listitem field

## SYNTAX

### ITEM (Default)

```
Set-PnPTaxonomyFieldValue -ListItem <ListItem> -InternalFieldName <String> -TermId <Guid>
 [-Label <String>] [-Connection <PnPConnection>]
```

### PATH

```
Set-PnPTaxonomyFieldValue -ListItem <ListItem> -InternalFieldName <String> -TermPath <String>
 [-Connection <PnPConnection>]
```

### ITEMS

```
Set-PnPTaxonomyFieldValue -ListItem <ListItem> -InternalFieldName <String> [-Terms <Hashtable>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update taxonomy term value in a listitem field.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTaxonomyFieldValue -ListItem $item -InternalFieldName 'Department' -TermId 863b832b-6818-4e6a-966d-2d3ee057931c
```

Sets the field called 'Department' to the value of the term with the ID specified

### EXAMPLE 2

```powershell
Set-PnPTaxonomyFieldValue -ListItem $item -InternalFieldName 'Department' -TermPath 'CORPORATE|DEPARTMENTS|HR'
```

Sets the field called 'Department' to the term called HR which is located in the DEPARTMENTS termset, which in turn is located in the CORPORATE termgroup.

### EXAMPLE 3

```powershell
Set-PnPTaxonomyFieldValue -ListItem $item -InternalFieldName 'Department' -Terms @{"TermId1"="Label1";"TermId2"="Label2"}
```

Sets the field called 'Department' with multiple terms by ID and label. You can refer to those terms with the {ID:label} token.

## PARAMETERS

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

### -InternalFieldName

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

### -Label

The Label value of the term

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: ITEM
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ListItem

The list item to set the field value to

```yaml
Type: ListItem
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

### -TermId

The Id of the Term

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: ITEM
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermPath

A path in the form of GROUPLABEL|TERMSETLABEL|TERMLABEL

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: PATH
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Terms

Allows you to specify terms with key value pairs that can be referred to in the template by means of the {id:label} token. See examples on how to use this parameter.

```yaml
Type: Hashtable
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: ITEMS
  Position: Named
  IsRequired: false
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
