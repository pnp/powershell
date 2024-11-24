---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPWebhookSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPWebhookSubscription
---

# Set-PnPWebhookSubscription

## SYNOPSIS

Updates a Webhook subscription

## SYNTAX

### Default (Default)

```
Set-PnPWebhookSubscription [-Subscription] <WebhookSubscriptionPipeBind> [-List <ListPipeBind>]
 [-NotificationUrl <String>] [-ExpirationDate <DateTime>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update Webhook subscription.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPWebhookSubscription -List MyList -Subscription ea1533a8-ff03-415b-a7b6-517ee50db8b6 -NotificationUrl https://my-func.azurewebsites.net/webhook
```

Updates an existing Webhook subscription with the specified id on the list MyList with a new Notification Url

### EXAMPLE 2

```powershell
Set-PnPWebhookSubscription -List MyList -Subscription ea1533a8-ff03-415b-a7b6-517ee50db8b6 -NotificationUrl https://my-func.azurewebsites.net/webhook -ExpirationDate "2017-09-01"
```

Updates an existing Webhook subscription with the specified id on the list MyList with a new Notification Url and a new expiration date

### EXAMPLE 3

```powershell
$subscriptions = Get-PnPWebhookSubscriptions -List MyList
$updated = $subscriptions[0]
$updated.ExpirationDate = "2017-10-01"
Set-PnPWebhookSubscription -List MyList -Subscription $updated
```

Updates the Webhook subscription from the list MyList with a modified subscription object.
Note: The date will be converted to Universal Time

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

### -ExpirationDate

The date at which the Webhook subscription will expire. (Default: 6 months from today)

```yaml
Type: DateTime
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

The list object or name from which the Webhook subscription will be modified

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

### -NotificationUrl

The URL of the Webhook endpoint that will be notified of the change

```yaml
Type: String
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

### -Subscription

The identity of the Webhook subscription to update

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
