---
Module Name: PnP.PowerShell
title: Restore-PnPRecycleBinItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Restore-PnPRecycleBinItem.html
---
 
# Restore-PnPRecycleBinItem

## SYNOPSIS
Restores the provided recycle bin item to its original location.

## SYNTAX

```powershell
Restore-PnPRecycleBinItem -Identity <RecycleBinItemPipeBind> [-Force] [-RowLimit <Int32>]
 [-Connection <PnPConnection>] 
```
```powershell
Restore-PnPRecycleBinItem -IdList <string[]> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet restores the specified item or set of items from the recycle bin to its original location.

## EXAMPLES

### EXAMPLE 1
```powershell
Restore-PnPRecycleBinItem -Identity 72e4d749-d750-4989-b727-523d6726e442
```

Restores the recycle bin item with Id 72e4d749-d750-4989-b727-523d6726e442 to its original location.

### EXAMPLE 2
```powershell
Get-PnPRecycleBinItem | ? -Property LeafName -like "*.docx" | Restore-PnPRecycleBinItem
```

Restores all the items of which the filename ends with the .docx extension from the first and second stage recycle bins to their original location. 

### EXAMPLE 3
```powershell
Get-PnPRecycleBinItem -RowLimit 10000 | Restore-PnPRecycleBinItem -Force
```

Permanently restores up to 10,000 items in the recycle bin without asking for confirmation.

### EXAMPLE 4
```powershell
Restore-PnPRecycleBinItem -IdList @("31897b05-fd3b-4c49-9898-2e7f10e59cac","b16f0733-9b07-4ef3-a4b6-896edca4babd", "367ef9d2-6080-45ea-9a03-e8c9029f59dd")
```

Restores the recycle bin items with Id 31897b05-fd3b-4c49-9898-2e7f10e59cac, b16f0733-9b07-4ef3-a4b6-896edca4babd, 367ef9d2-6080-45ea-9a03-e8c9029f59dd to their original location.

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
If provided, no confirmation will be asked to restore the recycle bin item.

```yaml
Type: SwitchParameter
Parameter Sets: (Restore Single Item By Id)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Id of the recycle bin item or the recycle bin item object itself to restore.

```yaml
Type: RecycleBinItemPipeBind
Parameter Sets: (Restore Single Item By Id)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -RowLimit
Limits restoration to a specified number of items.

```yaml
Type: Int32
Parameter Sets: (Restore Single Item By Id)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```
### -IdList
String array of Recycle Bin Item Ids

```yaml
Type: String array
Parameter Sets: (Restore Multiple Items By Id)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

