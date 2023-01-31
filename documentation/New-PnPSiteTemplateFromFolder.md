---
Module Name: PnP.PowerShell
title: New-PnPSiteTemplateFromFolder
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSiteTemplateFromFolder.html
---
 
# New-PnPSiteTemplateFromFolder

## SYNOPSIS
Generates a provisioning template from a given folder, including only files that are present in that folder

## SYNTAX

```powershell
New-PnPSiteTemplateFromFolder [[-Out] <String>] [[-Folder] <String>] [[-TargetFolder] <String>]
 [-Match <String>] [-ContentType <ContentTypePipeBind>] [-Properties <Hashtable>]
 [[-Schema] <XMLPnPSchemaVersion>] [-AsIncludeFile] [-Force] [-Encoding <Encoding>] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

Allows to create a new provisioning site template based on a given folder, including files present in it.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSiteTemplateFromFolder -Out template.xml
```

Creates an empty provisioning template, and includes all files in the current folder.

### EXAMPLE 2
```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp
```

Creates an empty provisioning template, and includes all files in the c:\temp folder.

### EXAMPLE 3
```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder.

### EXAMPLE 4
```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js -TargetFolder "Shared Documents"
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder and marks the files in the template to be added to the 'Shared Documents' folder

### EXAMPLE 5
```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js -TargetFolder "Shared Documents" -ContentType "Test Content Type"
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder and marks the files in the template to be added to the 'Shared Documents' folder. It will add a property to the item for the content type.

### EXAMPLE 6
```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js -TargetFolder "Shared Documents" -Properties @{"Title" = "Test Title"; "Category"="Test Category"}
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder and marks the files in the template to be added to the 'Shared Documents' folder. It will add the specified properties to the file entries.

### EXAMPLE 7
```powershell
New-PnPSiteTemplateFromFolder -Out template.pnp
```

Creates an empty provisioning template as a pnp package file, and includes all files in the current folder

### EXAMPLE 8
```powershell
New-PnPSiteTemplateFromFolder -Out template.pnp -Folder c:\temp
```

Creates an empty provisioning template as a pnp package file, and includes all files in the c:\temp folder

## PARAMETERS

### -AsIncludeFile
If specified, the output will only contain the &lt;pnp:Files&gt; element. This allows the output to be included in another template.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -ContentType
An optional content type to use.

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Folder
Folder to process. If not specified the current folder will be used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overwrites the output file if it exists.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Match
Optional wildcard pattern to match filenames against. If empty all files will be included.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Out
Filename to write to, optionally including full path.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Properties
Additional properties to set for every file entry in the generated template.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Schema
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

### -TargetFolder
Target folder to provision to files to. If not specified, the current folder name will be used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Encoding](https://learn.microsoft.com/dotnet/api/system.text.encoding)