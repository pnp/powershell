---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Enable-PnPPowerShellTelemetry.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Enable-PnPPowerShellTelemetry
---

# Enable-PnPPowerShellTelemetry

## SYNOPSIS

Enables sending of telemetry data.

## SYNTAX

### Enable sending of telemetry data after going through a confirmation question.

```
Enable-PnPPowerShellTelemetry
```

### Enable sending of telemetry data skipping the confirmation question.

```
Enable-PnPPowerShellTelemetry -Force
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
