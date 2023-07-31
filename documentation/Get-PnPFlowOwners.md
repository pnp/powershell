---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFlowOwners.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFlowOwners
---
  
# Get-PnPFlowOwners

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the flows for a given environment

## SYNTAX

```powershell
Get-PnPFlowOwners [-Environment <PowerAutomateEnvironmentPipeBind>] [-Identity <PowerPlatformPipeBind>] [-AsAdmin] 
[-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet returns the flowowners for a given flow for a given environment.

## EXAMPLES

### Example 1
```powershell
Get-PnPPowerPlatformEnvironment -Identity "MyOrganization (default)" | Get-PnPFlowOwners 
[-Environment <PowerAutomateEnvironmentPipeBind>] [-Identity <PowerPlatformPipeBind>] [-AsAdmin]
```
This returns all the owners of the given flow for a given Power Platform environment


## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the owners of the flow.

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
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsAdmin
If specified returns the owners of the given flow as admin. If not specified only the flows for the current user will be targeted, and returns the owners of the targetted flow.

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

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

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