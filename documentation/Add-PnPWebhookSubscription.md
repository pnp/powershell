---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPWebhookSubscription.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPWebhookSubscription
---
  
# Add-PnPWebhookSubscription

## SYNOPSIS
Adds a new Webhook subscription

## SYNTAX

```powershell
Add-PnPWebhookSubscription [-List <ListPipeBind>] -NotificationUrl <String> [-ExpirationDate <DateTime>]
 [-ClientState <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
The list object or name where the Webhook subscription will be added to

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

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


