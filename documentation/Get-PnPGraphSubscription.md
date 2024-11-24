---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPGraphSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPGraphSubscription
---

# Get-PnPGraphSubscription

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Subscription.Read.All

Gets subscriptions from Microsoft Graph.

## SYNTAX

### Return a list (Default)

```
Get-PnPGraphSubscription
```

### Return by specific ID

```
Get-PnPGraphSubscription [-Identity <String>]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return by specific ID
  Position: Named
  IsRequired: false
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
