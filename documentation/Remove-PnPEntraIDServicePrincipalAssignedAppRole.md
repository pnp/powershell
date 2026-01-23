---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADServicePrincipalAssignedAppRole.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPAzureADServicePrincipalAssignedAppRole
---
  
# Remove-PnPAzureADServicePrincipalAssignedAppRole

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: AppRoleAssignment.ReadWrite.All

Removes app roles configured on a service principal/application registration in Azure Active Directory.

## SYNTAX


### By instance
```powershell
Remove-PnPAzureADServicePrincipalAssignedAppRole -Identity <ServicePrincipalAssignedAppRoleBind> [-Connection <PnPConnection>]
```

### By assigned app role
```powershell
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal <ServicePrincipalPipeBind> [-Identity <ServicePrincipalAssignedAppRoleBind>] [-Connection <PnPConnection>]
```

### By app role name
```powershell
Remove-PnPAzureADServicePrincipalAssignedAppRole -Principal <ServicePrincipalPipeBind> -AppRoleName <String> [-Connection <PnPConnection>]
```

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

### -Principal
The object id, name or instance of the service principal/application registration to remove app roles for

```yaml
Type: ServicePrincipalPipeBind
Parameter Sets: By assigned app role, By app role name

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Identity
The object id, name or instance of the application role to remove from the service principal/application registration

```yaml
Type: ServicePrincipalAssignedAppRoleBind
Parameter Sets: By assigned app role, By instance

Required: True (By instance), False (By assigned app role)
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AppRoleName
The name of the permission to remove, i.e. Sites.Read.All

```yaml
Type: ServicePrincipalAssignedAppRoleBind
Parameter Sets: By app role name

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-delete-approleassignments)