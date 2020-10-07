---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpappsideloading
schema: 2.0.0
title: Set-PnPAppSideLoading
---

# Set-PnPAppSideLoading

## SYNOPSIS
Enables the App SideLoading Feature on a site

## SYNTAX

### On
```
Set-PnPAppSideLoading [-On] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Off
```
Set-PnPAppSideLoading [-Off] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAppSideLoading -On
```

This will turn on App side loading

### EXAMPLE 2
```powershell
Set-PnPAppSideLoading -Off
```

This will turn off App side loading

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Off

```yaml
Type: SwitchParameter
Parameter Sets: Off
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -On

```yaml
Type: SwitchParameter
Parameter Sets: On
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)