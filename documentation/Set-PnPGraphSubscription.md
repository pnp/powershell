---
Module Name: PnP.PowerShell
title: Set-PnPGraphSubscription
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPGraphSubscription.html
---
 
# Set-PnPGraphSubscription

## SYNOPSIS
Updates an existing Microsoft Graph subscription. Required Azure Active Directory application permission depends on the resource the subscription exists on, see https://learn.microsoft.com/graph/api/subscription-delete#permissions.

## SYNTAX

```powershell
Set-PnPGraphSubscription -Identity <GraphSubscriptionPipeBind> -ExpirationDate <DateTime>
  [<CommonParameters>]
```

## DESCRIPTION

Allows to update an existing Microsoft Graph subscription.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPGraphSubscription -Identity bc204397-1128-4911-9d70-1d8bceee39da -ExpirationDate "2020-11-22T18:23:45.9356913Z"
```

Updates the Microsoft Graph subscription with the id 'bc204397-1128-4911-9d70-1d8bceee39da' to expire at the mentioned date

## PARAMETERS

### -ExpirationDate
Date and time to set the expiration to. Take notice of the maximum allowed lifetime of the subscription endpoints as documented at https://learn.microsoft.com/graph/api/resources/subscription#maximum-length-of-subscription-per-resource-type

```yaml
Type: DateTime
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The unique id or an instance of a Microsoft Graph Subscription

```yaml
Type: GraphSubscriptionPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

