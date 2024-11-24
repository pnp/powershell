---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Test-PnPMicrosoft365GroupAliasIsUsed.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Test-PnPMicrosoft365GroupAliasIsUsed
---

# Test-PnPMicrosoft365GroupAliasIsUsed

## SYNOPSIS

Tests if a given alias is already used.

## SYNTAX

### Default (Default)

```
Test-PnPMicrosoft365GroupAliasIsUsed -Alias <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows you to test if a provided alias is used or free, helps decide if it can be used as part of connecting an Microsoft 365 group to an existing classic site collection.

## EXAMPLES

### EXAMPLE 1

```powershell
Test-PnPMicrosoft365GroupAliasIsUsed -Alias "MyGroup"
```

This will test if the alias MyGroup is already used

## PARAMETERS

### -Alias

Specifies the alias of the group. Cannot contain spaces.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
