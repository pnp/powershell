---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/find-pnpfile
schema: 2.0.0
title: Find-PnPFile
---

# Find-PnPFile

## SYNOPSIS
Finds a file in the virtual file system of the web.

## SYNTAX

### Web (Default)
```powershell
Find-PnPFile -Match <String> [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### List
```powershell
Find-PnPFile -Match <String> -List <ListPipeBind> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Folder
```powershell
Find-PnPFile -Match <String> -Folder <FolderPipeBind> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

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

### EXAMPLE 4
```powershell
Find-PnPFile -Match *newpnp*
```

Will return all files containing "newpnp" in the file name located in the current web.

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

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
