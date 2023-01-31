---
Module Name: PnP.PowerShell
title: Set-PnPBrowserIdleSignOut
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPBrowserIdleSignOut.html
---
 
# Set-PnPBrowserIdleSignOut

## SYNOPSIS
Sets the current configuration values for Idle session sign-out policy.

## SYNTAX

### Enable
```powershell
Set-PnPBrowserIdleSignOut -Enabled:$true -WarnAfter <TimeSpan> -SignOutAfter <TimeSpan>
```

### Disable
```powershell
Set-PnPBrowserIdleSignOut -Enabled:$false
```

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
Parameter Sets: DisableBrowserIdleSignout, EnableBrowserIdleSignout

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
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
Parameter Sets: EnableBrowserIdleSignout

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
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
Parameter Sets: EnableBrowserIdleSignout

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)