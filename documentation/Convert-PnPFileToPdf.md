---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Convert-PnPFileToPdf.html
external help file: PnP.PowerShell.dll-Help.xml
title: Convert-PnPFileToPdf
---
  
# Convert-PnPFileToPdf

## SYNOPSIS
Converts a file to Pdf

## SYNTAX


### Save to local path
```powershell
Convert-PnPFileToPdf -Url <String> -Path <String> [-Force]
```

### Return as memorystream
```powershell
Convert-PnPFileToPdf -Url <String> -AsMemoryStream
```

### Save to SharePoint Online (Same SiteCollection)
```powershell
Convert-PnPFileToPdf -Url <String> -DocumentLibrary <String> [-Checkout] [-CheckInComment <String>] [-CheckinType <CheckinType>]
 [-Approve] [-ApproveComment <String>] [-Publish] [-PublishComment <String>]
```

## DESCRIPTION
Allows converting of a file from SharePoint Online. The file contents can either be directly saved to local disk, or stored in memory for further processing, or Can be uploaded back to SharePoint Online SiteCollection

## EXAMPLES

### EXAMPLE 1
```powershell
Convert-PnPFileToPdf -Url "/sites/project/Shared Documents/Document.docx" -AsMemoryStream
```

Retrieves the file and converts to PDF, and outputs its content to the console as a Memory Stream

### EXAMPLE 2
```powershell
Convert-PnPFileToPdf -Url "/sites/project/Shared Documents/Document.docx"
```

Retrieves the file and converts to PDF, and outputs its content to the console as a Memory Stream

### EXAMPLE 3
```powershell
Convert-PnPFileToPdf -Url "/sites/project/Shared Documents/Document.docx" -Path "C:\Temp"
```

Retrieves the file and converts to PDF, and save it to the given local path

### EXAMPLE 4
```powershell
Convert-PnPFileToPdf -Url "/sites/project/Shared Documents/Document.docx" -Path "C:\Temp" -Force
```

Retrieves the file and converts to PDF, and save it to the given local path. Force parameter will override the existing file in the location where the document gets saved.

### EXAMPLE 5
```powershell
Convert-PnPFileToPdf -Url "/sites/SampleTeamSite/Shared Documents/Nishkalank's/Book.xlsx.docx" -DocumentLibrary "Archive"
```

Retrieves the file and converts to PDF, and save it to the given Document library (Folder) in SharePoint Online (Same SiteCollection). Returns the saved file information in the console.

### EXAMPLE 6
```powershell
Convert-PnPFileToPdf -Url "/sites/SampleTeamSite/Shared Documents/Nishkalank's/Presentation.pptx" -DocumentLibrary "Shared Documents" -CheckOut -CheckInType 1 -CheckInComment "Check-in via PowerShell" -Publish -PublishComment "Published via PowerShell" -Approve -ApproveComment "Approved via PowerShell"
```

Retrieves the file and converts to PDF, and save it to the given Document library in SharePoint Online (Same SiteCollection). Returns the saved file information in the console.
Note : CheckOut, CheckInType, CheckInComment, Publish, PublishComment, Approve and ApproveComment only works with the document libraries where Versioning is enabled.


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

### -DocumentLibrary
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

### -CheckInComment
The comment added to the checkin

```yaml
Type: String
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CheckinType
Specifies the type of check-in for a file.

```yaml
Type: Enum (Microsoft.SharePoint.Client.CheckinType)
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: False
Position: Named
Default value: MinorCheckIn
Accept pipeline input: False
Accept wildcard characters: False
```

### -Checkout
If versioning is enabled, this will check out the file first if it exists, upload the file, then check it in again

```yaml
Type: SwitchParameter
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Publish
Will auto publish the file

```yaml
Type: SwitchParameter
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PublishComment
The comment added to the publish action

```yaml
Type: String
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Approve
Will auto approve the uploaded file

```yaml
Type: SwitchParameter
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApproveComment
The comment added to the approval

```yaml
Type: String
Parameter Sets: (UPLOADTOSHAREPOINT)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
