---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Convert-PnPSiteTemplate.html
external help file: PnP.PowerShell.dll-Help.xml
title: Convert-PnPSiteTemplate
---
  
# Convert-PnPSiteTemplate

## SYNOPSIS
Converts a provisioning template to an other schema version

## SYNTAX

```powershell
Convert-PnPSiteTemplate [-Path] <String> [-Out <String>] [[-ToSchema] <XMLPnPSchemaVersion>]
 [-Encoding <Encoding>] [-Force] 
```

## DESCRIPTION

Allows to convert a provisioning template to an other schema version.

## EXAMPLES

### EXAMPLE 1
```powershell
Convert-PnPSiteTemplate -Path template.xml
```

Converts a provisioning template to the latest schema and outputs the result to current console.

### EXAMPLE 2
```powershell
Convert-PnPSiteTemplate -Path template.xml -Out newtemplate.xml
```

Converts a provisioning template to the latest schema and outputs the result the newtemplate.xml file.

### EXAMPLE 3
```powershell
Convert-PnPSiteTemplate -Path template.xml -Out newtemplate.xml -ToSchema V201512
```

Converts a provisioning template to the latest schema using the 201512 schema and outputs the result the newtemplate.xml file.

## PARAMETERS

### -Encoding
The encoding type of the XML file, Unicode is default

```yaml
Type: Encoding
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overwrites the output file if it exists

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Out
Filename to write to, optionally including full path

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Path to the xml file containing the site template

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -ToSchema
The schema of the output to use, defaults to the latest schema

```yaml
Type: XMLPnPSchemaVersion
Parameter Sets: (All)
Accepted values: LATEST, V201503, V201505, V201508, V201512, V201605, V201705, V201801, V201805, V201807, V201903, V201909, V202002

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Encoding documentation](https://learn.microsoft.com/dotnet/api/system.text.encoding?view=net-6.0)