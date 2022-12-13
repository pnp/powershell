---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFolderStorageMetric.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFolderStorageMetric
---
  
# Get-PnPFolderStorageMetric

## SYNOPSIS
Allows retrieval of storage metrics for a folder in SharePoint Online

## SYNTAX

### Folder via site relative URL (Default)
```powershell
Get-PnPFolderStorageMetric [-FolderSiteRelativeUrl <String>] [-Connection <PnPConnection>]
```

Retrieves the storage metrics of the current root folder or the site relative folder specified

### Folder via pipebind
```powershell
Get-PnPFolderStorageMetric -List <ListPipeBind> [-Connection <PnPConnection>]
```

Retrieves the storage metrics of the provided list

### Folder via list
```powershell
Get-PnPFolderStorageMetric -Identity <FolderPipeBind> [-Connection <PnPConnection>]
```

Retrieves the storage metrics of the provided folder

## DESCRIPTION
Allows retrieval of storage metrics for a folder in SharePoint Online. It will reveal the true storage usage, similar to what will be shown through storman.aspx, the date and time the folder was last modified, the amount of files inside the folder and more.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFolderStorageMetric
```

Retrieves the storage metrics of the current root folder

### EXAMPLE 2
```powershell
Get-PnPFolderStorageMetric -FolderSiteRelativeUrl "/Documents"
```

Retrieves the file and downloads it to c:\temp\image.jpg

### EXAMPLE 3
```powershell
Get-PnPFolderStorageMetric -Url /sites/project/_catalogs/themes/15/company.spcolor -AsString
```

Retrieves the contents of the file as text and outputs its contents to the console

### EXAMPLE 4
```powershell
Get-PnPFolderStorageMetric -Url /sites/project/Shared Documents/Folder/Presentation.pptx -AsFileObject
```

Retrieves the file and returns it as a File object

### EXAMPLE 5
```powershell
Get-PnPFolderStorageMetric -Url /sites/project/_catalogs/themes/15/company.spcolor -AsListItem
```

Retrieves the file and returns it as a ListItem object

### EXAMPLE 6
```powershell
Get-PnPFolderStorageMetric -Url /personal/john_tenant_onmicrosoft_com/Documents/Sample.xlsx -Path c:\temp -FileName Project.xlsx -AsFile
```

Retrieves the file Sample.xlsx by its site relative URL from a OneDrive for Business site and downloads it to c:\temp\Project.xlsx

### EXAMPLE 7
```powershell
Get-PnPFolderStorageMetric -Url "/sites/templates/Shared Documents/HR Site.pnp" -AsMemoryStream
```

Retrieves the file in memory for further processing

## PARAMETERS

### -AsFile

```yaml
Type: SwitchParameter
Parameter Sets: Save to local path

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsFileObject
Retrieve the file contents as a file object.

```yaml
Type: SwitchParameter
Parameter Sets: Return as file object

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsListItem
Returns the file as a listitem showing all its properties

```yaml
Type: SwitchParameter
Parameter Sets: Return as list item

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsString
Retrieve the file contents as a string

```yaml
Type: SwitchParameter
Parameter Sets: Return as string

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsMemoryStream

```yaml
Type: SwitchParameter
Parameter Sets: Download the content of the file to memory

Required: True
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

### -Filename
Name for the local file

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

### -ThrowExceptionIfFileNotFound
If provided in combination with -AsListItem, a System.ArgumentException will be thrown if the file specified in the -Url argument does not exist. Otherwise it will return nothing instead.

```yaml
Type: SwitchParameter
Parameter Sets: Return as list item

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
