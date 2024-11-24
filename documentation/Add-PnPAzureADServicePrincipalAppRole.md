---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPAzureADServicePrincipalAppRole.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPAzureADServicePrincipalAppRole
---

# Add-PnPAzureADServicePrincipalAppRole

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: AppRoleAssignment.ReadWrite.All and Application.Read.All or AppRoleAssignment.ReadWrite.All and Directory.Read.All

Adds an app role to a service principal/application registration in Azure Active Directory.

## SYNTAX

### By built in type

```
Add-PnPAzureADServicePrincipalAppRole -Principal <ServicePrincipalPipeBind>
 -AppRole <ServicePrincipalAppRoleBind> -BuiltInType <ServicePrincipalBuiltInType>
 [-Connection <PnPConnection>]
```

### By resource

```
Add-PnPAzureADServicePrincipalAppRole -Principal <ServicePrincipalPipeBind>
 -AppRole <ServicePrincipalAppRoleBind> -Resource <ServicePrincipalPipeBind>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows adding of an app role such as Sites.FullControl.All to a service principal/application registration in Azure Active Directory. This can be used to grant permissions to a service principal/application registration, such as a Managed Identity.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPAzureADServicePrincipalAppRole -Principal "62614f96-cb78-4534-bf12-1f6693e8237c" -AppRole "Directory.Read.All" -BuiltInType MicrosoftGraph
```

Adds the permission Directory.Read.All for Microsoft Graph to the service principal with the object id 62614f96-cb78-4534-bf12-1f6693e8237c

### EXAMPLE 2

```powershell
Get-PnPAzureADServicePrincipal -BuiltInType SharePointOnline | Get-PnPAzureADServicePrincipalAvailableAppRole -Identity "Sites.FullControl.All" | Add-PnPAzureADServicePrincipalAppRole -Principal "62614f96-cb78-4534-bf12-1f6693e8237c"
```

Adds the permission Site.FullControl.All for SharePoint Online to the service principal with the object id 62614f96-cb78-4534-bf12-1f6693e8237c

### EXAMPLE 3

```powershell
Get-PnPAzureADServicePrincipal -BuiltInType MicrosoftGraph | Get-PnPAzureADServicePrincipalAvailableAppRole -Identity "Group.ReadWrite.All" | Add-PnPAzureADServicePrincipalAppRole -Principal "mymanagedidentity"
```

Adds the permission Group.ReadWrite.All for Microsoft Graph to the service principal with the name mymanagedidentity.

### EXAMPLE 4

```powershell
Add-PnPAzureADServicePrincipalAppRole -Principal "62614f96-cb78-4534-bf12-1f6693e8237c" -AppRole "MyApplication.Read" -Resource "b8c2a8aa-33a0-43f4-a9d3-fe2851c5293e"
```

Adds the permission MyApplication.Read for the application registration with object id b8c2a8aa-33a0-43f4-a9d3-fe2851c5293e to the service principal with the object id 62614f96-cb78-4534-bf12-1f6693e8237c

## PARAMETERS

### -AppRole

The object id, name or instance of the application role to add to the service principal/application registration.

```yaml
Type: ServicePrincipalAppRoleBind
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

### -BuiltInType

The built in application type to use for the app role. This can be MicrosoftGraph or SharePointOnline.

```yaml
Type: ServicePrincipalAppRoleBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By built in type
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

### -Principal

The object id, name or instance of the service principal/application registration to add the app role to.

```yaml
Type: ServicePrincipalPipeBind
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

### -Resource

The object id, name or instance of the application to which the role belongs you wish to add to the service principal/application registration. If omitted, it will try to define the owning service principal from the passed in AppRole.

```yaml
Type: ServicePrincipalAppRoleBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By resource
  Position: Named
  IsRequired: false
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-post-approleassignments)
