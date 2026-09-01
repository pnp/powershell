---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPListItemComment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPListItemComment
---
  
# Add-PnPListItemComment

## SYNOPSIS
Adds a comment to the specified list item in the SharePoint list

## SYNTAX

### Single
```powershell
Add-PnPListItemComment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Text] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add comment to the specified list item.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPListItemComment -List "Demo List" -Identity "1" -Text "Hello world"
```

Adds a new comment to the list item with Id "1" in the "Demo List" SharePoint list.

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

### -Text
Specify text of the comment for the list item.

```yaml
Type: String
Parameter Sets: (All)

Required: True
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