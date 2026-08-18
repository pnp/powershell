---
Module Name: PnP.PowerShell
title: Reset-PnPRetentionLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Reset-PnPRetentionLabel.html
---
 
# Reset-PnPRetentionLabel

## SYNOPSIS
Resets a retention label on the specified list or library to None, or resets a retention label on specified list items in a list or a library

## SYNTAX

### Reset on a list
```powershell
Reset-PnPRetentionLabel [-List] <ListPipeBind> [-SyncToItems <Boolean>] 
 [-Connection <PnPConnection>] 
```

### Reset on items in bulk
```powershell
Reset-PnPRetentionLabel [-List] <ListPipeBind> -ItemIds <List<Int32>> [-BatchSize <Int32>] 
 [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Removes the retention label on a list or library and its items, or removes the retention label from specified items in a list or a library. Does not work for sensitivity labels.
When resetting retention label on specified items, cmdlet allows passing of unlimited number of items - items will be split and processed in batches (CSOM method SetComplianceTagOnBulkItems has a hard count limit on number of processed items in one go). If needed, batch size may be adjusted with BatchSize parameter.

## EXAMPLES

### EXAMPLE 1
```powershell
Reset-PnPRetentionLabel -List "Demo List"
```

This resets an O365 label on the specified list or library to None

### EXAMPLE 2
```powershell
Reset-PnPRetentionLabel -List "Demo List" -SyncToItems $true
```

This resets an O365 label on the specified list or library to None and resets the label on all the items in the list and library except Folders and where the label has been manually or previously automatically assigned

### EXAMPLE 3
```powershell
Set-PnPRetentionLabel -List "Demo List" -ItemIds @(1,2,3)
```

This clears a retention label from items with ids 1, 2 and 3 on a list "Demo List"

## PARAMETERS

### -BatchSize
Optional batch size when resetting a label on specified items.

```yaml
Type: Int32
Parameter Sets: (BulkItems)

Required: True
Position: Named
Default value: 25
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

### -ItemIds
List of iist item IDs to reset label. 

```yaml
Type: List<Int32>
Parameter Sets: (BulkItems)

Required: True
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
Parameter Sets: (List)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Learn article on applying retention labels](https://learn.microsoft.com/en-us/sharepoint/dev/apis/csom-methods-for-applying-retention-labels)


