---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPRequestAccessEmails.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPRequestAccessEmails
---

# Get-PnPRequestAccessEmails

## SYNOPSIS

Returns the request access e-mail addresses

## SYNTAX

### Default (Default)

```
Get-PnPRequestAccessEmails [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve request access e-mail addresses.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPRequestAccessEmails
```

This will return all the request access e-mail addresses for the current web

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
