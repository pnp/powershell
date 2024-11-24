---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTimeZoneId.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTimeZoneId
---

# Get-PnPTimeZoneId

## SYNOPSIS

Returns a time zone ID

## SYNTAX

### Default (Default)

```
Get-PnPTimeZoneId [[-Match] <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

In order to create a new classic site you need to specify the timezone this site will use. Use the cmdlet to retrieve a list of possible values.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTimeZoneId
```

This will return all time zone IDs in use by Office 365.

### EXAMPLE 2

```powershell
Get-PnPTimeZoneId -Match Stockholm
```

This will return the time zone IDs for Stockholm

## PARAMETERS

### -Match

A string to search for like 'Stockholm'

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
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
