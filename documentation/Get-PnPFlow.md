---
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFlow.html
Module Name: PnP.PowerShell
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
  
# Get-PnPFlow

## SYNOPSIS
Returns the flows for a given environment

## SYNTAX

```
Get-PnPFlow -Environment <PowerAutomateEnvironmentPipeBind> [-AsAdmin] [-Identity <PowerAutomateFlowPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlets returns the flows for a given enviroment.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPFlowEnvironment
Get-PnPFlow -Environment $environment
```
This returns all the flows for a given environment

### Example 2
```powershell
$environment = Get-PnPFlowEnvironment
Get-PnPFlow -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns specific flow

## PARAMETERS

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

### -Environment
The name of the environment or an Environment object to retrieve the available flows for.

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
Type: PowerAutomateFlowPipeBind
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


