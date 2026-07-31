---
Module Name: PnP.PowerShell
title: Read-PnPSiteTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Read-PnPSiteTemplate.html
---
 
# Read-PnPSiteTemplate

## SYNOPSIS
Loads/Reads a PnP file from the file system, string or stream

## SYNTAX

### By Path
```powershell
Read-PnPSiteTemplate [-Path] <String> [-TemplateProviderExtensions <ITemplateProviderExtension[]>] 
```

### By XML
```powershell
Read-PnPSiteTemplate [-Xml] <String> [-TemplateProviderExtensions <ITemplateProviderExtension[]>] 
```

### From a Stream
```powershell
Read-PnPSiteTemplate [-Stream] <Stream> [-TemplateProviderExtensions <ITemplateProviderExtension[]>] 
```

## DESCRIPTION
This cmdlet allows you to read a PnP Provisioning Template from disk, by passing in XML as text or from a stream to keep it in memory and not needing to physically save the template anywhere. The template can be modified in memory if needed and then applied to a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Read-PnPSiteTemplate -Path template.pnp
```

Loads a PnP file from the file system

### EXAMPLE 2
```powershell
Read-PnPSiteTemplate -Path template.pnp -TemplateProviderExtensions $extensions
```

Loads a PnP file from the file system using some custom template provider extensions while loading the file.

### EXAMPLE 3
```powershell
Read-PnPSiteTemplate -Xml $xml
```

Reads a PnP Provisioning template from a string containing the XML of a provisioning template

### EXAMPLE 4
```powershell
$stream = Get-PnPFile -Url "/sites/templates/Site Templates/sample1.pnp" -AsMemoryStream
Read-PnPSiteTemplate -Stream $stream
```

Downloads the contents of a PnP Provisioning template from a SharePoint Online location in memory and parses it as a PnP Provisioning Template

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

### -TemplateProviderExtensions
Allows you to specify ITemplateProviderExtension to execute while loading the template.

```yaml
Type: ITemplateProviderExtension[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Xml
Variable to read from, containing the valid XML of a provisioning template.

```yaml
Type: String
Parameter Sets: By XML

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stream
Stream containing an XML or PNP provisioning template package

```yaml
Type: Stream
Parameter Sets: By Stream

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)