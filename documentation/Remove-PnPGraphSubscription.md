---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPGraphSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPGraphSubscription
---

# Remove-PnPGraphSubscription

## SYNOPSIS

Removes an existing Microsoft Graph subscription. Required Azure Active Directory application permission depends on the resource the subscription exists on, see https://learn.microsoft.com/graph/api/subscription-delete#permissions.

## SYNTAX

### Default (Default)

```
Remove-PnPGraphSubscription -Identity <GraphSubscriptionPipeBind>
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
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
