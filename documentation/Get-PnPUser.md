---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPUser
---

# Get-PnPUser

## SYNOPSIS

Returns site users of current web

## SYNTAX

### Identity based request (Default)

```
Get-PnPUser [-Identity <UserPipeBind>] [-Connection <PnPConnection>]
```

### With rights assigned

```
Get-PnPUser [-WithRightsAssigned] [-Connection <PnPConnection>]
```

### With rights assigned detailed

```
Get-PnPUser [-WithRightsAssignedDetailed] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command will return all users that exist in the current site collection's User Information List, optionally identifying their current permissions to this site

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPUser
```

Returns all users from the User Information List of the current site collection regardless if they currently have rights to access the current site

### EXAMPLE 2

```powershell
Get-PnPUser -Identity 23
```

Returns the user with Id 23 from the User Information List of the current site collection

### EXAMPLE 3

```powershell
Get-PnPUser -Identity "i:0#.f|membership|user@tenant.onmicrosoft.com"
```

Returns the user with LoginName i:0#.f|membership|user@tenant.onmicrosoft.com from the User Information List of the current site collection

### EXAMPLE 4

```powershell
Get-PnPUser | ? Email -eq "user@tenant.onmicrosoft.com"
```

Returns the user with e-mail address user@tenant.onmicrosoft.com from the User Information List of the current site collection

### EXAMPLE 5

```powershell
Get-PnPUser -WithRightsAssigned
```

Returns only those users from the User Information List of the current site collection who currently have any kind of access rights given either directly to the user or Active Directory Group or given to the user or Active Directory Group via membership of a SharePoint Group to the current site

### EXAMPLE 6

```powershell
Get-PnPUser -WithRightsAssigned -Web subsite1
```

Returns only those users from the User Information List of the current site collection who currently have any kind of access rights given either directly to the user or Active Directory Group or given to the user or Active Directory Group via membership of a SharePoint Group to subsite 'subsite1'

### EXAMPLE 7

```powershell
Get-PnPUser -WithRightsAssignedDetailed
```

Returns all users who have been granted explicit access to the current site, lists and list items

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

### -Identity

User ID or login name

```yaml
Type: UserPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Identity based request
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -WithRightsAssigned

If provided, only users that currently have any kinds of access rights assigned to the current site collection will be returned. Otherwise all users, even those who previously had rights assigned, but not anymore at the moment, will be returned as the information is pulled from the User Information List. Only works if you don't provide an -Identity.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: With rights assigned
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -WithRightsAssignedDetailed

If provided, only users that currently have any specific kind of access rights assigned to the current site, lists or list items/documents will be returned. Otherwise all users, even those who previously had rights assigned, but not anymore at the moment, will be returned as the information is pulled from the User Information List. Only works if you don't provide an -Identity.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: With rights assigned detailed
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
