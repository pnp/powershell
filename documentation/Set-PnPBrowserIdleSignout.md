---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPBrowserIdleSignOut.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPBrowserIdleSignOut
---

# Set-PnPBrowserIdleSignOut

## SYNOPSIS

Sets the current configuration values for Idle session sign-out policy.

## SYNTAX

### Enable

```
Set-PnPBrowserIdleSignOut -Enabled:$true -WarnAfter <TimeSpan> -SignOutAfter <TimeSpan>
```

### Disable

```
Set-PnPBrowserIdleSignOut -Enabled:$false
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Use this cmdlet to set the current configuration values for Idle session sign-out, the time at which users are warned and subsequently signed out of Microsoft 365 after a period of browser inactivity in SharePoint Online and OneDrive.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPBrowserIdleSignOut -Enabled:$true -WarnAfter "0.00:45:00" -SignOutAfter "0.01:00:00"
```
This example enables the browser idle sign-out policy, sets a warning at 45 minutes and signs out users after a period of 60 minutes of browser inactivity.

### EXAMPLE 2

```powershell
Set-PnPBrowserIdleSignOut -Enabled:$true -WarnAfter (New-TimeSpan -Minutes 45) -SignOutAfter (New-TimeSpan -Hours 1)
```
This example enables the browser idle sign-out policy, sets a warning at 45 minutes and signs out users after a period of 60 minutes of browser inactivity.

### EXAMPLE 3

```powershell
Set-PnPBrowserIdleSignOut -Enabled:$false
```
This example disables the browser idle sign-out policy.

## PARAMETERS

### -Enabled

Enables the browser idle sign-out policy.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: DisableBrowserIdleSignOut
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: EnableBrowserIdleSignOut
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SignOutAfter

Specifies a time interval.
This parameter is used to specify a time value for **Get-PnPBrowserIdleSignOut** parameters such as *SignOutAfter*.
Specify the time interval in the following format:

\[-\]D.H:M:S.F

where:

- D = Days (0 to 10675199)
- H = Hours (0 to 23)
- M = Minutes (0 to 59)
- S = Seconds (0 to 59)
- F = Fractions of a second (0 to 9999999)

```yaml
Type: TimeSpan
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: EnableBrowserIdleSignOut
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -WarnAfter

Specifies a time interval.
This parameter is used to specify a time value for **Get-PnPBrowserIdleSignOut** parameters such as *WarnAfter*.
Specify the time interval in the following format:

\[-\]D.H:M:S.F

where:

- D = Days (0 to 10675199)
- H = Hours (0 to 23)
- M = Minutes (0 to 59)
- S = Seconds (0 to 59)
- F = Fractions of a second (0 to 9999999)

```yaml
Type: TimeSpan
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: EnableBrowserIdleSignOut
  Position: Named
  IsRequired: true
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
