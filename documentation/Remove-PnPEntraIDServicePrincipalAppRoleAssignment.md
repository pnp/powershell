---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPEntraIDServicePrincipalAppRoleAssignment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPEntraIDServicePrincipalAppRoleAssignment
---
  
# Remove-PnPEntraIDServicePrincipalAppRoleAssignment

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: `AppRoleAssignment.ReadWrite.All`, plus permissions to read the enterprise application and the target principal such as `Application.Read.All`, `Application.ReadWrite.All`, `Directory.Read.All`, `Directory.ReadWrite.All`, `User.Read.All`, `User.ReadWrite.All`, `Group.Read.All`, or `Group.ReadWrite.All`

Removes app role assignments configured on an enterprise application for a specific Entra ID user or group.

## SYNTAX

### By instance
```powershell
Remove-PnPEntraIDServicePrincipalAppRoleAssignment -Identity <AzureADServicePrincipalAppRoleAssignment> [-Connection <PnPConnection>]
```

### User
```powershell
Remove-PnPEntraIDServicePrincipalAppRoleAssignment -User <EntraIDUserPipeBind> [-AppRole <ServicePrincipalAvailableAppRoleBind>] [-Resource <ServicePrincipalPipeBind>] [-Connection <PnPConnection>]
```

### Group
```powershell
Remove-PnPEntraIDServicePrincipalAppRoleAssignment -Group <Object> [-AppRole <ServicePrincipalAvailableAppRoleBind>] [-Resource <ServicePrincipalPipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows removal of one or more app role assignments for a specific Entra ID user or group on an enterprise application represented by a service principal.

If `-AppRole` is omitted, all assignments for the selected user or group on the selected enterprise application are removed.

If you pipe in an app role instance retrieved through `Get-PnPEntraIDServicePrincipalAvailableAppRole`, the resource service principal is inferred automatically and you can omit `-Resource`.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPEntraIDServicePrincipalAppRoleAssignment -User "john@contoso.com" -Resource "Contoso CRM" -AppRole "Reader"
```

Removes the `Reader` enterprise app role assignment for the user `john@contoso.com` on the enterprise application `Contoso CRM`.

### EXAMPLE 2
```powershell
Remove-PnPEntraIDServicePrincipalAppRoleAssignment -Group "Sales Team" -Resource "Contoso CRM"
```

Removes all enterprise app role assignments for the group `Sales Team` on the enterprise application `Contoso CRM`.

### EXAMPLE 3
```powershell
Get-PnPEntraIDServicePrincipalAppRoleAssignment -User "john@contoso.com" -Resource "Contoso CRM" | Remove-PnPEntraIDServicePrincipalAppRoleAssignment
```

Removes the app role assignments returned from the pipeline.

### EXAMPLE 4
```powershell
Get-PnPEntraIDServicePrincipal -AppName "Contoso CRM" | Get-PnPEntraIDServicePrincipalAvailableAppRole -Identity "Reader" | Remove-PnPEntraIDServicePrincipalAppRoleAssignment -User "john@contoso.com"
```

Removes the `Reader` enterprise app role assignment for the user `john@contoso.com` on the `Contoso CRM` enterprise application.

## PARAMETERS

### -Identity
The app role assignment instance to remove. This parameter is typically supplied through the pipeline from `Get-PnPEntraIDServicePrincipalAppRoleAssignment`.

```yaml
Type: AzureADServicePrincipalAppRoleAssignment
Parameter Sets: By instance

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -User
The id, user principal name, or instance of the Entra ID user from which to remove enterprise application assignments.

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
The id, display name, or group instance of the Entra ID group from which to remove enterprise application assignments. This can be a group object returned by `Get-PnPEntraIDGroup`.

```yaml
Type: Object
Parameter Sets: Group

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -AppRole
The object id, value, or instance of the application role to remove. When an app role instance is provided through the pipeline, the resource service principal is inferred automatically.

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-delete-approleassignedto)