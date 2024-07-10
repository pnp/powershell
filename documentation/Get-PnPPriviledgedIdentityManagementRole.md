---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPriviledgedIdentityManagementRole.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPriviledgedIdentityManagementRole
---
  
# Get-PnPPriviledgedIdentityManagementRole

## SYNOPSIS

**Required Permissions**

* Microsoft Graph: RoleManagement.Read.Directory

Retrieve the available Privileged Identity Management roles that exist within the tenant

## SYNTAX

```powershell
Get-PnPPriviledgedIdentityManagementRole [-Identity <PriviledgedIdentityManagementRolePipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Retrieve the available Privileged Identity Management roles that exist within the tenant. These are the roles to which elevation can take place.

## EXAMPLES

### Example 1
```powershell
Get-PnPPriviledgedIdentityManagementRole
```

Retrieves the available Privileged Identity Management roles

### Example 2
```powershell
Get-PnPPriviledgedIdentityManagementRole -Identity "Global Administrator"
```

Retrieves the Privileged Identity Management with the provided name

### Example 3
```powershell
Get-PnPPriviledgedIdentityManagementRole -Identity 62e90394-69f5-4237-9190-012177145e10
```

Retrieves the Privileged Identity Management role with the provided id

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The name, id or instance of a Priviledged Identity Management role to retrieve the details of

```yaml
Type: PriviledgedIdentityManagementRolePipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: True
Accept pipeline input: True
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)