---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFileToSiteTemplate.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFileToSiteTemplate
---
  
# Add-PnPFileToSiteTemplate

## SYNOPSIS
Adds a file to a PnP Provisioning Template

## SYNTAX

### Local File
```powershell
Add-PnPFileToSiteTemplate [-Path] <String> [-Source] <String> [-Folder] <String>
 [[-Container] <String>] [[-FileLevel] <FileLevel>] [-FileOverwrite]
 [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Remove File
```powershell
Add-PnPFileToSiteTemplate [-Path] <String> [-SourceUrl] <String> [[-Container] <String>]
 [[-FileLevel] <FileLevel>] [-FileOverwrite] [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -Source $sourceFilePath -Folder $targetFolder
```

Adds a file to a PnP Site Template

### EXAMPLE 2
```powershell
Add-PnPFileToSiteTemplate -Path template.xml -Source $sourceFilePath -Folder $targetFolder
```

Adds a file reference to a PnP Site XML Template

### EXAMPLE 3
```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -Source "./myfile.png" -Folder "folderinsite" -FileLevel Published -FileOverwrite:$false
```

Adds a file to a PnP Site Template, specifies the level as Published and defines to not overwrite the file if it exists in the site.

### EXAMPLE 4
```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -Source $sourceFilePath -Folder $targetFolder -Container $container
```

Adds a file to a PnP Site Template with a custom container for the file

### EXAMPLE 5
```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -SourceUrl "Shared%20Documents/ProjectStatus.docs"
```

Adds a file to a PnP Provisioning Template retrieved from the currently connected site. The url can be server relative or web relative. If specifying a server relative url has to start with the current site url.

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

### -Container
The target Container for the file to add to the in-memory template, optional argument.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileLevel
The level of the files to add. Defaults to Published

```yaml
Type: FileLevel
Parameter Sets: (All)

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileOverwrite
Set to overwrite in site, Defaults to true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder
The target Folder for the file to add to the in-memory template.

```yaml
Type: String
Parameter Sets: Local File

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Filename of the .PNP Open XML site template to read from, optionally including full path.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Source
The file to add to the in-memory template, optionally including full path.

```yaml
Type: String
Parameter Sets: Local File

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceUrl
The file to add to the in-memory template, specifying its url in the current connected Web.

```yaml
Type: String
Parameter Sets: Remove File

Required: True
Position: 1
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
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


