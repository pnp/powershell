---
Module Name: PnP.PowerShell
title: Remove-PnPFileVersion
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFileVersion.html
---
 
# Remove-PnPFileVersion

## SYNOPSIS
Removes all or a specific file version.

## SYNTAX

### Return as file object (Default)
```powershell
Remove-PnPFileVersion -Url <String> [-Recycle] [-Force] [-Connection <PnPConnection>]
 
```

### All
```powershell
Remove-PnPFileVersion -Url <String> [-All] [-Recycle] [-Force] 
 [-Connection <PnPConnection>] 
```

### By Id
```powershell
Remove-PnPFileVersion -Url <String> [-Identity <FileVersionPipeBind>] [-Recycle] [-Force] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet removes all versions or one specific version for the specified file.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPFileVersion -Url Documents/MyDocument.docx -Identity 512
```

Removes the file version with Id 512.

### EXAMPLE 2
```powershell
Remove-PnPFileVersion -Url Documents/MyDocument.docx -Identity "Version 1.0"
```

Removes the file version with label "Version 1.0".

### EXAMPLE 3
```powershell
Remove-PnPFileVersion -Url Documents/MyDocument.docx -All
```

Removes all file versions.

## PARAMETERS

### -All
Specifies whether all file versions should be removed.

```yaml
Type: SwitchParameter
Parameter Sets: All

Required: False
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

### -Force
If provided, no confirmation will be requested and the action will be performed.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the version. Use ID or label.

```yaml
Type: FileVersionPipeBind
Parameter Sets: By Id

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recycle
Specifies whether the version(s) should go to the recycle bin.

```yaml
Type: SwitchParameter
Parameter Sets: By Id

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
Server relative url or site relative url of the file.
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

