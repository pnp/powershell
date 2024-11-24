---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAlert.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAlert
---

# Get-PnPAlert

## SYNOPSIS

Returns registered alerts for a user.

## SYNTAX

### Default (Default)

```
Get-PnPAlert [[-List] <ListPipeBind>] [-User <UserPipeBind>] [-Title <String>] [-AllUsers]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve all registered alerts for given users. Using `AllUsers` option will allow to retrieve all alerts in the current site, regardless of the user or list it belongs to.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAlert
```

Returns all registered alerts for the current user.

### EXAMPLE 2

```powershell
Get-PnPAlert -List "Demo List"
```

Returns all alerts registered on the given list for the current user.

### EXAMPLE 3

```powershell
Get-PnPAlert -List "Demo List" -User "i:0#.f|membership|Alice@contoso.onmicrosoft.com"
```

Returns all alerts registered on the given list for the specified user.

### EXAMPLE 4

```powershell
Get-PnPAlert -Title "Demo Alert"
```

Returns all alerts with the given title for the current user. Title comparison is case sensitive.

### EXAMPLE 5

```powershell
Get-PnPAlert -AllUsers
```

Returns all alerts that exist in the current site, regardless of the user or list it belongs to.

### EXAMPLE 6

```powershell
Get-PnPAlert -List "Demo List" -AllUsers
```

Returns all alerts that exist in the current site for the list "Demo List", regardless of the user it belongs to.

## PARAMETERS

### -AllUsers

Retrieves alerts for all users in the current site

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Alerts for all users
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
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Title

Retrieve alerts with this title. Title comparison is case sensitive.

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

User to retrieve the alerts for (User ID, login name or actual User object). Skip this parameter to retrieve the alerts for the current user. Note: Only site owners can retrieve alerts for other users.

```yaml
Type: UserPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Alerts for a specific user
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
