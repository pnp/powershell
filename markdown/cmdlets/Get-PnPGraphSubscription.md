---
title: Get-PnPGraphSubscription
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPGraphSubscription.html
Module Name: PnP.PowerShell
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
---
   
# Get-PnPGraphSubscription

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Subscription.Read.All

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



