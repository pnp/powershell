---
Module Name: PnP.PowerShell
title: Set-PnPAppSideLoading
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPAppSideLoading.html
---
 
# Set-PnPAppSideLoading

## SYNOPSIS
Enables the App SideLoading Feature on a site

## SYNTAX

### On
```powershell
Set-PnPAppSideLoading [-On] [-Connection <PnPConnection>] 
```

### Off
```powershell
Set-PnPAppSideLoading [-Off] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet enables or disables the app side loading feature on the site.

Allows to enable the App SideLoading Feature on a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAppSideLoading -On
```

This will turn on App side loading.

### EXAMPLE 2
```powershell
Set-PnPAppSideLoading -Off
```

This will turn off App side loading.

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

### -Off
Turns the feature off.

```yaml
Type: SwitchParameter
Parameter Sets: Off

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -On
Turns the feature on.

```yaml
Type: SwitchParameter
Parameter Sets: On

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

