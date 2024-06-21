---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPPriviledgedIdentityManagement.html
external help file: PnP.PowerShell.dll-Help.xml
title: Enable-PnPPriviledgedIdentityManagement
---
  
# Enable-PnPPriviledgedIdentityManagement

## SYNOPSIS

**Required Permissions**

* Microsoft Graph: RoleAssignmentSchedule.ReadWrite.Directory

Temporarily enables a Privileged Identity Management role for a user

## SYNTAX

### By Role Name And Principal 

```powershell
Enable-PnPPriviledgedIdentityManagement -Role <PriviledgedIdentityManagementRolePipeBind> [-PrincipalId <Guid>] [-Justification <string>] [-StartAt <DateTime>] [-ExpireInHours <short>] [-Connection <PnPConnection>] 
```

### By Role Name And User

```powershell
Enable-PnPPriviledgedIdentityManagement -Role <PriviledgedIdentityManagementRolePipeBind> -User <AzureADUserPipeBind> [-Justification <string>] [-StartAt <DateTime>] [-ExpireInHours <short>] [-Connection <PnPConnection>] 
```

### By Eligible Role Assignment

```powershell
Enable-PnPPriviledgedIdentityManagement -EligibleAssignment <PriviledgedIdentityManagementRolePipeBind> [-Justification <string>] [-StartAt <DateTime>] [-ExpireInHours <short>] [-Connection <PnPConnection>] 
```

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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpireInHours
Indication of after how many hours the elevation should expire. If omitted, the default value is 1 hour.

```yaml
Type: short
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Justification
Text to be logged as the reason for the elevation of rights. If omitted, the default value is "Elevated by PnP PowerShell".

```yaml
Type: string
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: "Elevated by PnP PowerShell"
Accept pipeline input: False
Accept wildcard characters: False
```

### -PrincipalId
The Id of of the principal to elevate. If omitted, the default value is the current user, if the connection has been made using a delegated identity. With an application identity, this parameter is required.

```yaml
Type: Guid
Parameter Sets: By Role Name And Principal
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role
The Id, name or instance of a role to elevate the current user to. Use `Get-PnPPriviledgedIdentityManagementRole` to retrieve the available roles.

```yaml
Type: PriviledgedIdentityManagementRolePipeBind
Parameter Sets: By Role Name And Principal, By Role Name And User
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -StartAt
Date and time at which to start the elevation. If omitted, the default value is the current date and time, meaning the activation will happen immediately.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: Get-Date
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The Id, username or instance of a user which needs to be elevated

```yaml
Type: AzureADUserPipeBind
Parameter Sets: By Role Name And User
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)