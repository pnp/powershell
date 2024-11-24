---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerShellTelemetryEnabled.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPowerShellTelemetryEnabled
---

# Get-PnPPowerShellTelemetryEnabled

## SYNOPSIS

Returns true if the PnP PowerShell Telemetry has been enabled.

## SYNTAX

### Default (Default)

```
Get-PnPPowerShellTelemetryEnabled [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

In order to help to make PnP PowerShell better, we can track anonymous telemetry. For more information on what we collect and how to prevent this data from being collected, visit [Configure PnP PowerShell](https://pnp.github.io/powershell/articles/configuration.html).

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPPowerShellTelemetryEnabled
```

Will return true or false.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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
