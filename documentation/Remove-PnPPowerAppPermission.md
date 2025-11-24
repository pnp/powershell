---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPowerAppPermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPPowerAppPermission
---
  
# Remove-PnPPowerAppPermission

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com  
* PowerApps: service.powerapps.com
* Microsoft Graph: User.Read.All, Group.Read.All

Removes user, group and (Everyone in organization) permissions from a Power App


## SYNTAX

```powershell
Remove-PnPPowerAppPermission [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerPlatformPipeBind> [-User <String>] [-Group <String>] [-Tenant] [-AsAdmin] [-Force] [-Verbose]
```

## DESCRIPTION
This cmdlet removes user, group, or (Everyone in organization) permissions from a PowerApp using the -User, -Group, or -Tenant parameter. Only one of these parameters can be specified at a time, and at least one must be provided.

## EXAMPLES

### Example 1
```powershell
Remove-PnPPowerAppPermission -Identity 9b2f87e6-4c3d-48c0-a2b6-c1b4e3e57f0f -User username@tenant.onmicrosoft.com
```
Removes the specified user permission from the specified PowerApp located in the default environment

### Example 2
```powershell
Remove-PnPPowerAppPermission -Identity 9b2f87e6-4c3d-48c0-a2b6-c1b4e3e57f0f -User 6844c04a-8ee7-40ad-af66-28f6e948cd04
```
Removes the specified user permission from the specified PowerApp located in the default environment

### Example 3
```powershell
Remove-PnPPowerAppPermission (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity 9b2f87e6-4c3d-48c0-a2b6-c1b4e3e57f0f -User username@tenant.onmicrosoft.com -AsAdmin
```
Removes the specified user permission from the specified PowerApp as an admin in the specified environment

### Example 4
```powershell
Remove-PnPPowerAppPermission (Get-PnPPowerPlatformEnvironment -Identity "myenvironment) -Identity 9b2f87e6-4c3d-48c0-a2b6-c1b4e3e57f0f -User username@tenant.onmicrosoft.com -AsAdmin -Force
```
Removes the specified user permission from the specified PowerApp as admin, without asking for confirmation, in the specified environment

### Example 5
```powershell
Remove-PnPPowerAppPermission -Identity "3f4a2c1d-0e9d-4c1e-8b55-9e3c7f0ba7e2" -Group "c6c4b4e0-cd72-4d64-8ec2-cfbd0388ec16" -Force
```
Removes the specified group's permission for the PowerApp without prompting using group id

### Example 6
```powershell
Remove-PnPPowerAppPermission -Identity "3f4a2c1d-0e9d-4c1e-8b55-9e3c7f0ba7e2" -Group "Finance Team"
```
Removes the specified group's permission for the PowerApp using group's display name

### Example 7
```powershell
Remove-PnPPowerAppPermission -Identity "3f4a2c1d-0e9d-4c1e-8b55-9e3c7f0ba7e2" -Tenant
```
Removes the (Everyone in organization) permission for the PowerApp using -Tenant parameter

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment instance. If omitted, the default environment will be used.

```yaml
Type: PowerPlatformEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: The default environment
Accept pipeline input: True
Accept wildcard characters: False
```

### -Identity
The Name, Id or instance of the PowerApp to add the permissions to.

```yaml
Type: PowerPlatformPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The user principal name or Id of the user to remove its permissions from the PowerApp.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
The group display name or Id of the group to remove its permissions from the PowerApp.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tenant
The (Everyone in organization) permission to remove from the PowerApp

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsAdmin
If specified, the permission will be removed as an admin. If not specified only the Apps to which the current user already has access can be modified.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Providing the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)