---
Module Name: PnP.PowerShell
title: New-PnPExtensibilityHandlerObject
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPExtensibilityHandlerObject.html
---
 
# New-PnPExtensibilityHandlerObject

## SYNOPSIS
Creates an ExtensibilityHandler Object, to be used by the Get-PnPSiteTemplate cmdlet

## SYNTAX

```powershell
New-PnPExtensibilityHandlerObject [-Assembly] <String> -Type <String> [-Configuration <String>] [-Disabled]
 
```

## DESCRIPTION

Allows to create an ExtensibilityHandler.

## EXAMPLES

### EXAMPLE 1
```powershell
$handler = New-PnPExtensibilityHandlerObject -Assembly Contoso.Core.Handlers -Type Contoso.Core.Handlers.MyExtensibilityHandler
Get-PnPSiteTemplate -Out NewTemplate.xml -ExtensibilityHandlers $handler
```

This will create a new ExtensibilityHandler object that is run during extraction of the template

## PARAMETERS

### -Assembly
The full assembly name of the handler

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Configuration
Any configuration data you want to send to the handler

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Disabled
If set, the handler will be disabled

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
The type of the handler

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

