---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPriviledgedIdentityManagementRoles.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPriviledgedIdentityManagementRoles
---
  
# Get-PnPPriviledgedIdentityManagementRoles

## SYNOPSIS

**Required Permissions**

* Microsoft Graph: RoleEligibilitySchedule.Read.Directory

Retrieve the available Privileged Identity Management roles for the current user

## SYNTAX

```powershell
Get-PnPPriviledgedIdentityManagementRoles [-Connection <PnPConnection>] 
```

## DESCRIPTION
Retrieve the available Privileged Identity Management roles for the current user

## EXAMPLES

### Example 1
```powershell
Get-PnPPriviledgedIdentityManagementRoles
```

Retrieves the available Privileged Identity Management roles for the current user

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)