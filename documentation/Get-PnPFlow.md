---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFlow.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFlow
---
  
# Get-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the flows for a given environment

## SYNTAX

```powershell
Get-PnPFlow -Environment <PowerAutomateEnvironmentPipeBind> [-AsAdmin] [-Identity <PowerPlatformPipeBind>] 
[-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet returns the flows for a given enviroment.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PnPFlow -Environment $environment
```
This returns all the flows for a given Power Platform environment

### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PnPFlow -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns a specific flow

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available flows for.

```yaml
Type: PowerAutomateEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Name/Id of the flow to retrieve.

```yaml
Type: PowerPlatformPipeBind
Parameter Sets: (All)
Aliases:

Required: False
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


