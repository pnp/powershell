---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTerm.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTerm
---

# Remove-PnPTerm

## SYNOPSIS

Removes a taxonomy term from the term store.

## SYNTAX

### By Term Id

```
Remove-PnPTerm -Identity <Guid> [-TermStore <TaxonomyTermStorePipeBind>] [-Confirm]
```

### By Term Name

```
Remove-PnPTerm -Identity <String> -TermSet <TaxonomyTermSetPipeBind>
 -TermGroup <TaxonomyTermGroupPipeBind> [-TermStore <TaxonomyTermStorePipeBind>]
 [-Connection <PnPConnection>] [-Confirm]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes a term from the term store.

## EXAMPLES

### Example 1

```powershell
Remove-PnPTerm -Identity 3d9e60e8-d89c-4cd4-af61-a010cf93b380
```

Removes a term by id.

### Example 2

```powershell
Remove-PnPTerm -Identity "Marketing" -TermSet "Departments" -TermGroup "Corporate"
```

Removes a term by name.

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

### -Identity

Specifies which term to remove.

```yaml
Type: TaxonomyTermPipeBind
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

### -TermGroup

Specifies which termgroup to find the termset in.

```yaml
Type: TaxonomyTermGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Term Name
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermSet

Specifies which termset to find the term in if the identity of the term is specified as a string.

```yaml
Type: TaxonomyTermSetPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Term Name
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermStore

Term store to use; if not specified the default term store is used.

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
