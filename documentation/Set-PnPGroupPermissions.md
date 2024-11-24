---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPGroupPermissions.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPGroupPermissions
---

# Set-PnPGroupPermissions

## SYNOPSIS

Adds and/or removes permissions of a specific SharePoint group.

## SYNTAX

### Default (Default)

```
Set-PnPGroupPermissions [-Identity] <GroupPipeBind> [-List <ListPipeBind>] [-AddRole <String[]>]
 [-RemoveRole <String[]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet adds or removes roles (permissions) of a specified group on a site or list level.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -AddRole Contribute
```

Adds the 'Contribute' permission to the SharePoint group with the name 'My Site Members'.

### EXAMPLE 2

```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -RemoveRole 'Full Control' -AddRole 'Read'
```

Removes the 'Full Control' from and adds the 'Read' permissions to the SharePoint group with the name 'My Site Members'.

### EXAMPLE 3

```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -AddRole @('Contribute', 'Design')
```

Adds the 'Contribute' and 'Design' permissions to the SharePoint group with the name 'My Site Members'.

### EXAMPLE 4

```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -RemoveRole @('Contribute', 'Design')
```

Removes the 'Contribute' and 'Design' permissions from the SharePoint group with the name 'My Site Members'.

### EXAMPLE 5

```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -List 'MyList' -RemoveRole @('Contribute')
```

Removes the 'Contribute' permissions from the list 'MyList' for the group with the name 'My Site Members'.

## PARAMETERS

### -AddRole

Name of the role (permission level) to add to the SharePoint group.

```yaml
Type: String[]
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

### -Identity

A group object, an ID or a name of a group.

```yaml
Type: GroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Name
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

### -List

Specify the Id, title or an instance of the list where permissions should be updated.

```yaml
Type: ListPipeBind
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

### -RemoveRole

Name of the role (permission level) to remove from the SharePoint group.

```yaml
Type: String[]
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
