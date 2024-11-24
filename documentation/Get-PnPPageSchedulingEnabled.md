---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPageSchedulingEnabled.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPageSchedulingEnabled
---

# Get-PnPPageSchedulingEnabled

## SYNOPSIS

Return true of false, reflecting the state of the modern page schedule feature

## SYNTAX

### Default (Default)

```
Get-PnPPageSchedulingEnabled [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This will return a boolean value stating if the modern page schedule feature has been enabled or not.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPPageSchedulingEnabled
```

This will return a boolean value stating if the modern page schedule feature has been enabled or not.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
