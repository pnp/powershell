---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Move-PnPPageComponent.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Move-PnPPageComponent
---

# Move-PnPPageComponent

## SYNOPSIS

Moves a page component to a different section/column

## SYNTAX

### Move to other section

```
Move-PnPPageComponent [-Page] <PagePipeBind> -InstanceId <Guid> -Section <Int32> [-Position <Int32>]
 [-Connection <PnPConnection>]
```

### Move to other section and column

```
Move-PnPPageComponent [-Page] <PagePipeBind> -InstanceId <Guid> -Section <Int32> -Column <Int32>
 [-Position <Int32>] [-Connection <PnPConnection>]
```

### Move to other column

```
Move-PnPPageComponent [-Page] <PagePipeBind> -InstanceId <Guid> -Column <Int32> [-Position <Int32>]
 [-Connection <PnPConnection>]
```

### Move within a column

```
Move-PnPPageComponent [-Page] <PagePipeBind> -InstanceId <Guid> -Position <Int32>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Moves a component to a different location on the page. Notice that the sections and or columns need to be present before moving the component.

## EXAMPLES

### EXAMPLE 1

```powershell
Move-PnPPageComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Section 1
```

Moves the specified component to the first section of the page.

### EXAMPLE 2

```powershell
Move-PnPPageComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Column 2
```

Moves the specified component to the second column of the current section.

### EXAMPLE 3

```powershell
Move-PnPPageComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Section 1 -Column 2
```

Moves the specified component to the first section of the page into the second column.

### EXAMPLE 4

```powershell
Move-PnPPageComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Section 1 -Column 2 -Position 2
```

Moves the specified component to the first section of the page into the second column and sets the column to position 2 in the list of webparts.

## PARAMETERS

### -Column

The column to move the web part to

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Move to other section and column
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Move to other column
  Position: Named
  IsRequired: true
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

### -InstanceId

The instance id of the control. Use Get-PnPPageComponent to retrieve the instance ids.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Page

The name of the page

```yaml
Type: PagePipeBind
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

### -Position

Change to order of the web part in the column

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Move to other section
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Move to other section and column
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Move to other column
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Section

The section to move the web part to

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Move to other section
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Move to other section and column
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
