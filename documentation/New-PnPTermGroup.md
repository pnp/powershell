---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPTermGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPTermGroup
---

# New-PnPTermGroup

## SYNOPSIS

Creates a taxonomy term group

## SYNTAX

### Default (Default)

```
New-PnPTermGroup -Name <String> [-Id <Guid>] [-Description <String>]
 [-TermStore <PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]>]
 [->] [->] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create a taxonomy term group.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPTermGroup -GroupName "Countries"
```

Creates a new taxonomy term group named "Countries"

### EXAMPLE 2

```powershell
New-PnPTermGroup -GroupName "Countries" -Contributors @("i:0#.f|membership|pradeepg@gautamdev.onmicrosoft.com","i:0#.f|membership|adelev@gautamdev.onmicrosoft.com") -Managers @("i:0#.f|membership|alexw@gautamdev.onmicrosoft.com","i:0#.f|membership|diegos@gautamdev.onmicrosoft.com")
```

Creates a new taxonomy term group named "Countries" and sets the users as contributors and managers of the term group. **The user names for contributors and managers need to be encoded claim for the specified login names.**

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

### -Contributors

The contributor to the term group who can create/edit term sets in the group. **The user names for contributors need to be encoded claim for the specified login names.**

```yaml
Type: string[]
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

Description to use for the term group.

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

GUID to use for the term group; if not specified, or the empty GUID, a random GUID is generated and used.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- GroupId
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

### -Managers

The manager of the term group who can create/edit term sets in the group as well as add/remove contributors. **The user names for managers need to be encoded claim for the specified login names.**

```yaml
Type: string[]
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

Name of the taxonomy term group to create.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- GroupName
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

### -TermStore

Term store to add the group to; if not specified the default term store is used.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- TermStoreName
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
