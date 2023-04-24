---
Module Name: PnP.PowerShell
title: Reset-PnPLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Reset-PnPLabel.html
---
 
# Reset-PnPLabel

## SYNOPSIS
Resets a retention label on the specified list or library to None

## SYNTAX

```powershell
Reset-PnPLabel [-List] <ListPipeBind> [-SyncToItems <Boolean>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Removes the retention label on a list or library and its items. Does not work for sensitivity labels.

## EXAMPLES

### EXAMPLE 1
```powershell
Reset-PnPLabel -List "Demo List"
```

This resets an O365 label on the specified list or library to None

### EXAMPLE 2
```powershell
Reset-PnPLabel -List "Demo List" -SyncToItems $true
```

This resets an O365 label on the specified list or library to None and resets the label on all the items in the list and library except Folders and where the label has been manually or previously automatically assigned

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

### -List
The ID or Url of the list

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SyncToItems
Reset label on existing items in the library

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

