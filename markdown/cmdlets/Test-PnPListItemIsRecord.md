---
Module Name: PnP.PowerShell
title: Test-PnPListItemIsRecord
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Test-PnPListItemIsRecord.html
---
 
# Test-PnPListItemIsRecord

## SYNOPSIS
Checks if a list item is a record

## SYNTAX

```powershell
Test-PnPListItemIsRecord [-List] <ListPipeBind> -Identity <ListItemPipeBind> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to check if the specified list item is a record.

## EXAMPLES

### EXAMPLE 1
```powershell
Test-PnPListItemIsRecord -List "Documents" -Identity 4
```

Returns true if the document in the documents library with id 4 is a record

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

