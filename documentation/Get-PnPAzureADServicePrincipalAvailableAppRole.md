---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADServicePrincipalAvailableAppRole.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureADServicePrincipalAvailableAppRole
---

# Get-PnPAzureADServicePrincipalAvailableAppRole

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: Any of Application.Read.All, Directory.Read.All, Application.ReadWrite.All, Directory.ReadWrite.All

Gets the available app roles available on a service principal/application registration in Azure Active Directory.

## SYNTAX

### Default (Default)

```
Get-PnPAzureADServicePrincipalAvailableAppRole -Principal <ServicePrincipalPipeBind>
 [-Identity <ServicePrincipalAppRoleBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows retrieval of all available app roles of a specific service principals/app registration in Azure Active Directory. To retrieve the app roles currently assigned to a specific service principal/application registration, use [Get-PnPAzureADServicePrincipalAssignedAppRole](Get-PnPAzureADServicePrincipalAssignedAppRole.md) instead.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureADServicePrincipalAvailableAppRole -Principal 797ee8a7-a950-4eb8-945d-7f10cc68a933
```

Retrieves all app roles of the application registration with the object Id 797ee8a7-a950-4eb8-945d-7f10cc68a933

### EXAMPLE 2

```powershell
Get-PnPAzureADServicePrincipalAvailableAppRole -Principal "My application"
```

Retrieves all app roles of the application registration with the name "My application".

### EXAMPLE 3

```powershell
Get-PnPAzureADServicePrincipal -AppId fd885e69-86dc-4f3b-851e-ad04920031cf | Get-PnPAzureADServicePrincipalAvailableAppRole
```

Retrieves all app roles of the application registration with the app Id/Client Id fd885e69-86dc-4f3b-851e-ad04920031cf

### EXAMPLE 4

```powershell
Get-PnPAzureADServicePrincipal -BuiltInType MicrosoftGraph | Get-PnPAzureADServicePrincipalAvailableAppRole -Identity "User.ReadWrite.All"
```

Retrieves the app role details of the role "User.ReadWrite.All" of the built in Microsoft Graph application registration.

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

The object id, name or instance of the application role to retrieve from the service principal/application registration.

```yaml
Type: ServicePrincipalAppRoleBind
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

The object id, name or instance of the service principal/application registration to list the app roles for.

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-list-approleassignments)
