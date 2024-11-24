---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPWebhookSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPWebhookSubscription
---

# Add-PnPWebhookSubscription

## SYNOPSIS

Adds a new Webhook subscription

## SYNTAX

### Default (Default)

```
Add-PnPWebhookSubscription -NotificationUrl <String> [-List <ListPipeBind>]
 [-ExpirationDate <DateTime>] [-ClientState <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a new Webhook subscription to a list.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPWebhookSubscription -List MyList -NotificationUrl https://my-func.azurewebsites.net/webhook
```

Add a Webhook subscription for the specified notification Url on the list MyList

### EXAMPLE 2

```powershell
Add-PnPWebhookSubscription -List MyList -NotificationUrl https://my-func.azurewebsites.net/webhook -ExpirationDate "2017-09-01"
```

Add a Webhook subscription for the specified notification Url on the list MyList with an expiration date set on September 1st, 2017

### EXAMPLE 3

```powershell
Add-PnPWebhookSubscription -List MyList -NotificationUrl https://my-func.azurewebsites.net/webhook -ExpirationDate "2017-09-01" -ClientState "Hello State!"
```

Add a Webhook subscription for the specified notification Url on the list MyList with an expiration date set on September 1st, 2017 with a specific client state

## PARAMETERS

### -ClientState

A client state information that will be passed through notifications

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

The list object or name where the Webhook subscription will be added to

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
