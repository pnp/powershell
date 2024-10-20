---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Unlock-PnPListItemRecord.html
external help file: PnP.PowerShell.dll-Help.xml
title: Unlock-PnPListItemRecord
---
  
# Unlock-PnPListItemRecord

## SYNOPSIS
Unlocks the list item record


## SYNTAX

```powershell
Unlock-PnPListItemRecord [-List] <ListPipeBind> -Identity <ListItemPipeBind> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Unlocks the list item record

## EXAMPLES

### EXAMPLE 1
```powershell
Unlock-PnPListItemRecord -List "Documents" -Identity 4
```

Unlocks the document in the documents library with id 4.

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
The ID of the listitem, or actual ListItem object to be unlocked

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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


