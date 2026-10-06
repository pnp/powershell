---
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Move-PnPItemProxy.html
Module Name: PnP.PowerShell
schema: 2.0.0
title: Move-PnPItemProxy
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
---
   
# Move-PnPItemProxy

## SYNOPSIS
Moves files and folders between the local file system and a SharePoint drive. It is used through the `Move-Item` alias.

## SYNTAX

### Path (Default)
```powershell
Move-PnPItemProxy [-Path] <String[]> [[-Destination] <String>] [-Container] [-Force] [-Filter <String>]
 [-Include <String[]>] [-Exclude <String[]>] [-PassThru] [-Credential <PSCredential>] [-WhatIf] [-Confirm]
```

### LiteralPath
```powershell
Move-PnPItemProxy [-LiteralPath] <String[]> [[-Destination] <String>] [-Container] [-Force] [-Filter <String>]
 [-Include <String[]>] [-Exclude <String[]>] [-PassThru] [-Credential <PSCredential>] [-WhatIf] [-Confirm]
```

## DESCRIPTION

**This cmdlet stands in for the `Move-Item` cmdlet that is natively available with PowerShell.**

When a SharePoint drive is created, for instance with `Connect-PnPOnline -CreateDrive`, PnP PowerShell points the `Move-Item` alias to `Move-PnPItemProxy` for the session. Removing the last SharePoint drive removes the alias again, and a drive created with `New-PSDrive -PSProvider SharePoint -NoProxyCmdLets` leaves it alone. Paths on the drive are server relative: on a drive named SPO, the library at https://contoso.sharepoint.com/sites/project/Shared%20Documents is `SPO:\sites\project\Shared Documents`.

When items are moved from the file system to a SharePoint drive or from a SharePoint drive to the file system, folders are moved with all of their content, and the source is removed once everything has been copied. A file or folder removed from SharePoint this way is deleted permanently, not sent to the recycle bin, and without `-Force` that includes a file that was not downloaded because a file with the same name already existed locally. Any other move, including one between two locations in SharePoint, is passed on to `Move-Item`.

> [!WARNING]
> A move between the file system and a SharePoint drive removes the whole source, also the parts it did not copy:
>
> - from a SharePoint folder holding more than 5,000 items, only the first 100 are copied
> - hidden files in a local folder are not copied
> - a local source given as a UNC path, or on a drive other than a drive letter such as `Temp:`, is not copied at all
> - without `-Force`, a file that already exists locally is not downloaded
> - `-LiteralPath` expands wildcard characters, so `-LiteralPath "file[1].txt"` moves `file1.txt`
>
> To keep the source until you have checked the result, copy with `Copy-PnPItemProxy` and remove the source afterwards.

For more information on `Move-Item`, please refer to the official PowerShell documentation [here](https://learn.microsoft.com/powershell/module/microsoft.powershell.management/move-item).

## EXAMPLES

### EXAMPLE 1
```powershell
Move-Item -Path "C:\Reports\*.xlsx" -Destination "SPO:\sites\project\Shared Documents\Reports"
```

Uploads all Excel files in the local `C:\Reports` folder to the Reports folder of the Shared Documents library of the site at /sites/project, then deletes them from `C:\Reports`.

### EXAMPLE 2
```powershell
Move-PnPItemProxy -Path "SPO:\sites\project\Shared Documents\Archive" -Destination "C:\Archive" -Force
```

Downloads the Archive folder to `C:\Archive`, overwriting local files with the same name, then permanently deletes the Archive folder from SharePoint. See the warning above for the content that is not downloaded.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Container
Treats the last segment of `-Destination` as a folder, even when it contains a period. Without it, a destination whose last segment contains a period is taken to be a file name. Only used when moving between the file system and a SharePoint drive.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Credential
Passed on to `Move-Item`. Not used when moving between the file system and a SharePoint drive.

```yaml
Type: PSCredential
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Destination
The path to move the items to. When it is a file name, only a single file can be moved to it.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Exclude
Passed on to `Move-Item`. Not used when moving between the file system and a SharePoint drive.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter
Passed on to `Move-Item`. Not used when moving between the file system and a SharePoint drive.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overwrites files that already exist at the destination. Without it, a move to a SharePoint drive stops at the first file that already exists there. A move from a SharePoint drive to the file system, however, does not download a file that already exists locally, and still deletes it from SharePoint.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Include
Passed on to `Move-Item`. Not used when moving between the file system and a SharePoint drive.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LiteralPath
The path of the items to move.

```yaml
Type: String[]
Parameter Sets: LiteralPath
Aliases: PSPath

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -PassThru
Returns objects for the files and folders created at the destination. By default, this cmdlet does not generate any output.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path of the items to move. Wildcard characters are permitted.

```yaml
Type: String[]
Parameter Sets: Path

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue, ByPropertyName)
Accept wildcard characters: True
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

