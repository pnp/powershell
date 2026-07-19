---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListItemComment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPListItemComment
---
  
# Get-PnPListItemComment

## SYNOPSIS
Retrieves all comments from the list item in the specified list.

## SYNTAX

```powershell
Get-PnPListItemComment -List <ListPipeBind> -Identity <ListItemPipeBind>
```

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListItemComment -List Tasks -Identity 1
```

Retrieves all comments for the specified list item from the Tasks list

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)