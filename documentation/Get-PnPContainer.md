---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContainer
---
  
# Get-PnPContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns one or more Containers in SharePoint Embedded. The Containers returned can be piped to `Set-PnPContainer` and `Remove-PnPContainer`.

## SYNTAX

```powershell
Get-PnPContainer [[-Identity] <ContainerPipeBind>] [-OwningApplicationId <Guid>] [-Paged] [-PagingToken <String>] [-SortByStorage <SortOrder>] [-ArchiveStatus <SPContainerArchiveStatusFilterProperties>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Returns the Containers of every SharePoint Embedded application in the tenant, the Containers of one application when `-OwningApplicationId` is given, or a single Container when `-Identity` is given. As an application owns exactly one container type, `-OwningApplicationId` also returns the Containers of one container type.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContainer
```

Returns the active Containers of every SharePoint Embedded application in the tenant.

### EXAMPLE 2
```powershell
Get-PnPContainer -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996
```

Returns the active Containers created under the specified SharePoint Embedded application.

### EXAMPLE 3
```powershell
Get-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Returns the properties of the specified Container by using the Container id.

### EXAMPLE 4
```powershell
Get-PnPContainer -Identity "https://contoso.sharepoint.com/contentstorage/CSP_4bd71a68-8312-4275-99b1-a2b70e3ba0e8"
```

Returns the properties of the specified Container by using the Container url.

### EXAMPLE 5
```powershell
Get-PnPContainer -SortByStorage Descending -ArchiveStatus Archived
```

Returns the archived Containers in the tenant, largest first.

## PARAMETERS

### -ArchiveStatus

The ArchiveStatus parameter is used to display containers in various stages of archiving. The following states are supported:
- Archived: Displays containers in all archived states.
- RecentlyArchived: Displays containers in the "Recently archived" state.
- FullyArchived: Displays containers in the "Fully archived" state.
- Reactivating: Displays containers in the "Reactivating" state.
- NotArchived: Displays active containers

```yaml
Type: SPContainerArchiveStatusFilterProperties
Parameter Sets: (All)

Required: False
Position: Named
Default value: NotArchived
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

### -Identity

Specify container site url, container api url or container id.

```yaml
Type: ContainerPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -OwningApplicationId

This parameter specifies the ID of the SharePoint repository services application. When omitted, the Containers of every application in the tenant are returned.

To retrieve Containers for the Microsoft Loop app, use OwningApplicationId: a187e399-0c36-4b98-8f04-1edc167a0996.
To retrieve Containers for the Microsoft Designer app, use OwningApplicationId: 5e2795e3-ce8c-4cfb-b302-35fe5cd01597

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Paged

Returns a single batch of Containers followed by a paging token, which can be passed to `-PagingToken` to retrieve the next batch. Without it, all Containers are returned.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -PagingToken

Use this parameter to provide the paging token returned when using `-Paged`. Together with `-Paged`, it returns the next batch of Containers, or the message End of containers view when there are no more. Without `-Paged`, it returns every Container after the token.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SortByStorage

Sorts the Containers by the storage they use, in ascending or descending order. When omitted, the Containers of every application in the tenant are sorted by creation date, newest first.

```yaml
Type: SortOrder
Parameter Sets: (All)
Accepted values: Ascending, Descending

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
