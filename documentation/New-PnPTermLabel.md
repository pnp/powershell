---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPTermLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPTermLabel
---

# New-PnPTermLabel

## SYNOPSIS

Creates a localized label for a taxonomy term

## SYNTAX

### Default (Default)

```
New-PnPTermLabel -Term <TaxonomyTermPipeBind> -Name <String> -Lcid <Int32> [-IsDefault]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a localized label for a taxonomy term. Use Get-PnPTerm -Include Labels to request the current labels on a taxonomy term.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPTermLabel -Name "Finanzwesen" -Lcid 1031 -Term (Get-PnPTerm -Identity "Finance" -TermSet "Departments" -TermGroup "Corporate")
```

Creates a new localized taxonomy label in German (LCID 1031) named "Finanzwesen" for the term "Finance" in the termset Departments which is located in the "Corporate" termgroup

### EXAMPLE 2

```powershell
Get-PnPTerm -Identity "Finance" -TermSet "Departments" -TermGroup "Corporate" | New-PnPTermLabel -Name "Finanzwesen" -Lcid 1031
```

Creates a new localized taxonomy label in German (LCID 1031) named "Finanzwesen" for the term "Finance" in the termset Departments which is located in the "Corporate" termgroup

## PARAMETERS

### -IsDefault

Makes this new label the default label

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

### -Lcid

The locale id to use for the localized term

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Name

The localized name of the term

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Term

The term to add the localized label to.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
