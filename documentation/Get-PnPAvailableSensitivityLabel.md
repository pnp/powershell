---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAvailableSensitivityLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAvailableSensitivityLabel
---

# Get-PnPAvailableSensitivityLabel

## SYNOPSIS

Gets the Microsoft Purview sensitivity labels that are available within the tenant

## SYNTAX

### Default (Default)

```
Get-PnPAvailableSensitivityLabel [-Identity <Guid>] [-User <AzureADUserPipeBind>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of the available Microsoft Purview sensitivity labels in the currently connected tenant. You can retrieve all the labels, a specific label or all the labels available to a specific user. When connected with a delegate token, it will return the Microsoft Purview sensitivity labels for the user you logged on with. When connecting with an application token, it will return all available Microsoft Purview sensitivity labels on the tenant.

For retrieval of the available classic Site Classification, use [Get-PnPAvailableSiteClassification](Get-PnPAvailableSiteClassification.md) instead.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAvailableSensitivityLabel
```

Returns all the Microsoft Purview sensitivity labels that exist on the tenant

### EXAMPLE 2

```powershell
Get-PnPAvailableSensitivityLabel -User johndoe@tenant.onmicrosoft.com
```

Returns all Microsoft Purview sensitivity labels which are available to the provided user

### EXAMPLE 3

```powershell
Get-PnPAvailableSensitivityLabel -Identity 47e66706-8627-4979-89f1-fa7afeba2884
```

Returns a specific Microsoft Purview sensitivity label by its id

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

### -Identity

The Id of the Microsoft Purview sensitivity label to retrieve

```yaml
Type: Guid
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

### -User

The UPN, Id or instance of an Azure AD user for which you would like to retrieve the Microsoft Purview sensitivity labels available to this user

```yaml
Type: SwitchParameter
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/informationprotectionpolicy-list-labels)
