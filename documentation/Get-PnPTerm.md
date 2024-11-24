---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTerm.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTerm
---

# Get-PnPTerm

## SYNOPSIS

Returns a Term Store Term.

## SYNTAX

### By Term Id

```
Get-PnPTerm
 -Identity <Guid> [-TermStore <Guid>] [-IncludeChildTerms] [-Connection <PnPConnection>]
 [-Includes <String[]>]
```

### By Term Name

```
Get-PnPTerm
 -Identity <Name> -TermSet <Guid|Name> -TermGroup <Guid|Name> [-TermStore <Guid>]
 [-Recursive] [-IncludeChildTerms][-IncludeDeprecated] [-Connection <PnPConnection>]
 [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION



## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTerm -TermSet "Departments" -TermGroup "Corporate"
```

Returns all term in the termset "Departments" which is in the group "Corporate" from the site collection termstore

### EXAMPLE 2

```powershell
Get-PnPTerm -Identity "Finance" -TermSet "Departments" -TermGroup "Corporate"
```

Returns the term named "Finance" in the termset "Departments" from the termgroup called "Corporate" from the site collection termstore

### EXAMPLE 3

```powershell
Get-PnPTerm -Identity ab2af486-e097-4b4a-9444-527b251f1f8d -TermSet "Departments" -TermGroup "Corporate"
```

Returns the term named with the given id, from the "Departments" termset in a term group called "Corporate" from the site collection termstore

### EXAMPLE 4

```powershell
Get-PnPTerm -Identity "Small Finance" -TermSet "Departments" -TermGroup "Corporate" -Recursive
```

Returns the term named "Small Finance", from the "Departments" termset in a term group called "Corporate" from the site collection termstore even if it is a subterm below "Finance"

### EXAMPLE 5

```powershell
$term = Get-PnPTerm -Identity "Small Finance" -TermSet "Departments" -TermGroup "Corporate" -Includes Labels
$term.Labels
```

Returns all the localized labels for the term named "Small Finance", from the "Departments" termset in a term group called "Corporate"

### EXAMPLE 6

```powershell
Get-PnPTerm -Identity "Small Finance" -TermSet "Departments" -TermGroup "Corporate" -Recursive -IncludeDeprecated
```

Returns the deprecated term named "Small Finance", from the "Departments" termset in a term group called "Corporate" from the site collection termstore even if it is a subterm below "Finance"

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

### -Identity

The Id or Name of a Term

```yaml
Type: GenericObjectNameIdPipeBind<Microsoft.SharePoint.Client.Taxonomy.Term>
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeChildTerms

Includes the hierarchy of child terms if available

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

### -IncludeDeprecated

Includes the deprecated terms if available.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Term name
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Includes

Optionally allows properties to be retrieved for the returned terms which are not included in the response by default

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

### -Recursive

Find the first term recursively matching the label in a term hierarchy.

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

### -TermGroup

Name of the termgroup to check.

```yaml
Type: TermGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Termset
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

Name of the termset to check.

```yaml
Type: TaxonomyItemPipeBind<TermSet>
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Termset
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
Type: GenericObjectNameIdPipeBind<TermStore>
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
