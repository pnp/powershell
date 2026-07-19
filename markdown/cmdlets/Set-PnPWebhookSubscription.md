---
Module Name: PnP.PowerShell
title: Set-PnPWebhookSubscription
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPWebhookSubscription.html
---
 
# Set-PnPWebhookSubscription

## SYNOPSIS
Updates a Webhook subscription

## SYNTAX

```powershell
Set-PnPWebhookSubscription [-Subscription] <WebhookSubscriptionPipeBind> [-List <ListPipeBind>]
 [-NotificationUrl <String>] [-ExpirationDate <DateTime>] [-Connection <PnPConnection>]
 
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpirationDate
The date at which the Webhook subscription will expire. (Default: 6 months from today)

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The list object or name from which the Webhook subscription will be modified

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NotificationUrl
The URL of the Webhook endpoint that will be notified of the change

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Subscription
The identity of the Webhook subscription to update

```yaml
Type: WebhookSubscriptionPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

