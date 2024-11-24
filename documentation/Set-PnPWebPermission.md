---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPWebPermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPWebPermission
---

# Set-PnPWebPermission

## SYNOPSIS

Sets web permissions.

## SYNTAX

### Set group permissions

```
Set-PnPWebPermission -Group <GroupPipeBind> [-Identity <WebPipeBind>] [-AddRole <String[]>]
 [-RemoveRole <String[]>] [-Connection <PnPConnection>]
```

### Set user permissions

```
Set-PnPWebPermission -User <String> [-Identity <WebPipeBind>] [-AddRole <String[]>]
 [-RemoveRole <String[]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet adds permissions to a user or a group or removes permissions from a user or a group.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPWebPermission -User "user@contoso.com" -AddRole "Contribute"
```

Adds the "Contribute" permission role to the specified user in the current web.

### EXAMPLE 2

```powershell
Set-PnPWebPermission -Group "Project Managers" -AddRole "Contribute"
```

Adds the "Contribute" permission role to the "Project Managers" group in the current web.

### EXAMPLE 3

```powershell
Set-PnPWebPermission -Identity projectA -User "user@contoso.com" -AddRole "Contribute"
```

Adds the "Contribute" permission role to the user "user@contoso.com" in the subweb of the current web with site relative url "projectA".

### EXAMPLE 4

```powershell
Set-PnPWebPermission -User "user@contoso.com" -AddRole "Custom Role 1","Custom Role 2"
```

Adds the specified permission roles to the user "user@contoso.com" in the current web.

## PARAMETERS

### -AddRole

The name of the permission level to add to the specified user or group.

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

### -Group

The name of the group.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set group permissions
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

The guid or site relative url of the web to use.

```yaml
Type: WebPipeBind
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

The name of the permission level to remove from the specified user or group.

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

### -User

The name of the user.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set user permissions
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
