---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Copy-PnPFolder.html
external help file: PnP.PowerShell.dll-Help.xml
title: Copy-PnPFolder
---
  
# Copy-PnPFolder

## SYNOPSIS
Copies a folder or file to a different location within SharePoint Online or allows uploading of an entire local folder with optionally subfolders to SharePoint Online.

### Copy files within Microsoft 365

## SYNTAX

```powershell
Copy-PnPFolder -SourceUrl <String> -TargetUrl <String> [-Overwrite] [-Force] [-IgnoreVersionHistory] [-NoWait] [-Connection <PnPConnection>] [-Verbose]
  
```

### Copy files from local to Microsoft 365

## SYNTAX

```powershell
Copy-PnPFolder -LocalPath <String> -TargetUrl <String> [-Overwrite] [-Recurse] [-RemoveAfterCopy] [-Connection <PnPConnection>] [-Verbose]
  
```

## DESCRIPTION

Copies a folder or file to a different location within SharePoiint. This location can be within the same document library, same site, same site collection or even to another site collection on the same tenant. Notice that if copying between sites or to a subsite you cannot specify a target filename, only a folder name.

Copying files and folders is bound to some restrictions. You can find more on it here: https://learn.microsoft.com/office365/servicedescriptions/sharepoint-online-service-description/sharepoint-online-limits#moving-and-copying-across-sites

It can also accommodate copying an entire folder with all its files and optionally even subfolders and files from a local path onto SharePoint Online.

## EXAMPLES

### EXAMPLE 1
```powershell
Copy-PnPFolder -SourceUrl "Shared Documents/MyProjectfiles" -TargetUrl "/sites/otherproject/Shared Documents" -Overwrite
```

Copies a folder named MyProjectFiles in the document library called Documents located in the current site to the root folder of the library named Documents in the site collection otherproject. If a folder named MyProjectFiles already exists, it will overwrite it.

### EXAMPLE 2
```powershell
Copy-PnPFolder -SourceUrl "/sites/project/Shared Documents/company.docx" -TargetUrl "/sites/otherproject/Shared Documents"
```

Copies a file named company.docx located in a document library called Shared Documents in the site collection project to the Shared Documents library in the site collection otherproject. If a file named company.docx already exists, it won't perform the copy.

### EXAMPLE 3
```powershell
Copy-PnPFolder -SourceUrl "Shared Documents/company.docx" -TargetUrl "/sites/otherproject/Shared Documents" -IgnoreVersionHistory
```

Copies a file named company.docx located in a document library called Documents in the current site to the site collection otherproject. If a file named company.docx already exists, it won't perform the copy. Only the latest version of the file will be copied and its history will be discarded.

### EXAMPLE 4
```powershell
Copy-PnPFolder -SourceUrl "/sites/project/Shared Documents/Archive" -TargetUrl "/sites/otherproject/Shared Documents" -Overwrite
```

Copies a folder named Archive located in a document library called Shared Documents in the site collection project to the Shared Documents library in the site collection otherproject. If a folder named Archive already exists, it will overwrite it.

### EXAMPLE 5
```powershell
Copy-PnPFolder -SourceUrl "Documents/company.docx" -TargetUrl "Documents/company2.docx"
```

Copies a file named company.docx located in a document library called Documents to a new document named company2.docx in the same library.

### EXAMPLE 6
```powershell
Copy-PnPFolder -SourceUrl "Shared Documents/company.docx" -TargetUrl "Shared Documents2/company.docx"
```

Copies a file named company.docx located in a document library called Documents to a document library called Documents2 in the same site. 

### EXAMPLE 7
```powershell
Copy-PnPFolder -SourceUrl "Shared DocuDocuments/company.docx" -TargetUrl "Subsite/Shared Documents"
```

Copies a file named company.docx located in a document library called Documents to the document library named Documents in a subsite named Subsite keeping the file name.

### EXAMPLE 8
```powershell
Copy-PnPFolder -SourceUrl "Shared Documents/company.docx" -TargetUrl "/sites/otherproject/Shared Documents" -Overwrite
```

Copies a file named company.docx located in a document library called Documents in the current site to the site collection otherproject. If a file named company.docx already exists, it will still perform the copy and replace the original company.docx file.

### EXAMPLE 9
```powershell
Copy-PnPFolder -SourceUrl "Shared Documents/MyDocs" -TargetUrl "/sites/otherproject/Documents" -Overwrite
```

Copies a folder named MyDocs in the document library called Documents located in the current site to the site collection otherproject. If the MyDocs folder exist it will copy into it, if not it will be created.

### EXAMPLE 10
```powershell
Copy-PnPFolder -SourceUrl "SubSite1/Documents/company.docx" -TargetUrl "SubSite2/Documents"
```

Copies a file named company.docx in the library named Documents in SubSite1 to the library named Documents in SubSite2.

### EXAMPLE 11
```powershell
$job = Copy-PnPFolder -SourceUrl "Shared Documents/company.docx" -TargetUrl "SubSite2/Shared Documents" -NoWait
$jobStatus = Receive-PnPCopyMoveJobStatus -Job $result
if($jobStatus.JobState == 0)
{
  Write-Host "Job finished"
}
```

Copies a file named company.docx from the current document library to the documents library in SubSite2. It will not wait for the action to return but returns job information instead. The Receive-PnPCopyMoveJobStatus cmdlet will return the job status.

### EXAMPLE 12
```powershell
Copy-PnPFolder -LocalPath "c:\temp" -TargetUrl "Subsite1/Shared Documents" -Recurse -Overwrite
```

Copies all the files and underlying folders from the local folder c:\temp to the document library Shared Documents in Subsite1. If a file already exists, it will be overwritten.

## PARAMETERS

### -Force
If provided, no confirmation will be requested and the action will be performed

```yaml
Type: SwitchParameter
Parameter Sets: WITHINM365

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreVersionHistory
If provided, only the latest version of the document will be copied and its history will be discarded. If not provided, all historical versions will be copied.

```yaml
Type: SwitchParameter
Parameter Sets: WITHINM365

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Overwrite
If provided, if a file already exists at the TargetUrl, it will be overwritten. If omitted, the copy operation will be canceled if the file already exists at the TargetUrl location when copying between two locations on SharePoint Online. If copying files from a local path to SharePoint Online, it will skip any file that already exists and still continue with the next one.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceUrl
Site or server relative URL specifying the file or folder to copy. Must include the file name if it is a file or the entire path to the folder if it is a folder.

```yaml
Type: String
Parameter Sets: WITHINM365
Aliases: SiteRelativeUrl, ServerRelativeUrl

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetUrl
Site or server relative URL where to copy the file or folder to. Must not include the file name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: TargetServerRelativeLibrary

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWait
If specified the task will return immediately after creating the copy job. The cmdlet will return a job object which can be used with Receive-PnPCopyMoveJobStatus to retrieve the status of the job.

```yaml
Type: SwitchParameter
Parameter Sets: WITHINM365

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recurse
When copying files from a local folder to SharePoint Online, this parameter will copy all files and folders within the local folder and all of its subfolders as well.

```yaml
Type: SwitchParameter
Parameter Sets: FROMLOCAL

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveAfterCopy
When copying files from a local folder to SharePoint Online, this parameter will remove all files locally that have successfully been uploaded to SharePoint Online. If a file fails, it will not be removed locally. Local folders will never be removed.

```yaml
Type: SwitchParameter
Parameter Sets: FROMLOCAL

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)