---
Module Name: PnP.PowerShell
title: Move-PnPFile
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Move-PnPFile.html
---
 
# Move-PnPFile

## SYNOPSIS
Moves a file or folder to a different location

## SYNTAX

```powershell
Move-PnPFile [-SourceUrl] <String> [-TargetUrl] <String> [-Overwrite] [-AllowSchemaMismatch] [-AllowSmallerVersionLimitOnDestination] [-IgnoreVersionHistory] [-NoWait] [-Force] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Allows moving a file or folder to a different location inside the same document library, such as in a subfolder, to a different document library on the same site collection or to a document library on another site collection. If you move a file to a different site or subweb you cannot specify a target filename.

Moving files and folders is bound to some restrictions. You can find more on it here: https://learn.microsoft.com/office365/servicedescriptions/sharepoint-online-service-description/sharepoint-online-limits#moving-and-copying-across-sites

## EXAMPLES

### EXAMPLE 1
```powershell
Move-PnPFile -SourceUrl "Shared Documents/Document.docx" -TargetUrl "Archive/Document2.docx"
```

Moves a file named Document.docx located in the document library named "Shared Documents" in the current site to the document library named "Archive" in the same site, renaming the file to Document2.docx. If a file named Document2.docx already exists at the destination, it won't perform the move.

### EXAMPLE 2
```powershell
Move-PnPFile -SourceUrl "Shared Documents/Document.docx" -TargetUrl "Archive" -Overwrite
```

Moves a file named Document.docx located in the document library named "Shared Documents" in the current site to the document library named "Archive" in the same site. If a file named Document.docx already exists at the destination, it will overwrite it.

### EXAMPLE 3
```powershell
Move-PnPFile -SourceUrl "Shared Documents/Document.docx" -TargetUrl "/sites/otherproject/Shared Documents" -Overwrite -AllowSchemaMismatch -AllowSmallerVersionLimitOnDestination
```

Moves a file named Document.docx located in the document library named "Shared Documents" in the current site to the document library named "Shared Documents" in another site collection "otherproject" allowing it to overwrite an existing file Document.docx in the destination, allowing the fields to be different on the destination document library from the source document library and allowing a lower document version limit on the destination compared to the source.

### EXAMPLE 4
```powershell
Move-PnPFile -SourceUrl "/sites/project/Shared Documents/Archive" -TargetUrl "/sites/archive/Project" -AllowSchemaMismatch -AllowSmallerVersionLimitOnDestination
```

Moves a folder named Archive located in the document library named "Shared Documents" in the current site to the document library named "Project" in another site collection "archive" not allowing it to overwrite an existing folder named "Archive" in the destination, allowing the fields to be different on the destination document library from the source document library and allowing a lower document version limit on the destination compared to the source.

### EXAMPLE 5
```powershell
$job = Move-PnPFile -SourceUrl "Shared Documents/company.docx" -TargetUrl "SubSite2/Shared Documents" -NoWait
$jobStatus = Receive-PnPCopyMoveJobStatus -Job $result
if($jobStatus.JobState == 0)
{
  Write-Host "Job finished"
}
```

Moves a file named company.docx from the current document library to the documents library in SubSite2. It will not wait for the action to return but returns job information instead. The Receive-PnPCopyMoveJobStatus cmdlet will return the job status.

## PARAMETERS

### -AllowSchemaMismatch
If provided and the target document library specified using TargetServerRelativeLibrary has different fields than the document library where the document is being moved from, the move will succeed. If not provided, it will fail to protect against data loss of metadata stored in fields that cannot be moved along.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowSmallerVersionLimitOnDestination
If provided and the target document library specified using TargetServerRelativeLibrary is configured to keep less historical versions of documents than the document library where the document is being moved from, the move will succeed. If not provided, it will fail to protect against data loss of historical versions that cannot be moved along.

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

### -Force
If provided, no confirmation will be requested and the action will be performed

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreVersionHistory
If provided, only the latest version of the document will be moved and its history will be discarded. If not provided, all historical versions will be moved along.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Overwrite
If provided, if a file or folder already exists at the TargetUrl, it will be overwritten. If omitted, the move operation will be canceled if the file or folder already exists at the TargetUrl location.

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
Site or server relative URL specifying the file or folder to move. Must include the file name if it is a file or the entire path to the folder if it is a folder.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetUrl
Site or server relative URL of a document library where to move the file or folder to. 

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWait
If specified the task will return immediately after creating the move job. The cmdlet will return a job object which can be used with Receive-PnPCopyMoveJobStatus to retrieve the status of the job.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements might be shown while executing the cmdlet.

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