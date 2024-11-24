---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Enable-PnPPriviledgedIdentityManagement.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Enable-PnPPriviledgedIdentityManagement
---

# Enable-PnPPriviledgedIdentityManagement

## SYNOPSIS

**Required Permissions**

* Microsoft Graph: RoleAssignmentSchedule.ReadWrite.Directory

Temporarily enables a Privileged Identity Management role for a user

## SYNTAX

### By Role Name And Principal

```
Enable-PnPPriviledgedIdentityManagement -Role <PriviledgedIdentityManagementRolePipeBind>
 [-PrincipalId <Guid>] [-Justification <string>] [-StartAt <DateTime>] [-ExpireInHours <short>]
 [-Connection <PnPConnection>]
```

### By Role Name And User

```
Enable-PnPPriviledgedIdentityManagement -Role <PriviledgedIdentityManagementRolePipeBind>
 -User <AzureADUserPipeBind> [-Justification <string>] [-StartAt <DateTime>]
 [-ExpireInHours <short>] [-Connection <PnPConnection>]
```

### By Eligible Role Assignment

```
Enable-PnPPriviledgedIdentityManagement
 -EligibleAssignment <PriviledgedIdentityManagementRolePipeBind> [-Justification <string>]
 [-StartAt <DateTime>] [-ExpireInHours <short>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Temporarily enables a Privileged Identity Management role for the provided allowing the user to perform actions that require the role. The role will be enabled starting at the specified date and time and will expire after the specified number of hours. The reason for the elevation of rights can be provided as justification.

## EXAMPLES

### Example 1

```powershell
Enable-PnPPriviledgedIdentityManagement -Role "Global Administrator"
```

Enables the global administrator role for the current user through Privileged Identity Management starting immediately and expiring in 1 hour

### Example 2

```powershell
Enable-PnPPriviledgedIdentityManagement -Role "Global Administrator" -Justification "Just because"
```

Enables the global administrator role for the current user through Privileged Identity Management starting immediately and expiring in 1 hour, adding the justification provided to be logged as the reason for the elevation of rights

### Example 3

```powershell
Enable-PnPPriviledgedIdentityManagement -Role "Global Administrator" -Justification "Just because" -StartAt (Get-Date).AddHours(2) -ExpireInHours 2
```

Enables the global administrator role for the current user through Privileged Identity Management starting in 2 hours from now and expiring 2 hours thereafter, adding the justification provided to be logged as the reason for the elevation of rights

### Example 4

```powershell
Enable-PnPPriviledgedIdentityManagement -Role "Global Administrator" -User "someone@contoso.onmicrosoft.com"
```

Enables the global administrator role for the provided user through Privileged Identity Management starting immediately and expiring in 1 hour

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

### -ExpireInHours

Indication of after how many hours the elevation should expire. If omitted, the default value is 1 hour.

```yaml
Type: short
DefaultValue: 1
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

### -Justification

Text to be logged as the reason for the elevation of rights. If omitted, the default value is "Elevated by PnP PowerShell".

```yaml
Type: string
DefaultValue: Elevated by PnP PowerShell
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

### -PrincipalId

The Id of of the principal to elevate. If omitted, the default value is the current user, if the connection has been made using a delegated identity. With an application identity, this parameter is required.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Role Name And Principal
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Role

The Id, name or instance of a role to elevate the current user to. Use `Get-PnPPriviledgedIdentityManagementRole` to retrieve the available roles.

```yaml
Type: PriviledgedIdentityManagementRolePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Role Name And Principal
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
- Name: By Role Name And User
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -StartAt

Date and time at which to start the elevation. If omitted, the default value is the current date and time, meaning the activation will happen immediately.

```yaml
Type: DateTime
DefaultValue: Get-Date
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

The Id, username or instance of a user which needs to be elevated

```yaml
Type: AzureADUserPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Role Name And User
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
