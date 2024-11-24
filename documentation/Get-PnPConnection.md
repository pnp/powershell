---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPConnection.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPConnection
---

# Get-PnPConnection

## SYNOPSIS

Returns the current connection

## SYNTAX

### Default (Default)

```
Get-PnPConnection [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns a PnP PowerShell Connection for use with the -Connection parameter on other cmdlets.

## EXAMPLES

### EXAMPLE 1

```powershell
$ctx = Get-PnPConnection
```

This will put the current connection for use with the -Connection parameter on other cmdlets.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying -ReturnConnection on Connect-PnPOnline. If not provided, the connection will be retrieved from the current context.

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
