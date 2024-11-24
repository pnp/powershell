---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPNavigationNode.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPNavigationNode
---

# Get-PnPNavigationNode

## SYNOPSIS

Returns all or a specific navigation node

## SYNTAX

### All nodes by location (Default)

```
Get-PnPNavigationNode [-Location <NavigationType>] [-Tree] [-Connection <PnPConnection>]
```

### A single node by ID

```
Get-PnPNavigationNode [-Id <Int32>] [-Tree] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve all navigation nodes or a specific on by using `Id` option.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPNavigationNode
```

Returns all navigation nodes in the quicklaunch navigation

### EXAMPLE 2

```powershell
Get-PnPNavigationNode -Location QuickLaunch
```

Returns all navigation nodes in the quicklaunch navigation

### EXAMPLE 3

```powershell
Get-PnPNavigationNode -Location TopNavigationBar
```

Returns all navigation nodes in the top navigation bar

### EXAMPLE 4

```powershell
$node = Get-PnPNavigationNode -Id 2030
PS> $children = $node.Children
```

Returns the selected navigation node and retrieves any children

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

### -Id

The Id of the node to retrieve

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: A single node by ID
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Location

The location of the nodes to retrieve. Either TopNavigationBar, QuickLaunch, SearchNav or Footer.

```yaml
Type: NavigationType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All nodes by location
  Position: Named
  IsRequired: false
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

### -Tree

Show a tree view of all navigation nodes

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
