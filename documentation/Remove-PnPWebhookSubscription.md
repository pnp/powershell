---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPWebhookSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPWebhookSubscription
---

# Remove-PnPWebhookSubscription

## SYNOPSIS

Removes a Webhook subscription from the resource

## SYNTAX

### Default (Default)

```
Remove-PnPWebhookSubscription [-Identity] <WebhookSubscriptionPipeBind> [-List <ListPipeBind>]
 [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove Webhook subscription from list.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPWebhookSubscription -List MyList -Identity ea1533a8-ff03-415b-a7b6-517ee50db8b6
```

Removes the Webhook subscription with the specified id from the list MyList

### EXAMPLE 2

```powershell
$subscriptions = Get-PnPWebhookSubscriptions -List MyList
Remove-PnPWebhookSubscription -Identity $subscriptions[0] -List MyList
```

Removes the first Webhook subscription from the list MyList

### EXAMPLE 3

```powershell
$subscriptions = Get-PnPWebhookSubscriptions -List MyList
$subscriptions[0] | Remove-PnPWebhookSubscription -List MyList
```

Removes the first Webhook subscription from the list MyList

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

### -Force

Specifying the Force parameter will skip the confirmation question.

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

### -Identity

The identity of the Webhook subscription to remove

```yaml
Type: WebhookSubscriptionPipeBind
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

### -List

The list object or name which the Webhook subscription will be removed from

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
