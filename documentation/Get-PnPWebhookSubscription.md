---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPWebhookSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPWebhookSubscription
---

# Get-PnPWebhookSubscription

## SYNOPSIS

Gets all the Webhook subscriptions of the resource

## SYNTAX

### Default (Default)

```
Get-PnPWebhookSubscription [-List <ListPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve Webhook subscriptions of specified list.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPWebhookSubscription -List MyList
```

Gets all Webhook subscriptions of the list MyList

### EXAMPLE 2

```powershell
Get-PnPList | Get-PnPWebhookSubscription
```

Gets all Webhook subscriptions of the all the lists

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

### -List

The list object or name to get the Webhook subscriptions from

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
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
