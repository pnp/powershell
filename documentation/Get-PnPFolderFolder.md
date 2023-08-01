---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFolderFolder.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFolderFolder
---
  
# Get-PnPFolderFolder

## SYNOPSIS
List subfolders in a folder

## SYNTAX

### Folder via url
```powershell
Get-PnPFolderFolder [-FolderSiteRelativeUrl <String>] [-ItemName <String>] [-ExcludeSystemFolders] [-Includes <String[]>] [-Recursive] [-Verbose] [-Connection <PnPConnection>] 
```

### Folder via pipebind
```powershell
Get-PnPFolderFolder [-Identity <FolderPipeBind>] [-ItemName <String>] [-ExcludeSystemFolders] [-Includes <String[]>] [-Recursive] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet allows listing of all the subfolders of a folder. It can optionally also list all folders in the underlying subfolders.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFolderFolder
```

Returns all the folders in the root of the current web

### EXAMPLE 2
```powershell
Get-PnPFolderFolder -Recurse
```

Returns all the folders in the entire site. This will take a while to complete and will cause a lot of calls to be made towards SharePoint Online. Use it wisely.

### EXAMPLE 3
```powershell
Get-PnPFolderFolder -Identity "Shared Documents"
```

Returns the folders located in the 'Shared Documents' folder located in the root of the current web

### EXAMPLE 4
```powershell
Get-PnPFolderFolder -Identity "Shared Documents" -ExcludeSystemFolders
```

Returns the folders located in the 'Shared Documents' folder located in the root of the current web which are not hidden system folders

### EXAMPLE 5
```powershell
Get-PnPFolderFolder -FolderSiteRelativeUrl "Shared Documents" -ItemName "Templates"
```

Returns the folder 'Template' which is located in the folder 'Shared Documents' which is located in the root of the current web

### EXAMPLE 6
```powershell
Get-PnPFolder -Identity "Shared Documents" | Get-PnPFolderFolder
```

Returns all folders in the "Shared Documents" folder which is located in the root of the current web

### EXAMPLE 7
```powershell
Get-PnPFolderFolder -FolderSiteRelativeUrl "SitePages" -Recursive
```

Returns all folders, including those located in any subfolders, in the folder SitePages which is located in the root of the current web

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
When provided, all system folders will be excluded from the output

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
Name of the folder to retrieve (not case sensitive)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recursive
A switch parameter to include folders of all subfolders in the specified folder

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