---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTermGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTermGroup
---

# Set-PnPTermGroup

## SYNOPSIS

Updates an existing term group.

## SYNTAX

### Default (Default)

```
Set-PnPTermGroup -Identity <TaxonomyTermGroupPipeBind> [-Name <String>] [-Description <String>]
 [-TermStore <TaxonomyTermStorePipeBind>] [-Connection <PnPConnection>] [->] [->]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The cmdlet allows you to update an existing term group.

## EXAMPLES

### Example 1

```powershell
Set-PnPTermGroup -Identity "Departments" -Name "Company Units"
```

Renames the Departments termgroup to "Company Units".

### Example 2

```powershell
Set-PnPTermGroup -Identity "Departments" -Name "Company Units" -Contributors @("i:0#.f|membership|pradeepg@gautamdev.onmicrosoft.com","i:0#.f|membership|adelev@gautamdev.onmicrosoft.com") -Managers @("i:0#.f|membership|alexw@gautamdev.onmicrosoft.com","i:0#.f|membership|diegos@gautamdev.onmicrosoft.com")
```

Renames the Departments termgroup to "Company Units" and adds contributors and managers of the term group. **The user names for contributors and managers need to be encoded claim for the specified login names.**

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

Optional description of the term group.

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

### -Identity

The term group to update. Either name or a GUID.

```yaml
Type: TaxonomyTermGroupPipeBind
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

The new name for the term group.

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

### -TermStore

The termstore to use. If not specified the default term store is used.

```yaml
Type: TaxonomyTermStorePipeBind
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
