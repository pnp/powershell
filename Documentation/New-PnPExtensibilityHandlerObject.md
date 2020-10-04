---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnpextensibilityhandlerobject
schema: 2.0.0
title: New-PnPExtensibilityHandlerObject
---

# New-PnPExtensibilityHandlerObject

## SYNOPSIS
Creates an ExtensibilityHandler Object, to be used by the Get-PnPSiteTemplate cmdlet

## SYNTAX

```
New-PnPExtensibilityHandlerObject [-Assembly] <String> -Type <String> [-Configuration <String>] [-Disabled]
 [<CommonParameters>]
```

## DESCRIPTION

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
Aliases:

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
Aliases:

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
Aliases:

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
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### OfficeDevPnP.Core.Framework.Provisioning.Model.ExtensibilityHandler

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)