---
Module Name: PnP.PowerShell
title: New-PnPContainerType
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPContainerType.html
---

# New-PnPContainerType

## SYNOPSIS

**Required Permissions**

    * SharePoint Embedded Administrator or Global Administrator role is required

Create a Container Type for a SharePoint Embedded Application. Refer to [Hands on Lab - Setup and Configure SharePoint Embedded](https://learn.microsoft.com/en-us/sharepoint/dev/embedded/mslearn/m01-05-hol) for more details.

## SYNTAX

### Trial

```powershell
New-PnPContainerType -ContainerTypeName <string> -OwningApplicationId <Guid> -TrialContainerType <SwitchParameter> [-Region <String>] [-AzureSubscriptionId <Guid>] [-ResourceGroup <String>]
```

### Standard

```powershell
New-PnPContainerType -ContainerTypeName <string> -OwningApplicationId <Guid> -Region <String> -AzureSubscriptionId <Guid> -ResourceGroup <String>
```

## DESCRIPTION

Enables the creation of either a trial or standard SharePoint Container Type. Use the `TrialContainerType` switch parameter to designate the container type as a trial.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPContainerType -ContainerTypeName "test1" -OwningApplicationId 50785fde-3082-47ac-a36d-06282ac5c7da  -AzureSubscription c7170373-eb8d-4984-8cc9-59bcc88c65a0 -ResouceGroup "SPEmbed" -Region "Uk-South"
```

Creates a standard SharePoint Container Type.

### EXAMPLE 2

```powershell
New-SPOContainerType -TrialContainerType -ContainerTypeName "test1" -OwningApplicationId df4085cc-9a38-4255-badc-5c5225610475
```

Creates a trial SharePoint Container Type.


## PARAMETERS

### ContainerTypeName

The name of the Container Type.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### OwningApplicationId

The unique identifier of the owning application which is the value of the Microsoft Entra ID app ID set up as part of configuring SharePoint Embed.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrialContainerType

The billing classification of the Container Type.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureSubscriptionId

The unique identifier of the Azure Active Directory profile (Microsoft Entra ID) for billing purposes.

```yaml
Type: Guid
Parameter Sets: Standard

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Region

The region of the Container Type.

```yaml
Type: String
Parameter Sets: Standard

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroup

The resource group of the Container Type.

```yaml
Type: String
Parameter Sets: Standard

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[https://pnp.github.io/pnpframework/api/PnP.Framework.Enums.TimeZone.html](https://pnp.github.io/pnpframework/api/PnP.Framework.Enums.TimeZone.html)
