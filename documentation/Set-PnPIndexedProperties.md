---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPIndexedProperties.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPIndexedProperties
---

# Set-PnPIndexedProperties

## SYNOPSIS

Marks values of the propertybag to be indexed by search.

## SYNTAX

### Default (Default)

```
Set-PnPIndexedProperties -Keys <System.Collections.Generic.List`1[System.String]>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Marks values of the propertybag to be indexed by search. Notice that this will overwrite the existing flags, i.e. only the properties you define with the cmdlet will be indexed.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPIndexedProperties -Keys SiteClosed, PolicyName
```

Example 1 overwrites the existing properties from the index and sets `SiteClosed` and `PolicyName` properties to be indexed.

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

### -Keys

Property keys to be indexed.

```yaml
Type: System.Collections.Generic.List`1[System.String]
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
