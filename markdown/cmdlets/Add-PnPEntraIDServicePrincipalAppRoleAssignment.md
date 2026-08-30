---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPEntraIDServicePrincipalAppRoleAssignment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPEntraIDServicePrincipalAppRoleAssignment
---
  
# Add-PnPEntraIDServicePrincipalAppRoleAssignment

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: `AppRoleAssignment.ReadWrite.All`, plus permissions to read the service principal and target principal such as `Application.Read.All`, `Application.ReadWrite.All`, `Directory.Read.All`, `Directory.ReadWrite.All`, `User.Read.All`, `User.ReadWrite.All`, `Group.Read.All`, or `Group.ReadWrite.All`

Assigns an Entra ID user or supported group, such as a security-enabled group or Microsoft 365 group, to an enterprise application, optionally with a specific app role.

## SYNTAX

### User

```powershell
Add-PnPEntraIDServicePrincipalAppRoleAssignment -User <EntraIDUserPipeBind> [-AppRole <ServicePrincipalAvailableAppRoleBind>] [-Resource <ServicePrincipalPipeBind>] [-Connection <PnPConnection>]
```

### Group

```powershell
Add-PnPEntraIDServicePrincipalAppRoleAssignment -Group <EntraIDGroupPipeBind> [-AppRole <ServicePrincipalAvailableAppRoleBind>] [-Resource <ServicePrincipalPipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows assigning an Entra ID user or supported group to an enterprise application represented by a service principal. If the enterprise application exposes one or more user-targeted app roles, provide the app role to assign through `-AppRole`. If it does not expose user-targeted app roles, the cmdlet assigns the default access role automatically.

If you pipe in an app role instance retrieved through `Get-PnPEntraIDServicePrincipalAvailableAppRole`, the owning service principal is inferred automatically and you can omit `-Resource`.

Supported groups include security-enabled groups and Microsoft 365 groups.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPEntraIDServicePrincipalAppRoleAssignment -User "john@contoso.com" -Resource "Contoso CRM" -AppRole "Reader"
```

Assigns the user `john@contoso.com` to the enterprise application `Contoso CRM` with the `Reader` app role.

### EXAMPLE 2
```powershell
Add-PnPEntraIDServicePrincipalAppRoleAssignment -Group "Sales Team" -Resource "Contoso CRM" -AppRole "Approver"
```

Assigns the group `Sales Team` to the enterprise application `Contoso CRM` with the `Approver` app role.

### EXAMPLE 3
```powershell
Get-PnPEntraIDServicePrincipal -AppName "Contoso CRM" | Get-PnPEntraIDServicePrincipalAvailableAppRole -Identity "Reader" | Add-PnPEntraIDServicePrincipalAppRoleAssignment -User "john@contoso.com"
```

Retrieves the `Reader` app role from the `Contoso CRM` enterprise application and assigns it to the user `john@contoso.com`.

### EXAMPLE 4
```powershell
Add-PnPEntraIDServicePrincipalAppRoleAssignment -Group "6d4d2db8-6f2a-49b4-bc46-c5d43a91d47f" -Resource "Contoso Intranet"
```

Assigns the group with the provided id to the `Contoso Intranet` enterprise application using the default access role. This works when the application does not expose any user-targeted app roles.

## PARAMETERS

### -User
The id, user principal name, or instance of the Entra ID user to assign to the enterprise application.

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
The id, display name, or group instance of the Entra ID group to assign to the enterprise application. This can be a group object returned by `Get-PnPEntraIDGroup`.

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
The object id, value, or instance of the application role to assign. If omitted, the cmdlet will assign the default access role when the enterprise application does not expose any user-targeted app roles. When you pipe in an app role instance, the resource service principal is inferred automatically.

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-post-approleassignedto)