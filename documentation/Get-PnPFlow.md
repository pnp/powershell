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

Returns Power Automate Flows

## SYNTAX

### All (Default)
```powershell
Get-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-AsAdmin] [-SharingStatus <FlowSharingStatus>] [-Connection <PnPConnection>] [-Verbose]
```

### By Identity
```powershell
Get-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-AsAdmin] [-Identity <PowerPlatformPipeBind>] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet returns Power Automate Flows meeting the specified criteria.

## EXAMPLES

### Example 1
```powershell
Get-PnPFlow -AsAdmin
```
Returns all the flows in the default Power Platform environment belonging to any user

### Example 2
```powershell
Get-PnPPowerPlatformEnvironment -Identity "MyOrganization (default)" | Get-PnPFlow
```
Returns all the flows for a given Power Platform environment belonging to the current user

### Example 3
```powershell
Get-PnPFlow -SharingStatus SharedWithMe
```
Returns all the flows which have been shared with the current user in the default Power Platform environment

### Example 4
```powershell
Get-PnPFlow -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
Returns a specific flow from the default Power Platform environment

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available flows for.

```yaml
Type: PowerAutomateEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: False
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

### -SharingStatus
Allows specifying the type of Power Automate Flows that should be returned. Valid values: All, SharedWithMe, Personal.

```yaml
Type: FlowSharingStatus
Parameter Sets: All

Required: False
Position: Named
Default value: All
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