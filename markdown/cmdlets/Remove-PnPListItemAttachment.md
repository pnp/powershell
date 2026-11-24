---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPListItemAttachment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPListItemAttachment
---
  
# Remove-PnPListItemAttachment

## SYNOPSIS
Removes attachment from the specified list item in the SharePoint list.

## SYNTAX

### Remove attachment from list item
```powershell
Remove-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-FileName <String>] [-Recycle <SwitchParameter>] [-Force <SwitchParameter>] [-Connection <PnPConnection>] 
```

### Remove all attachment files from list item
```powershell
Remove-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-All <SwitchParameter>] [-Recycle <SwitchParameter>] [-Force <SwitchParameter>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet removes one or all attachments from the specified list item in a SharePoint list.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName test.txt
```

Permanently delete an attachment from the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt.

### EXAMPLE 2
```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName test.txt -Recycle
```

Removes an attachment from the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt and sends it to recycle bin.

### EXAMPLE 3
```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName test.txt -Recycle -Force
```

Removes an attachment from the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt and sends it to recycle bin. It will not ask for confirmation from user.

### EXAMPLE 4
```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -All -Recycle -Force
```

Removes all attachments from the list item with Id "1" in the "Demo List" SharePoint list and sends them to recycle bin. It will not ask for confirmation from user.

### EXAMPLE 5
```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -All
```

Permanently deletes all attachments from the list item with Id "1" in the "Demo List" SharePoint list and sends them to recycle bin.

## PARAMETERS

### -FileName
Specify name of the attachment to delete from list item. The filename is not case sensitive.

```yaml
Type: String
Parameter Sets: (Single)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -All
Specify if you want to delete or recycle all the list item attachments.

```yaml
Type: SwitchParameter
Parameter Sets: (Multiple)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recycle
Specify if you want to send the attachment(s) to the recycle bin.

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
Specifying the Force parameter will skip the confirmation question.

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
The ID, Title or Url of the list. Note that when providing the name of the list, the list name is case-sensitive.

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
The ID of the list item, or actual ListItem object.

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
