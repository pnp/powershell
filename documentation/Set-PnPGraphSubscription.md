---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPGraphSubscription.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPGraphSubscription
---

# Set-PnPGraphSubscription

## SYNOPSIS

Updates an existing Microsoft Graph subscription. Required Azure Active Directory application permission depends on the resource the subscription exists on, see https://learn.microsoft.com/graph/api/subscription-delete#permissions.

## SYNTAX

### Default (Default)

```
Set-PnPGraphSubscription -Identity <GraphSubscriptionPipeBind> -ExpirationDate <DateTime>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update an existing Microsoft Graph subscription.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPGraphSubscription -Identity bc204397-1128-4911-9d70-1d8bceee39da -ExpirationDate "2020-11-22T18:23:45.9356913Z"
```

Updates the Microsoft Graph subscription with the id 'bc204397-1128-4911-9d70-1d8bceee39da' to expire at the mentioned date.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ExpirationDate

Date and time to set the expiration to. Take notice of the maximum allowed lifetime of the subscription endpoints as documented at https://learn.microsoft.com/graph/api/resources/subscription#maximum-length-of-subscription-per-resource-type

```yaml
Type: DateTime
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The unique id or an instance of a Microsoft Graph Subscription.

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
