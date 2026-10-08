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

```powershell
New-PnPContainerType -ContainerTypeName <String> -OwningApplicationId <Guid> [-TrialContainerType] [-IsPassThroughBilling] [-ApplicationRedirectUrl <String>] [-IsGovernableByAdmin <Boolean>] [-IsArchiveEnabled <Boolean>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Creates a trial, standard or pass-through billing SharePoint Container Type. Use the `-TrialContainerType` switch parameter to create a trial Container Type, and the `-IsPassThroughBilling` switch parameter to have the consuming tenant pay. Without either, a standard Container Type is created, billed to your tenant.

The billing of a standard Container Type is not set up by this cmdlet. Set it up separately, for example with `Add-SPOContainerTypeBilling` in the SharePoint Online Management Shell.

An application can own only one Container Type, and the billing model of a Container Type cannot be changed after it has been created.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPContainerType -ContainerTypeName "test1" -OwningApplicationId 50785fde-3082-47ac-a36d-06282ac5c7da
```

Creates a standard SharePoint Container Type.

### EXAMPLE 2

```powershell
New-PnPContainerType -TrialContainerType -ContainerTypeName "test1" -OwningApplicationId df4085cc-9a38-4255-badc-5c5225610475
```

Creates a trial SharePoint Container Type.

### EXAMPLE 3

```powershell
New-PnPContainerType -ContainerTypeName "test1" -OwningApplicationId 50785fde-3082-47ac-a36d-06282ac5c7da -IsPassThroughBilling -ApplicationRedirectUrl "https://contoso.com/app"
```

Creates a SharePoint Container Type billed to the consuming tenant, whose files redirect to the specified url.

## PARAMETERS

### -ApplicationRedirectUrl

The url of the application that files in Containers of this Container Type redirect to.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureSubscriptionId

This parameter is obsolete and is ignored. Set up the billing of a standard Container Type separately, for example with `Add-SPOContainerTypeBilling` in the SharePoint Online Management Shell.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContainerTypeName

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

### -IsArchiveEnabled

Whether Containers of this Container Type can be archived. Archiving moves their content to the cold tier, where it cannot be accessed until the Container is reactivated. When omitted, archiving is disabled. A change can take up to 24 hours to reach the consuming tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsGovernableByAdmin

Whether the administrators of a consuming tenant can manage Containers of this Container Type in the SharePoint admin center and PowerShell. When set to `$false`, they can only view them. When omitted, they can manage them.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsPassThroughBilling

Creates a Container Type billed to the consuming tenant rather than to your tenant. Cannot be combined with `-TrialContainerType`.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OwningApplicationId

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

### -Region

This parameter is obsolete and is ignored. Set up the billing of a standard Container Type separately, for example with `Add-SPOContainerTypeBilling` in the SharePoint Online Management Shell.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroup

This parameter is obsolete and is ignored. Set up the billing of a standard Container Type separately, for example with `Add-SPOContainerTypeBilling` in the SharePoint Online Management Shell.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrialContainerType

Creates a trial Container Type, which needs no billing. Cannot be combined with `-IsPassThroughBilling`.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf

Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[SharePoint Online Embedded Container Types](https://learn.microsoft.com/sharepoint/dev/embedded/concepts/app-concepts/containertypes)
