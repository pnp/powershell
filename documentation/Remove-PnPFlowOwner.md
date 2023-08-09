---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFlowOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPFlowOwner
---
  
# Remove-PnPFlowOwner

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Removes owner permissions to a Power Automate flow


## SYNTAX

### By Identity and User (default)
```powershell
Remove-PnPFlowOwner [-Environment <PowerAutomateEnvironmentPipeBind>] [-Identity <PowerPlatformPipeBind>] [-User <String>] [-AsAdmin] [-Force]
```


## DESCRIPTION
This cmdlet removes owner permissions for a user to a power automate flow.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Remove-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com
```
Removes the specified user email with owner access level to the specified flow

### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Remove-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04
```
Removes the specified user id with owner access level to the specified flow

### Example 3
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Remove-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -AsAdmin
```
Removes the specified user email with owner access level to the specified flow as admin

### Example 4
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Remove-PnPFlowOwner -environment $environment -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -AsAdmin -Force
```
Removes the specified user email with owner access level to the specified flow as admin, without confirmation

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

### -Force
Specifying the Force parameter will skip the confirmation question.

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