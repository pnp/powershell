---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPriviledgedIdentityManagementEligibleAssignment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPriviledgedIdentityManagementEligibleAssignment
---
  
# Get-PnPPriviledgedIdentityManagementEligibleAssignment

## SYNOPSIS

**Required Permissions**

* Microsoft Graph: RoleAssignmentSchedule.Read.Directory

Retrieve the available Privileged Identity Management eligibility assignment roles that exist within the tenant

## SYNTAX

```powershell
Get-PnPPriviledgedIdentityManagementEligibleAssignment [-Identity <PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Retrieve the available Privileged Identity Management eligibility assignment roles that exist within the tenant. These are the configured users with the configured roles they can be elevated to.

## EXAMPLES

### Example 1
```powershell
Get-PnPPriviledgedIdentityManagementEligibleAssignment
```

Retrieves the available Privileged Identity Management eligibility assignment roles 

### Example 2
```powershell
Get-PnPPriviledgedIdentityManagementEligibleAssignment -Identity 62e90394-69f5-4237-9190-012177145e10
```

Retrieves the Privileged Identity Management eligibility assignment role with the provided id

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
The name, id or instance of a Priviledged Identity Management eligibility assignment role to retrieve the details of

```yaml
Type: PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind
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