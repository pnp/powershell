---
Module Name: PnP.PowerShell
title: Remove-PnPFileFromSiteTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFileFromSiteTemplate.html
---
 
# Remove-PnPFileFromSiteTemplate

## SYNOPSIS
Removes a file from a PnP Provisioning Template

## SYNTAX

```powershell
Remove-PnPFileFromSiteTemplate [-Path] <String> [-FilePath] <String>
 [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>] 
```

## DESCRIPTION

Allows to remove a file from a PnP Provisioning Template.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPFileFromSiteTemplate -Path template.pnp -FilePath filePath
```

Removes a file from an in-memory PnP Provisioning Template

## PARAMETERS

### -FilePath
The relative File Path of the file to remove from the in-memory template

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Filename to read the template from, optionally including full path.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateProviderExtensions
Allows you to specify ITemplateProviderExtension to execute while saving the template.

```yaml
Type: ITemplateProviderExtension[]
Parameter Sets: (All)

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

