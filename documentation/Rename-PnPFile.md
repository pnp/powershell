---
Module Name: PnP.PowerShell
title: Rename-PnPFile
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Rename-PnPFile.html
---
 
# Rename-PnPFile

## SYNOPSIS
Renames a file in its current location

## SYNTAX

### SERVER
```powershell
Rename-PnPFile [-ServerRelativeUrl] <String> [-TargetFileName] <String> [-OverwriteIfAlreadyExists] [-Force]
 [-Connection <PnPConnection>]   
```

### SITE
```powershell
Rename-PnPFile [-SiteRelativeUrl] <String> [-TargetFileName] <String> [-OverwriteIfAlreadyExists] [-Force]
 [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to rename a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Rename-PnPFile -ServerRelativeUrl /sites/project/Documents/company.docx -TargetFileName mycompany.docx
```

Renames a file named company.docx located in the document library called Documents located in the projects sitecollection under the managed path sites to mycompany.docx. If a file named mycompany.aspx already exists, it won't perform the rename.

### EXAMPLE 2
```powershell
Rename-PnPFile -SiteRelativeUrl Documents/company.aspx -TargetFileName mycompany.docx
```

Renames a file named company.docx located in the document library called Documents located in the current site to mycompany.aspx. If a file named mycompany.aspx already exists, it won't perform the rename.

### EXAMPLE 3
```powershell
Rename-PnPFile -ServerRelativeUrl /sites/project/Documents/company.docx -TargetFileName mycompany.docx -OverwriteIfAlreadyExists
```

Renames a file named company.docx located in the document library called Documents located in the projects sitecollection under the managed path sites to mycompany.aspx. If a file named mycompany.aspx already exists, it will still perform the rename and replace the original mycompany.aspx file.

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

### -OverwriteIfAlreadyExists
If provided, if a file already exist with the provided TargetFileName, it will be overwritten. If omitted, the rename operation will be canceled if a file already exists with the TargetFileName file name.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativeUrl
Server relative Url specifying the file to rename. Must include the file name.

```yaml
Type: String
Parameter Sets: SERVER

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteRelativeUrl
Site relative Url specifying the file to rename. Must include the file name.

```yaml
Type: String
Parameter Sets: SITE

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetFileName
File name to rename the file to. Should only be the file name and not include the path to its location. Use Move-PnPFile to move the file to another location.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 1
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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

