---
Module Name: PnP.PowerShell
title: Remove-PnPListItemComment
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPListItemComment.html
---
 
# Remove-PnPListItemComment

## SYNOPSIS
Deletes a comment or all comments from a list item in a SharePoint list.

## SYNTAX

### Single
```powershell
Remove-PnPListItemComment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Text] [-Force] 
```

### All
```powershell
Remove-PnPListItemComment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-All] [-Force]
```

## DESCRIPTION

Allows to remove comments from list item.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -Text "test comment" -Force
```

Removes the comment with text "test comment" from list item with id "1" from the "Demo List" list. The text needs to be case sensitive. It may not work with comments containing mentions.

### EXAMPLE 2
```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -Text "test comment"
```

Removes the comment with text "test comment" from list item with id "1" from the "Demo List" list after asking for confirmation. The text needs to be case sensitive. It will may work with comments containing mentions.

### EXAMPLE 3
```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -All -Force
```

Removes all comments from list item with id "1" from the "Demo List" list.

### EXAMPLE 4
```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -All
```

Removes all comments from list item with id "1" from the "Demo List" list after asking for confirmation.

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
Specifying the Force parameter will skip the confirmation question

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

### -List
The ID, Title or Url of the list

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Text
When provided, item comments with specified text will be deleted. The text is case sensitive. If the comment contains mentions, it may not work.

```yaml
Type: String
Parameter Sets: Single

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -All
When specified, it will delete all comments for the specified list item.

```yaml
Type: SwitchParameter
Parameter Sets: Multiple

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
