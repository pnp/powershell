---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPGroup
---

# Get-PnPGroup

## SYNOPSIS

Returns a specific SharePoint group or all SharePoint groups in the current site

## SYNTAX

### All (Default)

```
Get-PnPGroup [-Connection <PnPConnection>] [-Includes <String[]>]
```

### ByName

```
Get-PnPGroup [[-Identity] <GroupPipeBind>] [-Connection <PnPConnection>] [-Includes <String[]>]
```

### Members

```
Get-PnPGroup [-AssociatedMemberGroup] [-Connection <PnPConnection>] [-Includes <String[]>]
```

### Visitors

```
Get-PnPGroup [-AssociatedVisitorGroup] [-Connection <PnPConnection>] [-Includes <String[]>]
```

### Owners

```
Get-PnPGroup [-AssociatedOwnerGroup] [-Connection <PnPConnection>] [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPGroup
```

Returns all SharePoint groups in the current site

### EXAMPLE 2
```powershell
Get-PnPGroup -Identity 'My Site Users'
```

This will return the group called 'My Site Users' if available in the current site. The name is case sensitive, so a group called 'My site users' would not be found.

### EXAMPLE 3
```powershell
Get-PnPGroup -AssociatedMemberGroup
```

This will return the current members group for the site

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPGroup
```

Returns all SharePoint groups in the current site

### EXAMPLE 2

```powershell
Get-PnPGroup -Identity 'My Site Users'
```

This will return the group called 'My Site Users' if available in the current site. The name is case sensitive, so a group called 'My site users' would not be found.

### EXAMPLE 3

```powershell
Get-PnPGroup -AssociatedMemberGroup
```

This will return the current members group for the site

## PARAMETERS

### -AssociatedMemberGroup

Retrieve the associated member group

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Members
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AssociatedOwnerGroup

Retrieve the associated owner group

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Owners
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AssociatedVisitorGroup

Retrieve the associated visitor group

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Visitors
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

### -Identity

Get a specific group by its name or id. The name case sensitive.

```yaml
Type: GroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Name
ParameterSets:
- Name: ByName
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Includes

Optionally allows properties to be retrieved for the returned SharePoint security group which are not included in the response by default

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
