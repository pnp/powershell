---
Module Name: PnP.PowerShell
title: Remove-PnPFile
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFile.html
---
 
# Remove-PnPFile

## SYNOPSIS
Removes a file.

## SYNTAX

### Server Relative
```powershell
Remove-PnPFile [-ServerRelativeUrl] <String> [-Recycle] [-Force] [-Connection <PnPConnection>] 
```

### Site Relative
```powershell
Remove-PnPFile [-SiteRelativeUrl] <String> [-Recycle] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet removes the specified file based on the site-relative or server-relative url.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPFile -ServerRelativeUrl /sites/project/_catalogs/themes/15/company.spcolor
```

Removes the file company.spcolor.

### EXAMPLE 2
```powershell
Remove-PnPFile -SiteRelativeUrl _catalogs/themes/15/company.spcolor
```

Removes the file company.spcolor.

### EXAMPLE 3
```powershell
Remove-PnPFile -SiteRelativeUrl _catalogs/themes/15/company.spcolor -Recycle
```

Removes the file company.spcolor and saves it to the Recycle Bin.

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

### -Force
If provided, no confirmation will be asked to remove the file, but instead it will silently be removed.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recycle
When provided, the file will be moved to recycle bin. If omitted, the file will be deleted directly.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativeUrl
Server relative URL of the file.

```yaml
Type: String
Parameter Sets: Server Relative

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteRelativeUrl
Site relative URL of the file.

```yaml
Type: String
Parameter Sets: Site Relative

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
