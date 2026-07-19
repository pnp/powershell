---
Module Name: PnP.PowerShell
title: Read-PnPTenantTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Read-PnPTenantTemplate.html
---
 
# Read-PnPTenantTemplate

## SYNOPSIS
Loads/Reads a PnP tenant template from the file system and returns an in-memory instance of this template.

## SYNTAX

### By Path (default)

```powershell
Read-PnPTenantTemplate -Path <String>
```

### By Stream

```powershell
Read-PnPTenantTemplate -Stream <Stream>
```

### By XML

```powershell
Read-PnPTenantTemplate -Xml <String>
```

## DESCRIPTION

Allows to load a PnP tenant template from the file system, from a stream or from a string to memory and return its instance object.

## EXAMPLES

### EXAMPLE 1
```powershell
Read-PnPTenantTemplate -Path template.pnp
```

Reads a PnP tenant template file from the file system and returns an in-memory instance

### EXAMPLE 2
```powershell
$template = Get-PnPFile "/sites/config/Templates/Default.xml" -AsMemoryStream
Read-PnPTenantTemplate -Stream $template
```

Downloads a PnP Tenant template from the provided location into memory and parses its contents into a TenantTemplate instance which can then be modified and passed on to the Apply-PnPTenantTemplate cmdlet without needing to write anything to disk

## PARAMETERS

### -Path
Filename to read from, optionally including full path.

```yaml
Type: String
Parameter Sets: By Path

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stream
A stream containing the PnP tenant template.

```yaml
Type: String
Parameter Sets: By Stream

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Xml
A string containing the XML of the PnP tenant template.

```yaml
Type: String
Parameter Sets: By XML

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)