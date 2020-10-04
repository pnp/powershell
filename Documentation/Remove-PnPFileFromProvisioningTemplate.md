---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpfilefromSiteTemplate
schema: 2.0.0
title: Remove-PnPFileFromSiteTemplate
---

# Remove-PnPFileFromSiteTemplate

## SYNOPSIS
Removes a file from a PnP Provisioning Template

## SYNTAX

```
Remove-PnPFileFromSiteTemplate [-Path] <String> [-FilePath] <String>
 [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>] [<CommonParameters>]
```

## DESCRIPTION

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
Aliases:

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
Aliases:

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
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)