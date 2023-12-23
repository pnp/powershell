---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Convert-PnPFile.html
external help file: PnP.PowerShell.dll-Help.xml
title: Convert-PnPFile
---
  
# Convert-PnPFile

## SYNOPSIS
Converts a file to another format

## SYNTAX


### Save to local path
```powershell
Convert-PnPFile -Url <String> -Path <String> [-Force]
```

### Return as memorystream
```powershell
Convert-PnPFile -Url <String> -AsMemoryStream
```

### Save to SharePoint Online (Same SiteCollection)
```powershell
Convert-PnPFile -Url <String> -Folder <String> 
```

## DESCRIPTION
Allows converting of a file from SharePoint Online. The file contents can either be directly saved to local disk, or stored in memory for further processing, or Can be uploaded back to SharePoint Online SiteCollection

## EXAMPLES

### EXAMPLE 1
```powershell
Convert-PnPFile -Url "/sites/demo/Shared Documents/Document.docx" -AsMemoryStream
```

Retrieves the file and converts to PDF, and outputs its content to the console as a Memory Stream

### EXAMPLE 2
```powershell
Convert-PnPFile -Url "/sites/demo/Shared Documents/Document.docx"
```

Retrieves the file and converts to PDF, and outputs its content to the console as a Memory Stream

### EXAMPLE 3
```powershell
Convert-PnPFile -Url "/sites/demo/Shared Documents/Document.docx" -Path "C:\Temp"
```

Retrieves the file and converts to PDF, and save it to the given local path

### EXAMPLE 4
```powershell
Convert-PnPFile -Url "/sites/demo/Shared Documents/Document.docx" -Path "C:\Temp" -Force
```

Retrieves the file and converts to PDF, and save it to the given local path. Force parameter will override the existing file in the location where the document gets saved.

### EXAMPLE 5
```powershell
Convert-PnPFile -Url "/sites/demo/Shared Documents/Test/Book.xlsx" -Folder "/sites/demo/Shared Documents/Archive"
```

Retrieves the file and converts to PDF, and save it to the given Document library (Folder) in SharePoint Online (same site collection)

### EXAMPLE 6
```powershell
Convert-PnPFile -Url "/sites/demo/Shared Documents/Test/Book.png" -ConvertToFormat Jpg  -Folder "/sites/demo/Shared Documents/Archive"
```

Retrieves the file and converts to JPG, and save it to the given Document library (Folder) in SharePoint Online (same site collection)

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

### -ConvertToFormat
The format to which you want the file to be converted. Default is PDF.

The following values are valid transformation targets and their supported source extensions:

| Target | Description                        | Supported source extensions
|:------|:-----------------------------------|---------------------------------
| glb   | Converts the item into GLB format  | cool, fbx, obj, ply, stl, 3mf
| html  | Converts the item into HTML format | eml, md, msg
| jpg   | Converts the item into JPG format  | 3g2, 3gp, 3gp2, 3gpp, 3mf, ai, arw, asf, avi, bas, bash, bat, bmp, c, cbl, cmd, cool, cpp, cr2, crw, cs, css, csv, cur, dcm, dcm30, dic, dicm, dicom, dng, doc, docx, dwg, eml, epi, eps, epsf, epsi, epub, erf, fbx, fppx, gif, glb, h, hcp, heic, heif, htm, html, ico, icon, java, jfif, jpeg, jpg, js, json, key, log, m2ts, m4a, m4v, markdown, md, mef, mov, movie, mp3, mp4, mp4v, mrw, msg, mts, nef, nrw, numbers, obj, odp, odt, ogg, orf, pages, pano, pdf, pef, php, pict, pl, ply, png, pot, potm, potx, pps, ppsx, ppsxm, ppt, pptm, pptx, ps, ps1, psb, psd, py, raw, rb, rtf, rw1, rw2, sh, sketch, sql, sr2, stl, tif, tiff, ts, txt, vb, webm, wma, wmv, xaml, xbm, xcf, xd, xml, xpm, yaml, yml
| pdf   | Converts the item into PDF format  | doc, docx, epub, eml, htm, html, md, msg, odp, ods, odt, pps, ppsx, ppt, pptx, rtf, tif, tiff, xls, xlsm, xlsx

For more information, check [this link](https://pnp.github.io/pnpcore/using-the-sdk/files-intro.html#converting-files).


```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: Pdf
Accept pipeline input: False
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
Parameter Sets: Save to local path, Upload to SharePoint

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