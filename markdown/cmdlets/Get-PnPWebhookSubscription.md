---
Module Name: PnP.PowerShell
title: Get-PnPWebhookSubscription
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPWebhookSubscription.html
---
 
# Get-PnPWebhookSubscription

## SYNOPSIS
Gets all the Webhook subscriptions of the resource

## SYNTAX

```powershell
Get-PnPWebhookSubscription [-List <ListPipeBind>] [-Connection <PnPConnection>]
 
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The list object or name to get the Webhook subscriptions from

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

