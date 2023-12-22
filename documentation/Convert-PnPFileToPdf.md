---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Convert-PnPFileToPDF.html
external help file: PnP.PowerShell.dll-Help.xml
title: Convert-PnPFileToPDF
---
  
# Convert-PnPFileToPDF

## SYNOPSIS
Converts a file to Pdf

## SYNTAX


### Save to local path
```powershell
Convert-PnPFileToPDF -Url <String> -Path <String> [-Force]
```

### Return as memorystream
```powershell
Convert-PnPFileToPDF -Url <String> -AsMemoryStream
```

### Save to SharePoint Online (Same SiteCollection)
```powershell
Convert-PnPFileToPDF -Url <String> -Folder <String> 
```

## DESCRIPTION
Allows converting of a file from SharePoint Online. The file contents can either be directly saved to local disk, or stored in memory for further processing, or Can be uploaded back to SharePoint Online SiteCollection

## EXAMPLES

### EXAMPLE 1
```powershell
Convert-PnPFileToPDF -Url "/sites/project/Shared Documents/Document.docx" -AsMemoryStream
```

Retrieves the file and converts to PDF, and outputs its content to the console as a Memory Stream

### EXAMPLE 2
```powershell
Convert-PnPFileToPDF -Url "/sites/project/Shared Documents/Document.docx"
```

Retrieves the file and converts to PDF, and outputs its content to the console as a Memory Stream

### EXAMPLE 3
```powershell
Convert-PnPFileToPDF -Url "/sites/project/Shared Documents/Document.docx" -Path "C:\Temp"
```

Retrieves the file and converts to PDF, and save it to the given local path

### EXAMPLE 4
```powershell
Convert-PnPFileToPDF -Url "/sites/project/Shared Documents/Document.docx" -Path "C:\Temp" -Force
```

Retrieves the file and converts to PDF, and save it to the given local path. Force parameter will override the existing file in the location where the document gets saved.

### EXAMPLE 5
```powershell
Convert-PnPFileToPDF -Url "/sites/SampleTeamSite/Shared Documents/Nishkalank's/Book.xlsx.docx" -Folder "Archive"
```

Retrieves the file and converts to PDF, and save it to the given Document library (Folder) in SharePoint Online (Same SiteCollection). Returns the saved file information in the console.



## PARAMETERS

### -Url
The URL (server or site relative) to the file

```yaml
Type: String
Parameter Sets: (All)
Aliases: ServerRelativeUrl, SiteRelativeUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```


### -AsMemoryStream

```yaml
Type: SwitchParameter
Parameter Sets: Return as memorystream

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Local path where the file should be saved

```yaml
Type: String
Parameter Sets: Save to local path

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -Force
Overwrites the file if it exists.

```yaml
Type: SwitchParameter
Parameter Sets: Save to local path

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder
The destination library in the site

```yaml
Type: FolderPipeBind
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
