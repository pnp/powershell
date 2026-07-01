---
Module Name: PnP.PowerShell
title: Restore-PnPFileVersion
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Restore-PnPFileVersion.html
---
 
# Restore-PnPFileVersion

## SYNOPSIS
Restores a specific file version.

## SYNTAX

```powershell
Restore-PnPFileVersion -Url <String> -Identity <FileVersionPipeBind> [-Force] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet restores a specific file version.

## EXAMPLES

### EXAMPLE 1
```powershell
Restore-PnPFileVersion -Url Documents/MyDocument.docx -Identity 512
```

Restores the file version with Id 512.

### EXAMPLE 2
```powershell
Restore-PnPFileVersion -Url /sites/HRSite/Documents/MyDocument.docx -Identity 512
```

Restores the file version with Id 512 for MyDocument.docx.

### EXAMPLE 3
```powershell
Restore-PnPFileVersion -Url Documents/MyDocument.docx -Identity "Version 1.0"
```

Restores the file version with label "Version 1.0".

## PARAMETERS

### -Identity
The identity of the version. Use ID or label.

```yaml
Type: FileVersionPipeBind
Parameter Sets: (All)

Required: True
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)