---
Module Name: PnP.PowerShell
title: Set-PnPListItemAsRecord
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPListItemAsRecord.html
---
 
# Set-PnPListItemAsRecord

## SYNOPSIS
Declares a list item as a record, for more information, see https://learn.microsoft.com/sharepoint/governance/records-management-in-sharepoint-server

## SYNTAX

```powershell
Set-PnPListItemAsRecord [-List] <ListPipeBind> -Identity <ListItemPipeBind> [-DeclarationDate <DateTime>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

Allows to set a list item as a record.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPListItemAsRecord -List "Documents" -Identity 4
```

Declares the document in the documents library with id 4 as a record.

### EXAMPLE 2
```powershell
Set-PnPListItemAsRecord -List "Documents" -Identity 4 -DeclarationDate $date
```

Declares the document in the documents library with id 4 as a record.

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

### -DeclarationDate
The declaration date.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID of the listitem, or actual ListItem object.

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

