---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADServicePrincipalAssignedAppRole.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPAzureADServicePrincipalAssignedAppRole
---

# Remove-PnPAzureADServicePrincipalAssignedAppRole

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: AppRoleAssignment.ReadWrite.All

Removes app roles configured on a service principal/application registration in Azure Active Directory.

## SYNTAX

### By instance

```
Remove-PnPAzureADServicePrincipalAssignedAppRole -Identity <ServicePrincipalAssignedAppRoleBind>
 [-Connection <PnPConnection>]
```

### By assigned app role

```
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal <ServicePrincipalPipeBind>
 [-Identity <ServicePrincipalAssignedAppRoleBind>] [-Connection <PnPConnection>]
```

### By app role name

```
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal <ServicePrincipalPipeBind>
 -AppRoleName <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows removal of one or more assigned app roles on a specific service principals/app registration in Azure Active Directory.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal 797ee8a7-a950-4eb8-945d-7f10cc68a933 -AppRoleName "User.ReadWrite.All"
```

Removes the app role "User.ReadWrite.All" from the application registration with the object Id 797ee8a7-a950-4eb8-945d-7f10cc68a933

### EXAMPLE 2

```powershell
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal "My application" -AppRoleName "Group.ReadWrite.All"
```

Removes the app role "Group.ReadWrite.All" from the application registration with the name "My application"

### EXAMPLE 3

```powershell
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal 797ee8a7-a950-4eb8-945d-7f10cc68a933
```

Removes all app roles from the application registration with the object Id 797ee8a7-a950-4eb8-945d-7f10cc68a933

### EXAMPLE 4

```powershell
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal "My application"
```

Removes all app roles from the application registration with the name "My application"

### EXAMPLE 5

```powershell
Get-PnPAzureADServicePrincipal -AppId fd885e69-86dc-4f3b-851e-ad04920031cf | Remove-PnPAzureADServicePrincipalAssignedAppRole
```

Removes all app roles from the application registration with the app Id/Client Id fd885e69-86dc-4f3b-851e-ad04920031cf

## PARAMETERS

### -AppRoleName

The name of the permission to remove, i.e. Sites.Read.All

```yaml
Type: ServicePrincipalAssignedAppRoleBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By app role name
  Position: Named
  IsRequired: true
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

The object id, name or instance of the application role to remove from the service principal/application registration

```yaml
Type: ServicePrincipalAssignedAppRoleBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By assigned app role
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: By instance
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Principal

The object id, name or instance of the service principal/application registration to remove app roles for

```yaml
Type: ServicePrincipalPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By assigned app role
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
- Name: By app role name
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-delete-approleassignments)
