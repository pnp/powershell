---
Module Name: PnP.PowerShell
title: Remove-PnPGraphSubscription
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPGraphSubscription.html
---
 
# Remove-PnPGraphSubscription

## SYNOPSIS
Removes an existing Microsoft Graph subscription. Required Azure Active Directory application permission depends on the resource the subscription exists on, see https://learn.microsoft.com/graph/api/subscription-delete#permissions.

## SYNTAX

```powershell
Remove-PnPGraphSubscription -Identity <GraphSubscriptionPipeBind>  
```

## DESCRIPTION

Allows to remove an existing Microsoft Graph subscription.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPGraphSubscription -Identity bc204397-1128-4911-9d70-1d8bceee39da
```

Removes the Microsoft Graph subscription with the id 'bc204397-1128-4911-9d70-1d8bceee39da'

## PARAMETERS

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

