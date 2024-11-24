---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Request-PnPReIndexList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Request-PnPReIndexList
---

# Request-PnPReIndexList

## SYNOPSIS

Marks the list for full indexing during the next incremental crawl

## SYNTAX

### Default (Default)

```
Request-PnPReIndexList [-Identity] <ListPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to mark the list for full indexing during the next incremental crawl.

## EXAMPLES

### EXAMPLE 1

```powershell
Request-PnPReIndexList -Identity "Demo List"
```

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

The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
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
