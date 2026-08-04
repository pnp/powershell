---
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPGraphSubscription
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPGraphSubscription.html
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
schema: 2.0.0
Module Name: PnP.PowerShell
---
  
# Remove-PnPGraphSubscription

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : depends on the resource the subscription was created on. Deleting a subscription requires read permissions on that resource, i.e. `Mail.Read` for a subscription on messages or `Sites.ReadWrite.All` for one on a SharePoint list. Note that the permission is not always the same as the one needed to create the subscription, so use the table in [Delete subscription](https://learn.microsoft.com/graph/api/subscription-delete?view=graph-rest-1.0#permissions) rather than the one for creating.

Removes an existing Microsoft Graph subscription.

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


