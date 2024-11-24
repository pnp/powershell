---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPListItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPListItem
---

# Get-PnPListItem

## SYNOPSIS

Retrieves list items

## SYNTAX

### All Items (Default)

```
Get-PnPListItem [-List] <ListPipeBind> -<SwitchParameter>][-Connection <PnPConnection>
 [-FolderServerRelativeUrl <String>] [-Fields <String[]>] [-PageSize <Int32>]
```

### By Id

```
Get-PnPListItem [-List] <ListPipeBind> [-Id <Int32>] [-Fields <String[]>]
 [-IncludeContentType <SwitchParameter>] [-Connection <PnPConnection>]
```

### By Unique Id

```
Get-PnPListItem [-List] <ListPipeBind> [-UniqueId <Guid>] [-Fields <String[]>]
 [-IncludeContentType <SwitchParameter>] [-Connection <PnPConnection>]
```

### By Query

```
Get-PnPListItem [-List] <ListPipeBind> [-Query <String>] [-FolderServerRelativeUrl <String>]
 [-PageSize <Int32>] [-IncludeContentType <SwitchParameter>] [-ScriptBlock <ScriptBlock>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list items.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPListItem -List Tasks
```

Retrieves all list items from the Tasks list

### EXAMPLE 2

```powershell
Get-PnPListItem -List Tasks -Id 1
```

Retrieves the list item with ID 1 from the Tasks list

### EXAMPLE 3

```powershell
Get-PnPListItem -List Tasks -UniqueId bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3
```

Retrieves the list item with UniqueId or GUID bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3 from the tasks lists

### EXAMPLE 4

```powershell
(Get-PnPListItem -List Tasks -Fields "Title","GUID").FieldValues
```

Retrieves all list items, but only includes the values of the Title and GUID fields in the list item object

### EXAMPLE 5

```powershell
Get-PnPListItem -List Tasks -Query "<View><Query><Where><Eq><FieldRef Name='GUID'/><Value Type='Guid'>bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3</Value></Eq></Where></Query></View>"
```

Retrieves all available fields of list items based on the CAML query specified

### EXAMPLE 6

```powershell
Get-PnPListItem -List Tasks -Query "<View><ViewFields><FieldRef Name='Title'/><FieldRef Name='Modified'/></ViewFields><Query><Where><Eq><FieldRef Name='Modified'/><Value Type='DateTime'><Today/></Value></Eq></Where></Query></View>"
```

Retrieves all list items modified today, retrieving the columns 'Title' and 'Modified'. When you use -Query, you can add a `<ViewFields>` clause to retrieve specific columns (since you cannot use -Fields)

### EXAMPLE 7

```powershell
Get-PnPListItem -List Tasks -PageSize 1000
```

Retrieves all list items from the Tasks list in pages of 1000 items

### EXAMPLE 8

```powershell
Get-PnPListItem -List Tasks -PageSize 1000 -ScriptBlock { Param($items) $items.Context.ExecuteQuery() } | ForEach-Object { $_.BreakRoleInheritance($true, $true) }
```

Retrieves all list items from the Tasks list in pages of 1000 items and breaks permission inheritance on each item

### EXAMPLE 9

```powershell
Get-PnPListItem -List Samples -FolderServerRelativeUrl "/sites/contosomarketing/Lists/Samples/Demo"
```

Retrieves all list items from the Demo folder in the Samples list located in the contosomarketing site collection

### EXAMPLE 10

```powershell
PS D:\Code> Get-PnPListItem -List "Shared Documents" | Select-Object id,@{label="Filename";expression={$_.FieldValues.FileLeafRef}}

Id Filename
-- --------
 1 Contoso-Financial-Calendar-Q1_68340_97779.pptx
 5 Does this work.docx
```

Retrieves all list items from the Shared Documents and shows each item's ID and Filename

### EXAMPLE 11

```powershell
Get-PnPListItem -List Tasks -Id 1 -IncludeContentType
```

Retrieves the list item with ID 1 from the Tasks list along with its content type information.

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

### -Fields

The fields to retrieve. If not specified all fields will be loaded in the returned list object.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All Items
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: By Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: By Unique Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -FolderServerRelativeUrl

The server relative URL of a list folder from which results will be returned.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All Items
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: By Query
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

The ID of the item to retrieve

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeContentType

If specified, it will retrieve the content type information of the list item(s).

```yaml
Type: Switch Parameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All
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

The list to query

```yaml
Type: ListPipeBind
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

### -PageSize

The number of items to retrieve per page request.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All Items
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: By Query
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Query

The CAML query to execute against the list

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Query
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ScriptBlock

The script block to run after every page request.

```yaml
Type: ScriptBlock
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All Items
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: By Query
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UniqueId

The UniqueId or GUID of the item to retrieve

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Unique Id
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
