---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileInFolder.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFileInFolder
---
  
# Get-PnPFileInFolder

## SYNOPSIS
List files in a folder

## SYNTAX

### Folder via url
```powershell
Get-PnPFileInFolder [-FolderSiteRelativeUrl <String>] [-ItemName <String>] [-Recurse] [-Includes <String[]>] [-Verbose] [-Connection <PnPConnection>] 
```

### Folder via pipebind
```powershell
Get-PnPFileInFolder [-Identity <FolderPipeBind>] [-ItemName <String>] [-Recurse] [-Includes <String[]>] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet allows listing of all the files in a folder. It can optionally also list all files in the underlying subfolders.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFileInFolder
```

Returns all the files in the root of the current web

### EXAMPLE 2
```powershell
Get-PnPFileInFolder -Recurse
```

Returns all the files in the entire site. This will take a while to complete and will cause a lot of calls to be made towards SharePoint Online. Use it wisely.

### EXAMPLE 3
```powershell
Get-PnPFileInFolder -Identity "Shared Documents"
```

Returns the files located in the 'Shared Documents' folder located in the root of the current web

### EXAMPLE 4
```powershell
Get-PnPFileInFolder -FolderSiteRelativeUrl "SitePages" -ItemName "Default.aspx"
```

Returns the file 'Default.aspx' which is located in the folder SitePages which is located in the root of the current web

### EXAMPLE 5
```powershell
Get-PnPFolder -Identity "Shared Documents" | Get-PnPFileInFolder
```

Returns all files in the "Shared Documents" folder which is located in the root of the current web

### EXAMPLE 6
```powershell
Get-PnPFileInFolder -FolderSiteRelativeUrl "SitePages" -Recurse
```

Returns all files, including those located in any subfolders, in the folder SitePages which is located in the root of the current web

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeSystemFolders
When provided, all files in system folders will be excluded from the output. This parameter is not supported when not providing a folder through -Identity or -FolderSiteRelativeUrl.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FolderSiteRelativeUrl
The site relative URL of the folder to retrieve

```yaml
Type: String
Parameter Sets: Folder via url

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
A folder instance to the folder to retrieve

```yaml
Type: FolderPipeBind
Parameter Sets: Folder via pipebind

Required: False
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Includes
Optionally allows properties to be retrieved for the returned files which are not included in the response by default

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemName
Name of the file to retrieve (not case sensitive)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recurse
A switch parameter to include files of all subfolders in the specified folder

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 4
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