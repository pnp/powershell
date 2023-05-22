---
Module Name: PnP.PowerShell
title: Move-PnPFolder
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Move-PnPFolder.html
---
 
# Move-PnPFolder

## SYNOPSIS
Move a folder to another location in the current web. If you want to move a folder to a different site collection, use the Move-PnPFile cmdlet instead, which also supports moving folders and also accross site collections. Move-PnPFolder can be used to move folders that are within the list view threshold, the commandlet will fail if the list view threshold is exceeded. 

## SYNTAX

```powershell
Move-PnPFolder -Folder <FolderPipeBind> -TargetFolder <String> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to move folder to another location in the current web. If you want to move a folder to a different site collection, use the Move-PnPFile cmdlet instead, which also supports moving folders and also across site collections. Move-PnPFolder can be used to move folders that are within the list view threshold, the commandlet will fail if the list view threshold is exceeded. 

## EXAMPLES

### EXAMPLE 1
```powershell
Move-PnPFolder -Folder Documents/Reports -TargetFolder 'Archived Reports'
```

This will move the folder Reports in the Documents library to the 'Archived Reports' library

### EXAMPLE 2
```powershell
Move-PnPFolder -Folder 'Shared Documents/Reports/2016/Templates' -TargetFolder 'Shared Documents/Reports'
```

This will move the folder Templates to the new location in 'Shared Documents/Reports'

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
The folder to move

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetFolder
The new parent location to which the folder should be moved to

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

