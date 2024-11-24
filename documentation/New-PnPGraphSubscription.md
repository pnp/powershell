---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPGraphSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPGraphSubscription
---

# New-PnPGraphSubscription

## SYNOPSIS

Creates a new Microsoft Graph Subscription which allows your webhook API to be called when a change occurs in Microsoft Graph

## SYNTAX

### Default (Default)

```
New-PnPGraphSubscription -ChangeType <GraphSubscriptionChangeType> -NotificationUrl <String>
 -Resource <String> [-ExpirationDateTime <DateTime>] [-ClientState <String>]
 [-LatestSupportedTlsVersion <GraphSubscriptionTlsVersion>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a new Microsoft Graph Subscription. The required Azure Active Directory application permission depends on the resource creating the subscription for, see https://learn.microsoft.com/graph/api/subscription-post-subscriptions#permissions. For a sample ASP.NET WebApi webhook implementation to receive the notifications from Microsoft Graph, see https://github.com/microsoftgraph/msgraph-training-changenotifications/blob/b8d21ca7aa5feeece336287c9a781e71b7ba01c6/demos/01-create-application/Controllers/NotificationsController.cs#L51.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPGraphSubscription -ChangeType Create -NotificationUrl https://mywebapiservice/notifications -Resource "me/mailFolders('Inbox')/messages" -ExpirationDateTime (Get-Date).AddDays(1) -ClientState [Guid]::NewGuid().ToString()
```

Creates a new Microsoft Graph subscription listening for incoming mail during the next 24 hours in the inbox of the user under which the connection has been made and will signal the URL provided through NotificationUrl when a message comes in

### EXAMPLE 2

```powershell
New-PnPGraphSubscription -ChangeType Updates -NotificationUrl https://mywebapiservice/notifications -Resource "Users" -ExpirationDateTime (Get-Date).AddHours(1) -ClientState [Guid]::NewGuid().ToString()
```

Creates a new Microsoft Graph subscription listening for changes to user objects during the next hour and will signal the URL provided through NotificationUrl when a change has been made

## PARAMETERS

### -ChangeType

The event(s) the subscription should trigger on

```yaml
Type: GraphSubscriptionChangeType
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
AcceptedValues:
- Created
- Updated
- Deleted
HelpMessage: ''
```

### -ClientState

Specifies the value of the clientState property sent by the service in each notification. The maximum length is 128 characters. The client can check that the notification came from the service by comparing the value of the clientState property sent with the subscription with the value of the clientState property received with each notification.

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

### -ExpirationDateTime

The datetime defining how long this subscription should stay alive before which it needs to get extended to stay alive. See https://learn.microsoft.com/graph/api/resources/subscription#maximum-length-of-subscription-per-resource-type for the supported maximum lifetime of the subscriber endpoints.

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

### -LatestSupportedTlsVersion

Specifies the latest version of Transport Layer Security (TLS) that the notification endpoint, specified by NotificationUrl, supports. If not provided, TLS 1.2 will be assumed.

```yaml
Type: GraphSubscriptionTlsVersion
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
AcceptedValues:
- v1_0
- v1_1
- v1_2
- v1_3
HelpMessage: ''
```

### -NotificationUrl

The URL that should be called when an event matching this subscription occurs

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

### -Resource

The resource to monitor for changes. See https://learn.microsoft.com/graph/api/subscription-post-subscriptions#resources-examples for the list with supported options.

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
