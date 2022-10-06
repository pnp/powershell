---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disable-PnPPowerShellTelemetry.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disable-PnPPowerShellTelemetry
---
  
## Disable-PnPPowerShellTelemetry

### SYNOPSIS

Disables telemetry collection by PnP PowerShell.

### SYNTAX

```powershell
Disable-PnPPowerShellTelemetry [-Force <SwitchParameter>]
```

## DESCRIPTION

The cmdlet disables the telemetry that is sent to application insights by PnP PowerShell.

## EXAMPLES

### EXAMPLE 1

```powershell
Disable-PnPPowerShellTelemetry
```

This will disable the telemetry collection after asking a confirmation question.

### EXAMPLE 2

```powershell
Disable-PnPPowerShellTelemetry -Force
```

This will disable the telemetry collection skipping the confirmation question.

## PARAMETERS

### -Force

Specifies whether to continue without waiting for confirmation.

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
