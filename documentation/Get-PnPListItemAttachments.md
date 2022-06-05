---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListItemAttachments.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPListItemAttachments
---
  
# Get-PnPListItemAttachments

## SYNOPSIS
Downloads the list item attachments to a specified path on the file system.

## SYNTAX

### Get attachments from list item
```powershell
Get-PnPListItemAttachments [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Path <String>] [-Force <SwitchParameter>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListItemAttachments -List "Demo List" -Identity 1 -Path "C:\temp"
```

Download all attachments from the list item with Id "1" in the "Demo List" SharePoint list and store it in the temp folder.

### EXAMPLE 2
```powershell
Get-PnPListItemAttachments -List "Demo List" -Identity 1 -Path "C:\temp" -Force
```

Download all attachments from the list item with Id "1" in the "Demo List" SharePoint list and store it in the temp folder and overwrite the files if they already exist.

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

### -Path
Specify the path on the local file system to download the list item attachments.

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
Specifying the Force parameter will skip the confirmation question and overwrite the file in file system.

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
The ID, Title or Url of the list.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)