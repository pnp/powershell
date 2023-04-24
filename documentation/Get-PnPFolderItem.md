---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFolderItem.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFolderItem
---
  
# Get-PnPFolderItem

## SYNOPSIS
List content in folder

## SYNTAX

### Folder via url
```powershell
Get-PnPFolderItem [-FolderSiteRelativeUrl <String>] [-ItemType <String>] [-ItemName <String>] [-Recursive]
 [-Connection <PnPConnection>] 
```

### Folder via pipebind
```powershell
Get-PnPFolderItem [-Identity <FolderPipeBind>] [-ItemType <String>] [-ItemName <String>] [-Recursive]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages"
```

Returns the contents of the folder SitePages which is located in the root of the current web

### EXAMPLE 2
```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages" -ItemName "Default.aspx"
```

Returns the file 'Default.aspx' which is located in the folder SitePages which is located in the root of the current web

### EXAMPLE 3
```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages" -ItemType Folder
```

Returns all folders in the folder SitePages which is located in the root of the current web

### EXAMPLE 4
```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages" -ItemType File
```

Returns all files in the folder SitePages which is located in the root of the current web

### EXAMPLE 5
```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages" -Recursive
```

Returns all files and folders, including contents of any subfolders, in the folder SitePages which is located in the root of the current web

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

### -FolderSiteRelativeUrl
The site relative URL of the folder to retrieve

```yaml
Type: String
Parameter Sets: Folder via url

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemName
Optional name of the item to retrieve

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemType
The type of contents to retrieve, either File, Folder or All (default)

```yaml
Type: String
Parameter Sets: (All)
Accepted values: Folder, File, All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recursive
A switch parameter to include contents of all subfolders in the specified folder

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


