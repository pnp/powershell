---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTermGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTermGroup
---

# Remove-PnPTermGroup

## SYNOPSIS

Removes a taxonomy term group and all its term sets.

## SYNTAX

### Default (Default)

```
Remove-PnPTermGroup -Identity <TaxonomyTermGroupPipeBind> [-TermStore <TaxonomyTermStorePipeBind>]
 [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes a term group and all the contained term sets.

## EXAMPLES

### Example 1

```powershell
Remove-PnPTermGroup -Identity 3d9e60e8-d89c-4cd4-af61-a010cf93b380
```

Removes the specified term group.

### Example 2

```powershell
Remove-PnPTermGroup -Identity "Corporate"
```
Removes the specified term group.

### Example 3

```powershell
Remove-PnPTermGroup -Identity "HR" -Force
```

Removes the specified term group without prompting for confirmation.

## PARAMETERS

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

### -Identity

The name or GUID of the group to remove.

```yaml
Type: TaxonomyTermGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- GroupName
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
