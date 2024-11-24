---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPContainerType.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPContainerType
---

# New-PnPContainerType

## SYNOPSIS

**Required Permissions**

    * SharePoint Embedded Administrator or Global Administrator role is required

Create a Container Type for a SharePoint Embedded Application. Refer to [Hands on Lab - Setup and Configure SharePoint Embedded](https://learn.microsoft.com/en-us/sharepoint/dev/embedded/mslearn/m01-05-hol) for more details.

## SYNTAX

### Trial

```
New-PnPContainerType -ContainerTypeName <string> -OwningApplicationId <Guid> -TrialContainerType
 [-Verbose]
```

### Standard

```
New-PnPContainerType -ContainerTypeName <string> -OwningApplicationId <Guid> -Region <String>
 -AzureSubscriptionId <Guid> -ResourceGroup <String> [-Verbose]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Enables the creation of either a trial or standard SharePoint Container Type. Use the `TrialContainerType` switch parameter to designate the container type as a trial.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPContainerType -ContainerTypeName "test1" -OwningApplicationId 50785fde-3082-47ac-a36d-06282ac5c7da -AzureSubscription c7170373-eb8d-4984-8cc9-59bcc88c65a0 -ResouceGroup "SPEmbed" -Region "Uk-South"
```

Creates a standard SharePoint Container Type.

### EXAMPLE 2

```powershell
New-SPOContainerType -TrialContainerType -ContainerTypeName "test1" -OwningApplicationId df4085cc-9a38-4255-badc-5c5225610475
```

Creates a trial SharePoint Container Type.

## PARAMETERS

### -AzureSubscriptionId

The unique identifier of the Azure Active Directory profile (Microsoft Entra ID) for billing purposes.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Standard
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ContainerTypeName

The name of the Container Type.

```yaml
Type: String
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

### -OwningApplicationId

The unique identifier of the owning application which is the value of the Microsoft Entra ID app ID set up as part of configuring SharePoint Embed.

```yaml
Type: Guid
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

### -Region

The region of the Container Type.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Standard
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ResourceGroup

The resource group of the Container Type.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Standard
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TrialContainerType

The billing classification of the Container Type.

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
- [SharePoint Online Embedded Container Types](https://learn.microsoft.com/sharepoint/dev/embedded/concepts/app-concepts/containertypes)
