---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListItemAttachment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPListItemAttachment
---
  
# Get-PnPListItemAttachment

## SYNOPSIS
Downloads the list item attachments to a specified path on the file system.

## SYNTAX

### Get attachments from list item
```powershell
Get-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Path <String>] [-Force <SwitchParameter>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to download the list item attachments to a specified path. Use `Force` option in order to skip the confirmation question and overwrite the files on the local disk, if they already exist.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListItemAttachment -List "Demo List" -Identity 1 -Path "C:\temp"
```

Downloads all attachments from the list item with Id "1" in the "Demo List" SharePoint list and stores them in the temp folder.

### EXAMPLE 2
```powershell
Get-PnPListItemAttachment -List "Demo List" -Identity 1 -Path "C:\temp" -Force
```

Downloads all attachments from the list item with Id "1" in the "Demo List" SharePoint list and stores them in the temp folder overwriting the files if they already exist.

## PARAMETERS

### -Path
Specify the path on the local file system to download the list item attachments to.

```yaml
Type: String
Parameter Sets: (All)

Required: False
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
The ID of the listitem, or actual ListItem object

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Specifying the Force parameter will skip the confirmation question and overwrite the files on the local disk, if they already exist.

```yaml
Type: String
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
