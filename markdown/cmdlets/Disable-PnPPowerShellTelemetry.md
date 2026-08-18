---
Module Name: PnP.PowerShell
title: Disable-PnPPowerShellTelemetry
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Disable-PnPPowerShellTelemetry.html
---
 
# Disable-PnPPowerShellTelemetry

## SYNOPSIS
Disables sending of telemetry data.

## SYNTAX

### Disable sending of telemetry data after going through a confirmation question.

```powershell
Disable-PnPPowerShellTelemetry
```

### Disable sending of telemetry data skipping the confirmation question.

```powershell
Disable-PnPPowerShellTelemetry -Force
```
## DESCRIPTION

This cmdlet disables sending of telemetry data.

## EXAMPLES

### EXAMPLE 1
```powershell
Disable-PnPPowerShellTelemetry
```

Disables sending to telemetry data after answering the confirmation question.

### EXAMPLE 2
```powershell
Disable-PnPPowerShellTelemetry -Force
```

Disables sending to telemetry data skipping the confirmation question.
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