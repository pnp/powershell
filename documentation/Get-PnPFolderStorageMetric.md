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
Allows retrieval of storage metrics for a folder in SharePoint Online. It will reveal the true storage usage, similar to what will be shown through storman.aspx, the date and time the folder was last modified, the amount of files inside the folder and more.

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
Name for the local file

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
Name for the local file

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
Name for the local file

```yaml
Type: Identity
Parameter Sets: Folder via pipebind

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
