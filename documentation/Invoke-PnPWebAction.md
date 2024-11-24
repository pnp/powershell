---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Invoke-PnPWebAction.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Invoke-PnPWebAction
---

# Invoke-PnPWebAction

## SYNOPSIS

Executes operations on web, lists and list items.

## SYNTAX

### Default (Default)

```
Invoke-PnPWebAction [-ListName <String>] [-Webs <Web[]>]
 [-WebAction <System.Action`1[Microsoft.SharePoint.Client.Web]>]
 [-ShouldProcessWebAction <System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]>]
 [-PostWebAction <System.Action`1[Microsoft.SharePoint.Client.Web]>]
 [-ShouldProcessPostWebAction <System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]>]
 [-WebProperties <String[]>] [-ListAction <System.Action`1[Microsoft.SharePoint.Client.List]>]
 [-ShouldProcessListAction <System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]>]
 [-PostListAction <System.Action`1[Microsoft.SharePoint.Client.List]>]
 [-ShouldProcessPostListAction <System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]>]
 [-ListProperties <String[]>]
 [-ListItemAction <System.Action`1[Microsoft.SharePoint.Client.ListItem]>]
 [-ShouldProcessListItemAction <System.Func`2[Microsoft.SharePoint.Client.ListItem,System.Boolean]>]
 [-ListItemProperties <String[]>] [-SubWebs] [-DisableStatisticsOutput] [-SkipCounting]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to execute operations on web, lists and list items.

## EXAMPLES

### EXAMPLE 1

```powershell
Invoke-PnPWebAction -ListAction ${function:ListAction}
```

This will call the function ListAction on all the lists located on the current web.

### EXAMPLE 2

```powershell
Invoke-PnPWebAction -ShouldProcessListAction ${function:ShouldProcessList} -ListAction ${function:ListAction}
```

This will call the function ShouldProcessList, if it returns true the function ListAction will then be called. This will occur on all lists located on the current web

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

### -DisableStatisticsOutput

Will not output statistics after the operation

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

### -ListAction

Function to be executed on the list. There is one input parameter of type List

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.List]
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

### -ListItemAction

Function to be executed on the list item. There is one input parameter of type ListItem

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.ListItem]
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

### -ListItemProperties

The properties to load for list items.

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

### -ListName

{{ Fill ListName Description }}

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

### -ListProperties

The properties to load for list.

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

### -PostListAction

Function to be executed on the list, this will trigger after list items have been processed. There is one input parameter of type List

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.List]
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

### -PostWebAction

Function to be executed on the web, this will trigger after lists and list items have been processed. There is one input parameter of type Web

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.Web]
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

### -ShouldProcessListAction

Function to be executed on the web that would determine if ListAction should be invoked, There is one input parameter of type List and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]
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

### -ShouldProcessListItemAction

Function to be executed on the web that would determine if ListItemAction should be invoked, There is one input parameter of type ListItem and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.ListItem,System.Boolean]
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

### -ShouldProcessPostListAction

Function to be executed on the web that would determine if PostListAction should be invoked, There is one input parameter of type List and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]
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

### -ShouldProcessPostWebAction

Function to be executed on the web that would determine if PostWebAction should be invoked, There is one input parameter of type Web and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]
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

### -ShouldProcessWebAction

Function to be executed on the web that would determine if WebAction should be invoked, There is one input parameter of type Web and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]
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

### -SkipCounting

Will skip the counting process; by doing this you will not get an estimated time remaining

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

### -SubWebs

Specify if sub webs will be processed

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

### -WebAction

Function to be executed on the web. There is one input parameter of type Web

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.Web]
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

### -WebProperties

The properties to load for web.

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

### -Webs

Webs you want to process (for example different site collections), will use Web parameter if not specified

```yaml
Type: Web[]
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
