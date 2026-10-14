---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEntraIDServicePrincipalAppRoleAssignment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPEntraIDServicePrincipalAppRoleAssignment
---
  
# Get-PnPEntraIDServicePrincipalAppRoleAssignment

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: permissions to read the enterprise application, such as `Application.Read.All`, `Application.ReadWrite.All`, `Directory.Read.All`, or `Directory.ReadWrite.All`. Depending on how you identify the target principal, additional permissions such as `User.Read.All`, `User.ReadWrite.All`, `Group.Read.All`, or `Group.ReadWrite.All` can be required.

Gets app role assignments configured on an enterprise application for a specific Entra ID user or group.

## SYNTAX

### User
```powershell
Get-PnPEntraIDServicePrincipalAppRoleAssignment -User <EntraIDUserPipeBind> [-AppRole <ServicePrincipalAvailableAppRoleBind>] [-Resource <ServicePrincipalPipeBind>] [-Connection <PnPConnection>]
```

### Group
```powershell
Get-PnPEntraIDServicePrincipalAppRoleAssignment -Group <EntraIDGroupPipeBind> [-AppRole <ServicePrincipalAvailableAppRoleBind>] [-Resource <ServicePrincipalPipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows retrieval of app role assignments for a specific Entra ID user or group on an enterprise application represented by a service principal.

If you pipe in an app role instance retrieved through `Get-PnPEntraIDServicePrincipalAvailableAppRole`, the resource service principal is inferred automatically and you can omit `-Resource`.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEntraIDServicePrincipalAppRoleAssignment -User "john@contoso.com" -Resource "Contoso CRM"
```

Retrieves all enterprise app role assignments for the user `john@contoso.com` on the enterprise application `Contoso CRM`.

### EXAMPLE 2
```powershell
Get-PnPEntraIDServicePrincipalAppRoleAssignment -Group "Sales Team" -Resource "Contoso CRM"
```

Retrieves all enterprise app role assignments for the group `Sales Team` on the enterprise application `Contoso CRM`.

### EXAMPLE 3
```powershell
Get-PnPEntraIDServicePrincipal -AppName "Contoso CRM" | Get-PnPEntraIDServicePrincipalAvailableAppRole -Identity "Reader" | Get-PnPEntraIDServicePrincipalAppRoleAssignment -User "john@contoso.com"
```

Retrieves the `Reader` enterprise app role assignment for the user `john@contoso.com` on the `Contoso CRM` enterprise application.

## PARAMETERS

### -User
The id, user principal name, or instance of the Entra ID user for which to retrieve enterprise application assignments.

```yaml
Type: EntraIDUserPipeBind
Parameter Sets: User

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
The id, display name, or group instance of the Entra ID group for which to retrieve enterprise application assignments. This can be a group object returned by `Get-PnPEntraIDGroup`.

```yaml
Type: EntraIDGroupPipeBind
Parameter Sets: Group

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -AppRole
The object id, value, or instance of the application role to filter the assignments on. When an app role instance is provided through the pipeline, the resource service principal is inferred automatically.

```yaml
Type: ServicePrincipalAvailableAppRoleBind
Parameter Sets: User, Group

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Resource
The object id, name, or instance of the enterprise application service principal. This parameter can be omitted when an app role instance is provided through the pipeline.

```yaml
Type: ServicePrincipalPipeBind
Parameter Sets: User, Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-list-approleassignedto)