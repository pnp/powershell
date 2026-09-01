---
Module Name: PnP.PowerShell
title: Remove-PnPWebhookSubscription
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPWebhookSubscription.html
---
 
# Remove-PnPWebhookSubscription

## SYNOPSIS
Removes a Webhook subscription from the resource

## SYNTAX

```powershell
Remove-PnPWebhookSubscription [-Identity] <WebhookSubscriptionPipeBind> [-List <ListPipeBind>] [-Force]
 [-Connection <PnPConnection>] 
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the Webhook subscription to remove

```yaml
Type: WebhookSubscriptionPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
The list object or name which the Webhook subscription will be removed from

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

