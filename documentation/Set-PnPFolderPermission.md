---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPFolderPermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPFolderPermission
---

# Set-PnPFolderPermission

## SYNOPSIS

Sets or clears permissions on folders within SharePoint Online.

## SYNTAX

### User (Default)

```
Set-PnPFolderPermission [-List] <ListPipeBind> -Identity <FolderPipeBind> -User <String>
 [-AddRole <String>] [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate]
 [-Connection <PnPConnection>]
```

### Group

```
Set-PnPFolderPermission [-List] <ListPipeBind> -Identity <FolderPipeBind> -Group <GroupPipeBind>
 [-AddRole <String>] [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate]
 [-Connection <PnPConnection>]
```

### Inherit

```
Set-PnPFolderPermission [-List] <ListPipeBind> -Identity <FolderPipeBind> [-InheritPermissions]
 [-SystemUpdate] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets or clears permissions on folders within SharePoint Online. When adding permissions, if you don't use -InheritPermissions, the library will get unique permissions that initially match those of its parent. Use `Get-PnPRoleDefinition` to retrieve all available roles you can add or remove using this cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPFolderPermission -List 'Shared Documents' -Identity 'Shared Documents/Folder' -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the folder named 'Folder' located in the root of the library 'Shared Documents'.

### EXAMPLE 2

```powershell
Set-PnPFolderPermission -List 'AnotherDocumentLibrary' -Identity 'AnotherDocumentLibrary/Folder/Subfolder' -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission from the user 'user@contoso.com' for the folder named 'Subfolder' located in the folder 'Folder' which is located in the root of the library 'AnotherDocumentLibrary'.

### EXAMPLE 3

```powershell
Set-PnPFolderPermission -List 'Shared Documents' -Identity 'Shared Documents/Folder' -User 'user@contoso.com' -AddRole 'Contribute' -ClearExisting
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the folder named 'Folder' located in the root of the library 'Shared Documents' and removes all other permissions.

### EXAMPLE 4

```powershell
Get-PnPFolder -Url 'Shared Documents/Folder' | Set-PnPFolderPermission -List 'Shared Documents' -InheritPermissions
```

Resets permissions for the folder named 'Folder' located in the root of the library 'Shared Documents' to inherit permissions from the library 'Shared Documents'.

## PARAMETERS

### -AddRole

The role that must be assigned to the group or user.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: User
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ClearExisting

Clears all existing permissions.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: User
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

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

### -Group

The ID, name or instance of a SharePoint Group to add or remove permissions to/from.

```yaml
Type: GroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Group
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

The ID of the folder, the server relative URL to the folder or actual Folder object.

```yaml
Type: FolderPipeBind
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

### -InheritPermissions

Inherit permissions from the parent, removing unique permissions.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Inherit
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -List

The ID, Title or Url of the list the folder is part of.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RemoveRole

The role that must be removed from the group or user.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: User
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SystemUpdate

Update the folder permissions without creating a new version or triggering Microsoft Power Automate Flow.

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

### -User

A valid login name of a user (e.g. john@doe.com) or an Entra ID Group (ADGroup).

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: User
  Position: Named
  IsRequired: true
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
