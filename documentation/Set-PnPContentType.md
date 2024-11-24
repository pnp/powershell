---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPContentType.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPContentType
---

# Set-PnPContentType

## SYNOPSIS

Updates a content type in a web or a list

## SYNTAX

### Default (Default)

```
Set-PnPContentType [-Identity] <ContentTypePipeBind> [-List] <ListPipeBind>
 [-InSiteHierarchy] <SwitchParameter> [-UpdateChildren] <SwitchParameter> [-Name] <String>
 [-Description] <String> [-Group] <String> [-Hidden] <String> [-ReadOnly] <String>
 [-Sealed] <String> [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows modification of the settings of a content type in a list or site.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPContentType -Identity "Project Document" -UpdateChildren -Name "Project Documentation" -Description "Documentation for projects"
```

This will update a content type called "Project Document" in the current web and rename it to "Project Documentation" and change its description to "Documentation for projects"

### EXAMPLE 2

```powershell
Set-PnPContentType -Identity "Project Document" -UpdateChildren -Group "Custom Content Types" -Hidden
```

This will update a content type called "Project Document" in the current web, make it hidden and change its group to "Custom Content Types".

### EXAMPLE 3

```powershell
Set-PnPContentType -Identity "Project Document" -List "Projects" -Name "Project Documentation" -Description "Documentation for projects"
```

This will update a content type called "Project Document" in the list called "Projects" in the current web and rename it to "Project Documentation" and change its description to "Documentation for projects".

### EXAMPLE 4

```powershell
Set-PnPContentType -Identity "Project Document" -List "Projects" -FormClientSideComponentId "dfed9a30-ec25-4aaf-ae9f-a68f3598f13a" -FormClientSideComponentProperties '{ "someKey": "some value" }'
```

This will update a content type called "Project Document" in the list called "Projects" in the current web and connect an SPFx Form Customizer to it for form customization purposes. It updates the display, new item and edit item forms all at the same time.

### EXAMPLE 5

```powershell
Set-PnPContentType -Identity "Project Document" -List "Projects" -DisplayFormClientSideComponentId "dfed9a30-ec25-4aaf-ae9f-a68f3598f13a" -DisplayFormClientSideComponentProperties '{ "someKey": "some value" }'
```

This will update a content type called "Project Document" in the list called "Projects" in the current web and connect an SPFx Form Customizer to it for form customization purposes. It only updates the display form, leaving the new item and edit item forms as they are.

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

### -Description

The updated description of the content type.

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

### -DisplayFormClientSideComponentId

The component ID of an SPFx Form Customizer to connect to this content type for usage with display forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Options
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DisplayFormClientSideComponentProperties

The component properties of an SPFx Form Customizer to connect to this content type for usage with display forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Options
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EditFormClientSideComponentId

The component ID of an SPFx Form Customizer to connect to this content type for usage with edit item forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Options
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EditFormClientSideComponentProperties

The component properties of an SPFx Form Customizer to connect to this content type for usage with edit item forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Options
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -FormClientSideComponentId

Convenience option to set the component ID of an SPFx Form Customizer to connect to this content type for usage with new, edit and display forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Convenience Options
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -FormClientSideComponentProperties

Convenience option to set the component properties of an SPFx Form Customizer to connect to this content type for usage with new, edit and display forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Convenience Options
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

The updated group to which the content type belongs.

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

### -Hidden

Specify if you want to hide the content type.

```yaml
Type: Boolean
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

### -Identity

The name or ID of the content type to update

```yaml
Type: ContentTypePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -InSiteHierarchy

Search site hierarchy for content types

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

### -List

The list in which the content type to be updated resides.

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

### -Name

The updated name of the content type.

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

### -NewFormClientSideComponentId

The component ID of an SPFx Form Customizer to connect to this content type for usage with new item forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Options
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -NewFormClientSideComponentProperties

The component properties of an SPFx Form Customizer to connect to this content type for usage with new item forms.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Form Customizers Options
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ReadOnly

Specify if you want to set the content type as read only.

```yaml
Type: Boolean
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

### -Sealed

Specify if you want to seal the content type.

```yaml
Type: Boolean
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

### -UpdateChildren

Specify if you want to update the child content types

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

### -Verbose

When provided, additional debug statements will be shown while updating the content type.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
