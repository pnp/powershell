---
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContainerApplication.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContainerApplication
---
 
# Get-PnPContainerApplication

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Returns the SharePoint Embedded applications registered in the tenant.

## SYNTAX

### All (Default)
```powershell
Get-PnPContainerApplication [-Connection <PnPConnection>]
```

### By owning application
```powershell
Get-PnPContainerApplication [-OwningApplicationId] <Guid> [[-ApplicationId] <Guid>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Returns the SharePoint Embedded applications of every publisher registered in the tenant. With `-OwningApplicationId`, returns the details of one application: the guest applications with permissions on it and its sharing settings. Adding `-ApplicationId` returns the permissions of that guest application on the owning application.

**Known issue:** without `-OwningApplicationId`, and with `-ApplicationId`, the cmdlet can fail with "The type of data at position ... is different than the one expected." SharePoint Online returns an empty version limit for an application that has none set, which the SharePoint client library cannot read. `Get-SPOApplication` in the SharePoint Online Management Shell is affected the same way. Until an updated SharePoint client library fixes this, use `-OwningApplicationId` without `-ApplicationId` to return the details of each application.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContainerApplication
```

Returns every SharePoint Embedded application in the tenant.

### EXAMPLE 2
```powershell
Get-PnPContainerApplication -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996
```

Returns the guest applications with permissions on the Microsoft Loop application and its sharing settings.

### EXAMPLE 3
```powershell
Get-PnPContainerApplication -OwningApplicationId 2ce03211-353e-45d7-b487-8ac6981332cf -ApplicationId 7b8c9d0e-1f2a-4b3c-8d4e-5f6a7b8c9d0e
```

Returns the permissions of the specified guest application on the owning application.

## PARAMETERS

### -ApplicationId

The id of a guest application, to return its permissions on the application given with `-OwningApplicationId`.

```yaml
Type: Guid
Parameter Sets: By owning application

Required: False
Position: 1
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

### -OwningApplicationId

The id of the SharePoint Embedded application to return the details of.

To return the Microsoft Loop app, use OwningApplicationId: a187e399-0c36-4b98-8f04-1edc167a0996.
To return the Microsoft Designer app, use OwningApplicationId: 5e2795e3-ce8c-4cfb-b302-35fe5cd01597

```yaml
Type: Guid
Parameter Sets: By owning application

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Manage containers with PowerShell](https://learn.microsoft.com/sharepoint/dev/embedded/admin/manage-containers-powershell)

