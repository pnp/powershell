---
Module Name: PnP.PowerShell
title: Remove-PnPFolder
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFolder.html
---
 
# Remove-PnPFolder

## SYNOPSIS
Deletes a folder within a parent folder.

## SYNTAX

```powershell
Remove-PnPFolder -Name <String> -Folder <FolderPipeBind> [-Recycle] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to remove a folder.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPFolder -Name NewFolder -Folder _catalogs/masterpage
```

Removes the folder 'NewFolder' from '_catalogsmasterpage'.

### EXAMPLE 2
```powershell
Remove-PnPFolder -Name NewFolder -Folder _catalogs/masterpage -Recycle
```

Removes the folder 'NewFolder' from '_catalogsmasterpage' and saves it in the Recycle Bin.

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
The parent folder in the site.

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The folder name.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recycle
When provided, the folder will be moved to the recycle bin. If omitted, the folder will be directly deleted.

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

