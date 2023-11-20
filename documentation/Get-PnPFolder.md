---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFolder.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFolder
---
  
# Get-PnPFolder

## SYNOPSIS
Returns a folder object

## SYNTAX

### Folders in current Web (Default)
```powershell
Get-PnPFolder [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose]
```

### Root folder of the current Web
```powershell
Get-PnPFolder -CurrentWebRootFolder [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose]
```

### Folder by url
```powershell
Get-PnPFolder -Url <String> [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose]
```

### Root folder of a list
```powershell
Get-PnPFolder -ListRootFolder <ListPipeBind> [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose]
```

### Folders In List
```powershell
Get-PnPFolder -List <ListPipeBind> [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Retrieves the folder instance of the specified location.

Use [Resolve-PnPFolder](Resolve-PnPFolder.md) to create the folder if it does not exist.
Use [Get-PnPFolderInFolder](Get-PnPFolderInFolder.md) to retrieve subfolders.
Use [Get-PnPFileInFolder](Get-PnPFileInFolder.md) to retrieve files in a folder.
Use [Get-PnPFolderItem](Get-PnPFolderItem.md) to retrieve files and subfolders.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFolder
```

Returns all the folders located in the root of the current web

### EXAMPLE 2
```powershell
Get-PnPFolder -CurrentWebRootFolder
```

Returns the folder instance representing the root of the current web

### EXAMPLE 3
```powershell
Get-PnPFolder -Url "Shared Documents"
```

Returns the folder called 'Shared Documents' which is located in the root of the current web

### EXAMPLE 4
```powershell
Get-PnPFolder -Url "/sites/demo/Shared Documents"
```

Returns the folder called 'Shared Documents' which is located in the root of the site collection located at '/sites/demo'

### EXAMPLE 5
```powershell
Get-PnPFolder -ListRootFolder "Shared Documents"
```

Returns the root folder of the list called 'Shared Documents'

### EXAMPLE 6
```powershell
Get-PnPFolder -List "Shared Documents"
```

Returns the folders inside the root folder of the list called 'Shared Documents'. Please use Get-PnPFolder -ListRootFolder <folder> | Get-PnPFolderInFolder instead.

## PARAMETERS

### -CurrentWebRootFolder
If provided, the folder representing the root of the current web will be returned

```yaml
Type: SwitchParameter
Parameter Sets: Root folder of the current Web

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

### -List
Name, ID or instance of a list or document library to retrieve the folders residing in it for. Please use Get-PnPFolder -ListRootFolder <folder> | Get-PnPFolderInFolder instead.

```yaml
Type: ListPipeBind
Parameter Sets: Folders In List

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListRootFolder
Name, ID or instance of a list or document library to retrieve the rootfolder of.

```yaml
Type: ListPipeBind
Parameter Sets: Root folder of a list

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
Site or server relative URL of the folder to retrieve. In the case of a server relative url, make sure that the url starts with the managed path as the current web.

```yaml
Type: String
Parameter Sets: Folder By Url
Aliases: RelativeUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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