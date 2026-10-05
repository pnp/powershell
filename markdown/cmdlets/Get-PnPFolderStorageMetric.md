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

### Folder via pipebind
```powershell
Get-PnPFolderStorageMetric -List <ListPipeBind> [-Connection <PnPConnection>]
```

### Folder via list
```powershell
Get-PnPFolderStorageMetric -Identity <FolderPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION
Allows retrieval of storage metrics for a folder in SharePoint Online. It will reveal the true storage usage, similar to what will be shown through storman.aspx, the date and time the folder was last modified and the amount of files inside the folder.

Please note that there is a delay of typically just a few minutes between making changes to files on a site and this cmdlet returning updated values.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFolderStorageMetric
```
Retrieves the storage metrics of the current site/web

### EXAMPLE 2
```powershell
Get-PnPFolderStorageMetric -List "Documents"
```
Retrieves the storage metrics of the specified document library

### EXAMPLE 3
```powershell
Get-PnPFolderStorageMetric -FolderSiteRelativeUrl "Shared Documents"
```
Retrieves the storage metrics of the folder using the server-relative Url

### EXAMPLE 4
```powershell
$folder = Get-PnPFolder -Url "Shared Documents"
Get-PnPFolderStorageMetric -Identity $folder
```

Retrieves the storage metrics of the folder using the identity parameter

## PARAMETERS

### -FolderSiteRelativeUrl
The path to the folder to query for its storage consumption, relative to the SharePoint Online site to which the connection has been made, i.e. "Shared Documents\Subfolder"

```yaml
Type: String
Parameter Sets: Folder via site relative URL

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
Id, name or instance of a list to query for its storage consumption

```yaml
Type: ListPipeBind
Parameter Sets: Folder via list

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Id, name or instance of a folder to query for its storage consumption

```yaml
Type: FolderPipeBind
Parameter Sets: Folder via pipebind

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)