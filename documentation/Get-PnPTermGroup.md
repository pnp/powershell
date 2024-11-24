---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTermGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTermGroup
---

# Get-PnPTermGroup

## SYNOPSIS

Returns a taxonomy term group

## SYNTAX

### Default (Default)

```
Get-PnPTermGroup
 [-Identity <TaxonomyTermGroupPipeBind>] [-TermStore <TaxonomyTermStorePipeBind>]
 [-Connection <PnPConnection>] [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve a taxonomy term group.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTermGroup
```

Returns all Term Groups in the site collection termstore

### EXAMPLE 2

```powershell
Get-PnPTermGroup -Identity "Departments"
```

Returns the termgroup named "Departments" from the site collection termstore

### EXAMPLE 3

```powershell
Get-PnPTermGroup -Identity ab2af486-e097-4b4a-9444-527b251f1f8d
```

Returns the termgroup with the given ID from the site collection termstore

## PARAMETERS

### -Identity

Name of the taxonomy term group to retrieve.

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
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Includes

Optionally allows properties to be retrieved for the returned term group(s) which are not included in the response by default

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

### -TermStore

Term store to use; if not specified the default term store is used.

```yaml
Type: TaxonomyTermStorePipeBind
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
