---
Module Name: PnP.PowerShell
title: Get-PnPPowerShellTelemetryEnabled
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerShellTelemetryEnabled.html
---
 
# Get-PnPPowerShellTelemetryEnabled

## SYNOPSIS
Returns true if the PnP PowerShell Telemetry has been enabled.

## SYNTAX

```powershell
Get-PnPPowerShellTelemetryEnabled [-Connection <PnPConnection>] [<CommonParameters>]
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

