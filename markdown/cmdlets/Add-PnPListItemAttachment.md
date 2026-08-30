---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPListItemAttachment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPListItemAttachment
---
  
# Add-PnPListItemAttachment

## SYNOPSIS
Adds an attachment to the specified list item in the SharePoint list

## SYNTAX

### Upload attachment file from path
```powershell
Add-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Path <String>] [-NewFileName <String>] [-Connection <PnPConnection>] 
```

### Upload attachment file from stream
```powershell
Add-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-FileName <String>] [-Stream <Stream>] [-Connection <PnPConnection>] 
```

### Create attachment file from text
```powershell
Add-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-FileName <String>] [-Content <text>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet allows adding a file as an attachment to a list item in a SharePoint Online list.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPListItemAttachment -List "Demo List" -Identity 1 -Path c:\temp\test.mp4
```

Adds a new attachment to the list item with Id "1" in the "Demo List" SharePoint list with file name as test.mp4 from the specified path.


### EXAMPLE 2
```powershell
Add-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName "test.txt" -Content '{ "Test": "Value" }'
```

Adds a new attachment to the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt and content as specified.

### EXAMPLE 3
```powershell
Add-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName "test.mp4" -Stream $fileStream
```

Adds a new attachment to the list item with Id "1" in the "Demo List" SharePoint list with file name as test.mp4 and content coming from a stream.

## PARAMETERS

### -Content
Specify text of the attachment for the list item.

```yaml
Type: String
Parameter Sets: (Upload file from text)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The local file path

```yaml
Type: String
Parameter Sets: (Upload file)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NewFileName
Filename to give to the attachment file on SharePoint

```yaml
Type: String
Parameter Sets: (Upload file)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileName
Filename to give to the attachment file on SharePoint

```yaml
Type: String
Parameter Sets: (Upload file from stream, Upload file from text)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stream
Stream with the file contents

```yaml
Type: Stream
Parameter Sets: (Upload file from stream)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The ID, Title or Url of the list. Note that when providing the name of the list, the name is case-sensitive.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Identity
The ID of the listitem, or actual ListItem object to add the attachment to.

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
