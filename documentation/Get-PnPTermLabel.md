---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTermLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTermLabel
---

# Get-PnPTermLabel

## SYNOPSIS

Returns all or a specific translation label for a term.

## SYNTAX

### By Term Id

```
Get-PnPTermLabel -Term <Guid> [-Lcid <Int32>] [-TermStore <TaxonomyTermStorePipeBind>]
```

### By Term Name

```
Get-PnPTermLabel -Term <String> -TermSet <TaxonomyTermSetPipeBind>
 -TermGroup <TaxonomyTermGroupPipeBind> [-Lcid <Int32>] [-TermStore <TaxonomyTermStorePipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlets allows to retrieve all or a specific translation label for a term

## EXAMPLES

### Example 1

```powershell
Get-PnPTermLabel -Term af8601d6-d925-46dd-af7b-4a58515ffd83
```

Returns all translation labels for a term

### Example 2

```powershell
Get-PnPTermLabel -Term af8601d6-d925-46dd-af7b-4a58515ffd83 -Lcid 1033
```

Returns all translation labels for a term for a specific language

### Example 3

```powershell
Get-PnPTermLabel -Term "Marketing" -TermSet "Departments" -TermGroup "Corporate"
```

Returns all translation labels for a term

## PARAMETERS

### -Lcid

Specify the codepage value for a language, for instance 1033 for English.

```yaml
Type: Int32
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

### -Term

The term to retieve the labels for

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Term Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermGroup

The TermGroup containing the termset, required when referring to the term by name.

```yaml
Type: TaxonomyTermGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Termset
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermSet

The TermSet to containing the term, required when referring to the term by name.

```yaml
Type: TaxonomyTermSetPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Termset
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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
