---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disable-PnPPowerShellTelemetry.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disable-PnPPowerShellTelemetry
---
  
# Disable-PnPPowerShellTelemetry

## SYNOPSIS
Disables PnP PowerShell telemetry tracking

## SYNTAX

```powershell
Disable-PnPPowerShellTelemetry [-Force] [<CommonParameters>]
```

## DESCRIPTION
Disables PnP PowerShell telemetry tracking

## EXAMPLES

### EXAMPLE 1
```powershell
Disable-PnPPowerShellTelemetry
```

Will prompt you to confirm to disable telemetry tracking.

### EXAMPLE 2
```powershell
Disable-PnPPowerShellTelemetry -Force
```

Will disable telemetry tracking without prompting.

## PARAMETERS

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


