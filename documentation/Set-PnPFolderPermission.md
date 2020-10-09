---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpfolderpermission
schema: 2.0.0
title: Set-PnPFolderPermission
---

# Set-PnPFolderPermission

## SYNOPSIS
Sets folder permissions. Use Get-PnPRoleDefinition to retrieve all available roles you can add or remove using this cmdlet.

## SYNTAX

### User (Default)
```powershell
Set-PnPFolderPermission [-List] <ListPipeBind> -Identity <FolderPipeBind> -User <String> [-AddRole <String>]
 [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Group
```powershell
Set-PnPFolderPermission [-List] <ListPipeBind> -Identity <FolderPipeBind> -Group <GroupPipeBind>
 [-AddRole <String>] [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Inherit
```powershell
Set-PnPFolderPermission [-List] <ListPipeBind> -Identity <FolderPipeBind> [-InheritPermissions] [-SystemUpdate]
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPFolderPermission -List 'Shared Documents' -Identity 'Shared Documents\Folder' -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the folder named 'Folder' located in the root of the library 'Shared Documents'

### EXAMPLE 2
```powershell
Set-PnPFolderPermission -List 'Documents' -Identity 'Shared Documents\Folder\Subfolder' -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission to the user 'user@contoso.com' for the folder named 'Subfolder' located in the folder 'Folder' which is located in the root of the library 'Shared Documents'

### EXAMPLE 3
```powershell
Set-PnPFolderPermission -List 'Documents' -Identity 'Shared Documents\Folder' -User 'user@contoso.com' -AddRole 'Contribute' -ClearExisting
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the folder named 'Folder' located in the root of the library 'Shared Documents' and removes all other permissions

### EXAMPLE 4
```powershell
Get-PnPFolder -Url 'Shared Documents\Folder' | Set-PnPFolderPermission -List 'Documents' -InheritPermissions
```

Resets permissions for the folder named 'Folder' located in the root of the library 'Shared Documents' to inherit permissions from the library 'Shared Documents'

## PARAMETERS

### -AddRole
The role that must be assigned to the group or user

```yaml
Type: String
Parameter Sets: User, Group
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearExisting
Clear all existing permissions

```yaml
Type: SwitchParameter
Parameter Sets: User, Group
Aliases:

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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group

```yaml
Type: GroupPipeBind
Parameter Sets: Group
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID of the folder, the server relative URL to the folder or actual Folder object

```yaml
Type: FolderPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -InheritPermissions
Inherit permissions from the parent, removing unique permissions

```yaml
Type: SwitchParameter
Parameter Sets: Inherit
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The ID, Title or Url of the list the folder is part of

```yaml
Type: ListPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveRole
The role that must be removed from the group or user

```yaml
Type: String
Parameter Sets: User, Group
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SystemUpdate
Update the folder permissions without creating a new version or triggering MS Flow.

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User

```yaml
Type: String
Parameter Sets: User
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)