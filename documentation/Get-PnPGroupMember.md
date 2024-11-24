---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPGroupMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPGroupMember
---

# Get-PnPGroupMember

## SYNOPSIS

Retrieves all members of a SharePoint group

## SYNTAX

### Default (Default)

```
Get-PnPGroupMember -Group <GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command will return all the users (or a specific user) that are members of the provided SharePoint group

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPGroupMember -Group "Marketing Site Members"
```

Returns all the users that are a member of the group "Marketing Site Members" in the current site collection

### EXAMPLE 2

```powershell
Get-PnPGroupMember -Group "Marketing Site Members" -User "manager@domain.com"
```

Will return a user if the user "manager@domain.com" is a member of the specified SharePoint group

### EXAMPLE 3

```powershell
Get-PnPGroup | Get-PnPGroupMember
```

Returns all the users that are a member of any of the groups in the current site collection

### EXAMPLE 4

```powershell
Get-PnPGroup | ? Title -Like 'Marketing*' | Get-PnPGroupMember
```

Returns all the users that are a member of any of the groups of which their name starts with the word 'Marketing' in the current site collection

## PARAMETERS

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

A group object, an ID or a name of a group

```yaml
Type: GroupPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
