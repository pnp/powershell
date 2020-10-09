---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/copy-pnpfile
schema: 2.0.0
title: Copy-PnPFile
---

# Copy-PnPFile

## SYNOPSIS
Copies a file or folder to a different location. This location can be within the same document library, same site, same site collection or even to another site collection on the same tenant. Currently there is a 200MB file size limit for the file or folder to be copied.

## SYNTAX

```powershell
Copy-PnPFile [-SourceUrl] <String> [-TargetUrl] <String> [-OverwriteIfAlreadyExists] [-Force]
 [-SkipSourceFolderName] [-IgnoreVersionHistory] [-Web <WebPipeBind>] [-Connection <PnPConnection>] 
  [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/MyDocs -TargetUrl /sites/otherproject/Documents -SkipSourceFolderName -OverwriteIfAlreadyExists
```

Copies a folder named MyDocs in the document library called Documents located in the current site to the root folder of the library named Documents in the site collection otherproject.

### EXAMPLE 2
```powershell
PS:>Copy-PnPFile -ServerRelativeUrl "/sites/project/Shared Documents/company.docx" -TargetServerRelativeLibrary "/sites/otherproject/Shared Documents"
```

Copies a file named company.docx located in a document library called Shared Documents in the site collection project to the Shared Documents library in the site collection otherproject. If a file named company.docx already exists, it won't perform the copy.

### EXAMPLE 3
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/company.docx -TargetUrl /sites/otherproject/Documents/company.docx
```

Copies a file named company.docx located in a document library called Documents in the current site to the site collection otherproject. If a file named company.docx already exists, it won't perform the copy.

### EXAMPLE 4
```powershell
PS:>Copy-PnPFile -ServerRelativeUrl "/sites/project/Shared Documents/Archive" -TargetServerRelativeLibrary "/sites/otherproject/Shared Documents" -OverwriteIfAlreadyExists
```

Copies a folder named Archive located in a document library called Shared Documents in the site collection project to the Shared Documents library in the site collection otherproject. If a folder named Archive already exists, it will overwrite it.

### EXAMPLE 5
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/company.docx -TargetUrl Documents/company2.docx
```

Copies a file named company.docx located in a document library called Documents to a new document named company2.docx in the same library.

### EXAMPLE 6
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/company.docx -TargetUrl Documents2/company.docx
```

Copies a file named company.docx located in a document library called Documents to a document library called Documents2 in the same site. 

### EXAMPLE 7
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/company.docx -TargetUrl Subsite/Documents/company2.docx
```

Copies a file named company.docx located in a document library called Documents to the document library named Document in a subsite named Subsite as a new document named company2.docx.

### EXAMPLE 8
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/company.docx -TargetUrl Subsite/Documents
```

Copies a file named company.docx located in a document library called Documents to the document library named Document in a subsite named Subsite keeping the file name.

### EXAMPLE 9
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/company.docx -TargetUrl /sites/otherproject/Documents/company.docx -OverwriteIfAlreadyExists
```

Copies a file named company.docx located in a document library called Documents in the current site to the site collection otherproject. If a file named company.docx already exists, it will still perform the copy and replace the original company.docx file.

### EXAMPLE 10
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/MyDocs -TargetUrl /sites/otherproject/Documents -OverwriteIfAlreadyExists
```

Copies a folder named MyDocs in the document library called Documents located in the current site to the site collection otherproject. If the MyDocs folder exist it will copy into it, if not it will be created.

### EXAMPLE 11
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/MyDocs -TargetUrl /sites/otherproject/Documents/MyDocs -SkipSourceFolderName -OverwriteIfAlreadyExists
```

Copies a folder named MyDocs in the MyDocs folder of the library named Documents. If the MyDocs folder does not exists, it will be created.

### EXAMPLE 12
```powershell
PS:>Copy-PnPFile -SourceUrl Documents/MyDocs -TargetUrl /sites/otherproject/Documents/MyDocs -OverwriteIfAlreadyExists
```

Copies a folder named MyDocs in the root of the library named Documents. If the MyDocs folder exists in the target, a subfolder also named MyDocs is created.

### EXAMPLE 13
```powershell
PS:>Copy-PnPFile -SourceUrl SubSite1/Documents/company.docx -TargetUrl SubSite2/Documents
```

Copies a file named company.docx in the library named Documents in SubSite1 to the library named Documents in SubSite2.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

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
If provided, only the latest version of the document will be copied and its history will be discared. If not provided, all historical versions will be copied along.

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OverwriteIfAlreadyExists
If provided, if a file already exists at the TargetUrl, it will be overwritten. If omitted, the copy operation will be canceled if the file already exists at the TargetUrl location.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipSourceFolderName
If the source is a folder, the source folder name will not be created, only the contents within it

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
Site or server relative Url specifying the file or folder to copy. Must include the file name if it's a file or the entire path to the folder if it's a folder.

```yaml
Type: String
Parameter Sets: (All)
Aliases: SiteRelativeUrl, ServerRelativeUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetUrl
Server relative Url where to copy the file or folder to. Must not include the file name.

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

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)