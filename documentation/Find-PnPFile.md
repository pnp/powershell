---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Find-PnPFile.html
external help file: PnP.PowerShell.dll-Help.xml
title: Find-PnPFile
---
  
# Find-PnPFile

## SYNOPSIS
Finds a file in the virtual file system of the web.

## SYNTAX

### Web (Default)
```powershell
Find-PnPFile [-Match] <String> [-Connection <PnPConnection>] 
```

### List
```powershell
Find-PnPFile [-Match] <String> -List <ListPipeBind> [-Connection <PnPConnection>]
 
```

### Folder
```powershell
Find-PnPFile [-Match] <String> -Folder <FolderPipeBind> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to find a file in the virtual file system of the web.

## EXAMPLES

### EXAMPLE 1
```powershell
Find-PnPFile -Match *.master
```

Will return all masterpages located in the current web.

### EXAMPLE 2
```powershell
Find-PnPFile -List "Documents" -Match *.pdf
```

Will return all pdf files located in given list.

### EXAMPLE 3
```powershell
Find-PnPFile -Folder "Shared Documents/Sub Folder" -Match *.docx
```

Will return all docx files located in given folder.

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

### -Folder
Folder object or relative url of a folder to query

```yaml
Type: FolderPipeBind
Parameter Sets: Folder

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
List title, url or an actual List object to query

```yaml
Type: ListPipeBind
Parameter Sets: List

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Match
Wildcard query

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


