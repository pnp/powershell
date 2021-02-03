---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFile.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFile
---
  
# Get-PnPFile

## SYNOPSIS
Downloads a file.

## SYNTAX

### Return as file object (Default)
```powershell
Get-PnPFile [-Url] <String> -AsFileObject [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Return as list item
```powershell
Get-PnPFile [-Url] <String> -AsListItem [-ThrowExceptionIfFileNotFound] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Save to local path
```powershell
Get-PnPFile [-Url] <String> -AsFile [-Path <String>] [-Filename <String>] [-Force] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Return as string
```powershell
Get-PnPFile [-Url] <String> -AsString [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFile -Url /sites/project/_catalogs/themes/15/company.spcolor
```

Retrieves the file and downloads it to the current folder

### EXAMPLE 2
```powershell
Get-PnPFile -Url /sites/project/_catalogs/themes/15/company.spcolor -Path c:\temp -FileName company.spcolor -AsFile
```

Retrieves the file and downloads it to c:\temp\company.spcolor

### EXAMPLE 3
```powershell
Get-PnPFile -Url /sites/project/_catalogs/themes/15/company.spcolor -AsString
```

Retrieves the file and outputs its contents to the console

### EXAMPLE 4
```powershell
Get-PnPFile -Url /sites/project/_catalogs/themes/15/company.spcolor -AsFile
```

Retrieves the file and returns it as a File object

### EXAMPLE 5
```powershell
Get-PnPFile -Url /sites/project/_catalogs/themes/15/company.spcolor -AsListItem
```

Retrieves the file and returns it as a ListItem object

### EXAMPLE 6
```powershell
Get-PnPFile -Url _catalogs/themes/15/company.spcolor -Path c:\temp -FileName company.spcolor -AsFile
```

Retrieves the file by site relative URL and downloads it to c:\temp\company.spcolor

## PARAMETERS

### -AsFile

```yaml
Type: SwitchParameter
Parameter Sets: Save to local path

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsFileObject
Retrieve the file contents as a file object.

```yaml
Type: SwitchParameter
Parameter Sets: Return as file object

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsListItem
Returns the file as a listitem showing all its properties

```yaml
Type: SwitchParameter
Parameter Sets: Return as list item

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsString
Retrieve the file contents as a string

```yaml
Type: SwitchParameter
Parameter Sets: Return as string

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

### -Filename
Name for the local file

```yaml
Type: String
Parameter Sets: Save to local path

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overwrites the file if it exists.

```yaml
Type: SwitchParameter
Parameter Sets: Save to local path

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Local path where the file should be saved

```yaml
Type: String
Parameter Sets: Save to local path

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ThrowExceptionIfFileNotFound
If provided in combination with -AsListItem, a System.ArgumentException will be thrown if the file specified in the -Url argument does not exist. Otherwise it will return nothing instead.

```yaml
Type: SwitchParameter
Parameter Sets: Return as list item

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The URL (server or site relative) to the file

```yaml
Type: String
Parameter Sets: (All)
Aliases: ServerRelativeUrl, SiteRelativeUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


