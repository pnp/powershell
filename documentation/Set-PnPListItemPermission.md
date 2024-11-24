---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPListItemPermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPListItemPermission
---

# Set-PnPListItemPermission

## SYNOPSIS

Sets list item permissions.

## SYNTAX

### User (Default)

```
Set-PnPListItemPermission [-List] <ListPipeBind> -Identity <ListItemPipeBind> -User <String>
 [-AddRole <String>] [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate]
 [-Connection <PnPConnection>]
```

### Group

```
Set-PnPListItemPermission [-List] <ListPipeBind> -Identity <ListItemPipeBind> -Group <GroupPipeBind>
 [-AddRole <String>] [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate]
 [-Connection <PnPConnection>]
```

### Inherit

```
Set-PnPListItemPermission [-List] <ListPipeBind> -Identity <ListItemPipeBind> [-InheritPermissions]
 [-SystemUpdate] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to modify list item permissions.
Use Get-PnPRoleDefinition to retrieve all available roles you can add or remove using this cmdlet.
Use Get-PnPGroup to retrieve all groups available that you can add or remove using this cmdlet when using the -group parameter.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for listitem with id 1 in the list 'Documents'.

### EXAMPLE 2

```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission from the user 'user@contoso.com' for listitem with id 1 in the list 'Documents'.

### EXAMPLE 3

```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -User 'user@contoso.com' -AddRole 'Contribute' -ClearExisting
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for listitem with id 1 in the list 'Documents' and removes all other permissions.

### EXAMPLE 4

```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -InheritPermissions
```

Resets permissions for listitem with id 1 to inherit permissions from the list 'Documents'.

### EXAMPLE 5

```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -AddRole 'Read' -RemoveRole 'Contribute' -Group "Site collection Visitors"
```

Adds the 'Read' permission to the group of Site collection Visitors while removing the 'Contribute' permissions.

## PARAMETERS

### -AddRole

The role that must be assigned to the group or user.

```yaml
Type: RoleDefinitionPipeBind
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

Clear all existing permissions.

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

A group object, an ID or a name of a group.

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

The ID of the listitem, or actual ListItem object.

```yaml
Type: ListItemPipeBind
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

Inherit permissions from the list, removing unique permissions.

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

The ID, Title or Url of the list.

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RemoveRole

The role that must be removed from the group or user.

```yaml
Type: RoleDefinitionPipeBind
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

Update the item permissions without creating a new version or triggering MS Flow.

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

A valid login name of a user (e.g. john@doe.com).

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
