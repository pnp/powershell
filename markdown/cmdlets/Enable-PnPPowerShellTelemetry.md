---
Module Name: PnP.PowerShell
title: Enable-PnPPowerShellTelemetry
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPPowerShellTelemetry.html
---
 
# Enable-PnPPowerShellTelemetry

## SYNOPSIS
Enables sending of telemetry data.

## SYNTAX

### Enable sending of telemetry data after going through a confirmation question.

```powershell
Enable-PnPPowerShellTelemetry
```

### Enable sending of telemetry data skipping the confirmation question.

```powershell
Enable-PnPPowerShellTelemetry -Force
```
## DESCRIPTION

This cmdlet Enables sending of telemetry data.

## EXAMPLES

### EXAMPLE 1
```powershell
Enable-PnPPowerShellTelemetry
```

Enables sending of telemetry data after answering the confirmation question.

### EXAMPLE 2
```powershell
Enable-PnPPowerShellTelemetry -Force
```

Enables sending of telemetry data skipping the confirmation question.
## PARAMETERS
### -Force
Switch parameter which executes the cmdlet and skips the confirmation question.

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