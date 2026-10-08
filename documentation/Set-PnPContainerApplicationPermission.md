---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContainerApplicationPermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPContainerApplicationPermission
---

# Set-PnPContainerApplicationPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : FileStorageContainerTypeReg.Selected, delegated or application, as the application owning the Container Type, or delegated FileStorageContainerTypeReg.Manage.All. Delegated permissions also need the SharePoint Embedded Administrator or Global Administrator role

Sets the permissions an application has on the Containers of a SharePoint Embedded Container Type.

## SYNTAX

```powershell
Set-PnPContainerApplicationPermission [-ContainerTypeId] <Guid> [-ApplicationId] <Guid> [-AppOnlyPermissions <ContainerApplicationPermission[]>] [-DelegatedPermissions <ContainerApplicationPermission[]>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Grants an application permissions on the Containers of a Container Type registered in the tenant, through Microsoft Graph, and returns the resulting grant. Only the permissions passed in are changed: when the application already has a grant, leaving out `-AppOnlyPermissions` or `-DelegatedPermissions` keeps its permissions of that kind; when it has none yet, it gets no permissions of the kind left out. A change can take up to an hour to take effect.

With `FileStorageContainerTypeReg.Selected`, only the application that owns the Container Type can change its permissions.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPContainerApplicationPermission -ContainerTypeId 4f0af585-8dcc-0000-223d-661eb2c604e4 -ApplicationId 7b8c9d0e-1f2a-4b3c-8d4e-5f6a7b8c9d0e -AppOnlyPermissions ReadContent, WriteContent -DelegatedPermissions ReadContent
```

Lets the application read and write the content of the Containers without a user, and read their content on behalf of a user.

### EXAMPLE 2
```powershell
Set-PnPContainerApplicationPermission -ContainerTypeId 4f0af585-8dcc-0000-223d-661eb2c604e4 -ApplicationId 7b8c9d0e-1f2a-4b3c-8d4e-5f6a7b8c9d0e -AppOnlyPermissions None
```

Removes the permissions of the application on the Containers.

## PARAMETERS

### -ApplicationId

The id of the application to grant the permissions to.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases: GuestApplicationId

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AppOnlyPermissions

The permissions of the application when it calls without a user:
- None: no permissions.
- ReadContent, WriteContent, ManageContent: read, write, or read, write and manage the content of Containers.
- Create, Delete: create or delete Containers.
- Read, Write: read or update the properties of Containers.
- EnumeratePermissions, AddPermissions, UpdatePermissions, DeletePermissions, DeleteOwnPermission, ManagePermissions: list, add, change or remove the members of Containers, remove its own membership, or all of these.
- Full: all permissions.

`None` cannot be combined with other values. When omitted, the permissions the application has when it calls without a user are kept, or are none for an application without a grant yet.

```yaml
Type: ContainerApplicationPermission[]
Parameter Sets: (All)
Aliases: PermissionAppOnly
Accepted values: None, ReadContent, WriteContent, ManageContent, Create, Delete, Read, Write, EnumeratePermissions, AddPermissions, UpdatePermissions, DeletePermissions, DeleteOwnPermission, ManagePermissions, Full

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

### -ContainerTypeId

The id of the Container Type, which has to be registered in the tenant.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DelegatedPermissions

The permissions of the application when it calls on behalf of a user, with the same values as `-AppOnlyPermissions`. When omitted, the permissions the application has when it calls on behalf of a user are kept, or are none for an application without a grant yet.

```yaml
Type: ContainerApplicationPermission[]
Parameter Sets: (All)
Aliases: PermissionDelegated
Accepted values: None, ReadContent, WriteContent, ManageContent, Create, Delete, Read, Write, EnumeratePermissions, AddPermissions, UpdatePermissions, DeletePermissions, DeleteOwnPermission, ManagePermissions, Full

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
[Create fileStorageContainerTypeAppPermissionGrant](https://learn.microsoft.com/graph/api/filestoragecontainertyperegistration-post-applicationpermissiongrants)
