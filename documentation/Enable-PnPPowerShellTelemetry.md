---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPPowerShellTelemetry.html
external help file: PnP.PowerShell.dll-Help.xml
title: Enable-PnPPowerShellTelemetry
---
  
# Enable-PnPPowerShellTelemetry

## SYNOPSIS
Enables PnP PowerShell telemetry tracking.

## SYNTAX

```powershell
Enable-PnPPowerShellTelemetry [-Force] [<CommonParameters>]
```

## DESCRIPTION
In order to help to make PnP PowerShell better, we can track anonymous telemetry. We track the version of the cmdlets you are using, which cmdlet you are executing and which version of SharePoint you are connecting to. Use Disable-PnPPowerShellTelemetry to turn this off, alternative, use the -NoTelemetry switch on Connect-PnPOnline to turn it off for that session.

## EXAMPLES

### EXAMPLE 1
```powershell
Enable-PnPPowerShellTelemetry
```

Will prompt you to confirm to enable telemetry tracking.

### EXAMPLE 2
```powershell
Enable-PnPPowerShellTelemetry -Force
```

Will enable telemetry tracking without prompting.

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


