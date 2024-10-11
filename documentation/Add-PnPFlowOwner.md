---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFlowOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFlowOwner
---
  
# Add-PnPFlowOwner

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Assigns/updates permissions to a Power Automate flow

## SYNTAX

```powershell
Add-PnPFlowOwner [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerPlatformPipeBind> -User <String> -Role <FlowAccessRole> [-AsAdmin] [-Verbose]
```

## DESCRIPTION
This cmdlet assigns/updates permissions for a user to a Power Automate flow.

## EXAMPLES

### Example 1
```powershell
Add-PnPFlowOwner -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -Role CanEdit
```
Assigns the specified user with 'CanEdit' access level to the specified flow in the default environment

### Example 2
```powershell
Add-PnPFlowOwner -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04 -Role CanView
```
Assigns the specified user with 'CanView' access level to the specified flow in the default environment

### Example 3
```powershell
Add-PnPFlowOwner -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04 -Role CanViewWithShare
```
Assigns the specified user with 'CanViewWithShare' access level to the specified flow in the specified environment

### Example 4
```powershell
Add-PnPFlowOwner -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -AsAdmin -Role CanEdit
```
Assigns the specified user with 'CanEdit' access level to the specified flow as admin in the specified environment

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
The Name, Id or instance of the Power Automate Flow to add the permissions to.

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
The user principal name or Id of the user to assign permissions to the Power Automate Flow.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role
The type of permissions to assign to the user on the Power Automate Flow. Valid values: CanView, CanViewWithShare, CanEdit.

```yaml
Type: FlowUseFlowAccessRolerRoleName
Parameter Sets: (All)

Required: True
Position: Named
Default value: CanView
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsAdmin
If specified, the permission will be set as an admin. If not specified only the flows to which the current user already has access can be modified.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)