---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPListPermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPListPermission
---

# Set-PnPListPermission

## SYNOPSIS

Allows permissions on a SharePoint list to be changed.

## SYNTAX

### Group

```
Set-PnPListPermission -Identity <ListPipeBind> -Group <GroupPipeBind> [-AddRole <String>]
 [-RemoveRole <String>] [-Connection <PnPConnection>]
```

### User

```
Set-PnPListPermission -Identity <ListPipeBind> -User <String> [-AddRole <String>]
 [-RemoveRole <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows changing permissions on a SharePoint list. In case you would like to break the permission inheritance on a list from its parent, you can use [Set-PnPList -BreakRoleInheritance](Set-PnPList.md#-breakroleinheritance).
Use Get-PnPRoleDefinition to retrieve all available roles you can add or remove using this cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'.

### EXAMPLE 2

```powershell
Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission from the user 'user@contoso.com' for the list 'Documents'.

## PARAMETERS

### -AddRole

The name of the role that must be assigned to the group or user. The name of the role is localized and depends on the language in which the site has been created, so i.e. for an English site you would use `Full Control`, but for a Dutch site you would use `Volledig beheer`.

```yaml
Type: String
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

The Id, title or an instance of the list.

```yaml
Type: ListPipeBind
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

### -RemoveRole

The name of the role that must be removed from the group or user. The name of the role is localized and depends on the language in which the site has been created, so i.e. for an English site you would use `Full Control`, but for a Dutch site you would use `Volledig beheer`.

```yaml
Type: String
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
