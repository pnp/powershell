---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPNavigationNode.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPNavigationNode
---

# Remove-PnPNavigationNode

## SYNOPSIS

Removes a menu item from either the quick launch or top navigation.

## SYNTAX

### Remove a node by ID (Default)

```
Remove-PnPNavigationNode [-Identity] <NavigationNodePipeBind> [-Force] [-Connection <PnPConnection>]
```

### Remove node by Title

```
Remove-PnPNavigationNode [-Location] <NavigationType> -Title <String> [-Header <String>] [-Force]
 [-Connection <PnPConnection>]
```

### All Nodes

```
Remove-PnPNavigationNode [-All] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove menu item from either the quick launch or top navigation.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPNavigationNode -Identity 1032
```

Removes the navigation node with the specified id.

### EXAMPLE 2

```powershell
Get-PnPNavigationNode -Location Footer | Select-Object -First 1 | Remove-PnPNavigationNode -Force
```

Removes the first node of the footer navigation without asking for confirmation.

### EXAMPLE 3

```powershell
Remove-PnPNavigationNode -Title Recent -Location QuickLaunch
```

Removes the recent navigation node from the quick launch in the current web after confirmation has been given that it should be deleted.

### EXAMPLE 4

```powershell
Remove-PnPNavigationNode -Title Home -Location TopNavigationBar -Force
```

Removes the home navigation node from the top navigation bar in the current web without prompting for a confirmation.

### EXAMPLE 5

```powershell
Get-PnPNavigationNode -Location QuickLaunch | Remove-PnPNavigationNode -Force
```

Removes all the navigation nodes from the quick launch bar in the current web without prompting for a confirmation.

## PARAMETERS

### -All

Specifying the All parameter will remove all the nodes from specified Location.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All Nodes
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

### -Force

Specifying the Force parameter will skip the confirmation question.

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

### -Header

Obsolete.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Remove node by Title
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

The Id or node object to delete.

```yaml
Type: NavigationNodePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Remove a node by ID
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Location

Obsolete.

```yaml
Type: NavigationType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Remove node by Title
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- TopNavigationBar
- QuickLaunch
- SearchNav
- Footer
HelpMessage: ''
```

### -Title

Obsolete.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Remove node by Title
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
