---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPView.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPView
---

# Set-PnPView

## SYNOPSIS

Change view properties.

## SYNTAX

### Default (Default)

```
Set-PnPView [[-List] <ListPipeBind>] -Identity <ViewPipeBind> [-Values <Hashtable>]
 [-Fields <String[]>] [-Aggregations <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets one or more properties of an existing view, see here https://learn.microsoft.com/previous-versions/office/sharepoint-server/ee543328(v=office.15) for the list of view properties.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPView -List "Tasks" -Identity "All Tasks" -Values @{JSLink="hierarchytaskslist.js|customrendering.js";Title="My view"}
```

Updates the "All Tasks" view on list "Tasks" to use hierarchytaskslist.js and customrendering.js for the JSLink and changes the title of the view to "My view".

### EXAMPLE 2

```powershell
Get-PnPList -Identity "Tasks" | Get-PnPView | Set-PnPView -Values @{JSLink="hierarchytaskslist.js|customrendering.js"}
```

Updates all views on list "Tasks" to use hierarchytaskslist.js and customrendering.js for the JSLink.

### EXAMPLE 3

```powershell
Set-PnPView -List "Documents" -Identity "Corporate Documents" -Fields "Title","Created"
```

Updates the Corporate Documents view on the Documents library to have two fields.

### EXAMPLE 4

```powershell
Set-PnPView -List "Documents" -Identity "Corporate Documents" -Fields "Title","Created" -Aggregations "<FieldRef Name='Title' Type='COUNT'/>"
```

Updates the Corporate Documents view on the Documents library and sets the totals (aggregations) to Count on the Title field.

### EXAMPLE 5

```powershell
Set-PnPView -List "Documents" -Identity "Dept Documents" -Fields "Title,"Created" -Values @{Paged=$true;RowLimit=[UInt32]"100"}
```

Updates the Dept Documents view on the Documents library to show items paged in batches of 100, note the type casting on the value to prevent warnings.

## PARAMETERS

### -Aggregations

A valid XML fragment containing one or more Aggregations.

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

An array of fields to use in the view. Notice that specifying this value will remove the existing fields.

```yaml
Type: String[]
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

The Id, Title or instance of the view.

```yaml
Type: ViewPipeBind
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

### -List

The Id, Title or Url of the list.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Values

Hashtable of properties to update on the view. Use the syntax @{property1="value";property2="value"}.

```yaml
Type: Hashtable
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
