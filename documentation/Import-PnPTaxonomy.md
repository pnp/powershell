---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Import-PnPTaxonomy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Import-PnPTaxonomy
---

# Import-PnPTaxonomy

## SYNOPSIS

Imports a taxonomy from either a string array or a file

## SYNTAX

### Direct

```
Import-PnPTaxonomy [-Terms <String[]>] [-Lcid <Int32>] [-TermStoreName <String>]
 [-Delimiter <String>] [-SynchronizeDeletions] [-Connection <PnPConnection>]
```

### File

```
Import-PnPTaxonomy -Path <String> [-Lcid <Int32>] [-TermStoreName <String>] [-Delimiter <String>]
 [-SynchronizeDeletions] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to import taxonomy terms from array or file.

## EXAMPLES

### EXAMPLE 1

```powershell
Import-PnPTaxonomy -Terms 'Company|Locations|Stockholm'
```

Creates a new termgroup, 'Company', a termset 'Locations' and a term 'Stockholm'

### EXAMPLE 2

```powershell
Import-PnPTaxonomy -Terms 'Company|Locations|"Stockholm,Central"'
```

Creates a new termgroup, 'Company', a termset 'Locations', a term 'Stockholm,Central'

### EXAMPLE 3

```powershell
Import-PnPTaxonomy -Terms 'Company|Locations|Stockholm|Central','Company|Locations|Stockholm|North'
```

Creates a new termgroup, 'Company', a termset 'Locations', a term 'Stockholm' and two subterms: 'Central', and 'North'

### EXAMPLE 4

```powershell
Import-PnPTaxonomy -Path ./mytaxonomyterms.txt
```

Imports the taxonomy from the file specified. Each line has to be in the format TERMGROUP|TERMSET|TERM. See example 2 for examples of the format.

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

### -Delimiter

The path delimiter to be used, by default this is '|'

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

### -Lcid



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

### -Path

Specifies a file containing terms per line, in the format as required by the Terms parameter.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: File
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SynchronizeDeletions

If specified, terms that exist in the termset, but are not in the imported data, will be removed.

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

### -Terms

An array of strings describing termgroup, termset, term, subterms using a default delimiter of '|'.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Direct
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermStoreName

Term store to import to; if not specified the default term store is used.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
