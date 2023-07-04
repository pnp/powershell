---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPAzureADServicePrincipalAppRole.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPAzureADServicePrincipalAppRole
---
  
# Add-PnPAzureADServicePrincipalAppRole

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: AppRoleAssignment.ReadWrite.All and Application.Read.All or AppRoleAssignment.ReadWrite.All and Directory.Read.All

Adds an app role to a service principal/application registration in Azure Active Directory.

## SYNTAX

### By built in type

```powershell
Add-PnPAzureADServicePrincipalAppRole -Principal <ServicePrincipalPipeBind> -AppRole <ServicePrincipalAppRoleBind> -BuiltInType <ServicePrincipalBuiltInType> [-Connection <PnPConnection>]
```

### By resource 

```powershell
Add-PnPAzureADServicePrincipalAppRole -Principal <ServicePrincipalPipeBind> -AppRole <ServicePrincipalAppRoleBind> -Resource <ServicePrincipalPipeBind> [-Connection <PnPConnection>]
```

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

### -Principal
The object id, name or instance of the service principal/application registration to add the app role to.

```yaml
Type: ServicePrincipalPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -AppRole
The object id, name or instance of the application role to add to the service principal/application registration.

```yaml
Type: ServicePrincipalAppRoleBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -BuiltInType
The built in application type to use for the app role. This can be MicrosoftGraph or SharePointOnline.

```yaml
Type: ServicePrincipalAppRoleBind
Parameter Sets: By built in type

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Resource
The object id, name or instance of the application to which the role belongs you wish to add to the service principal/application registration. If omitted, it will try to define the owning service principal from the passed in AppRole.

```yaml
Type: ServicePrincipalAppRoleBind
Parameter Sets: By resource

Required: False
Position: Named
Default value: None
Accept pipeline input: True
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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-post-approleassignments)