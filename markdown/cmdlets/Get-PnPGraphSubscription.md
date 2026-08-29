---
Module Name: PnP.PowerShell
schema: 2.0.0
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPGraphSubscription
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPGraphSubscription.html
---
   
# Get-PnPGraphSubscription

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : depends on the resource the subscription was created on. Reading a subscription back requires read permissions on that resource, i.e. `Mail.Read` for a subscription on messages or `Sites.ReadWrite.All` for one on a SharePoint list. Note that the permission is not always the same as the one needed to create the subscription, so use the table in [List subscriptions](https://learn.microsoft.com/graph/api/subscription-list?view=graph-rest-1.0#permissions) rather than the one for creating.
  * Microsoft Graph API : `Subscription.Read.All` (delegated) is only required to also return subscriptions that were created by *other* applications. It is not needed to return the subscriptions created by this application.

Gets subscriptions from Microsoft Graph.

## SYNTAX

### Return a list (Default)
```powershell
Get-PnPGraphSubscription 
```

### Return by specific ID
```powershell
Get-PnPGraphSubscription [-Identity <String>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPGraphSubscription
```

Retrieves all subscriptions from Microsoft Graph

### EXAMPLE 2
```powershell
Get-PnPGraphSubscription -Identity 328c7693-5524-44ac-a946-73e02d6b0f98
```

Retrieves the subscription from Microsoft Graph with the id 328c7693-5524-44ac-a946-73e02d6b0f98

## PARAMETERS

### -Identity
Returns the subscription with the provided subscription id

```yaml
Type: String
Parameter Sets: Return by specific ID

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)



