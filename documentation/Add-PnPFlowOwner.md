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

### By Identity and User (default)
```powershell
Add-PnPFlowOwner [-Environment <PowerAutomateEnvironmentPipeBind>] [-Identity <PowerPlatformPipeBind>] [-User <String>] [-AsAdmin] [-RoleName <FlowUserRoleName>]
```


## DESCRIPTION
This cmdlet assigns/updates permissions for a user to a power automate flow.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Add-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -RoleName CanEdit
```
Assigns the specified useremail with 'CanEdit' access level to the specified flow

### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Add-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04 -RoleName CanView
```
Assigns the specified user id with 'CanView' access level to the specified flow

### Example 3
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Add-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04 -RoleName CanViewWithShare
```
Assigns the specified user id with 'CanViewWithShare' access level to the specified flow

### Example 4
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Add-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -AsAdmin -RoleName CanEdit
```
Assigns the specified useremail with 'CanEdit' access level to the specified flow as admin

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available flows for.

```yaml
Type: PowerAutomateEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: The default environment
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Name/Id of the flow to retrieve.

```yaml
Type: PowerPlatformPipeBind
Parameter Sets: By Identity
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
Returns the user with the provided user id or username.

```yaml
Type: String
Parameter Sets: Return by specific ID/UserName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RoleName
Allows specifying the type of access levels with which user should be added to the flow . Valid values: CanView, CanViewWithShare, CanEdit.

```yaml
Type: FlowUserRoleName
Parameter Sets: All

Required: True
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```



### -AsAdmin
If specified returns all the flows as admin. If not specified only the flows for the current user will be returned.

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