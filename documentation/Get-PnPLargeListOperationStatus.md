---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPLargeListOperationStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPLargeListOperationStatus
---

# Get-PnPLargeListOperationStatus

## SYNOPSIS

Get the status of a large list operation. Currently supports large list removal operation.

## SYNTAX

### Default (Default)

```
Get-PnPLargeListOperationStatus [-Identity] <ListId> [-OperationId] <OperationId>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to get the status of a large list operation.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPLargeListOperationStatus -Identity 9ea5d197-2227-4156-9ae1-725d74dc029d -OperationId 924e6a34-5c90-4d0d-8083-2efc6d1cf481
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

ID of the list. Retrieve the value for this parameter from the output of the large list operation command. It can be retrieved as:
`Remove-PnPList -Identity "Contoso" -Recycle -LargeList`

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OperationId

OperationId of the large list operation. Retrieve the value for this parameter from the output of the large list operation command which can be used as:
`Remove-PnPList -Identity "Contoso" -Recycle -LargeList`.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
