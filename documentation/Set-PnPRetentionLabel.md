---
Module Name: PnP.PowerShell
title: Set-PnPRetentionLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPRetentionLabel.html
---
 
# Set-PnPRetentionLabel

## SYNOPSIS
Sets a retention label on the specified list or library, or on specified items within a list or library. Use ReSet-PnPRetentionLabel to remove the label again.

## SYNTAX

### Set on a list
```powershell
Set-PnPRetentionLabel [-List] <ListPipeBind> -Label <String> [-SyncToItems <Boolean>] [-BlockDeletion <Boolean>]
 [-BlockEdit <Boolean>] [-Connection <PnPConnection>] 
```

### Set on items in bulk
```powershell
Set-PnPRetentionLabel [-List] <ListPipeBind> -Label <String> -ItemIds <List<Int32>> [-BatchSize <Int32>] 
 [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Allows setting a retention label on a list or library and its items, or sets the retention label for specified items in a list or a library. Does not work for sensitivity labels.
When setting retention label to specified items, cmdlet allows passing of unlimited number of items - items will be split and processed in batches (CSOM method SetComplianceTagOnBulkItems has a hard count limit on number of processed items in one go). If needed, batch size may be adjusted with BatchSize parameter.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPRetentionLabel -List "Demo List" -Label "Project Documentation"
```

This sets an O365 label on the specified list or library. 

### EXAMPLE 2
```powershell
Set-PnPRetentionLabel -List "Demo List" -Label "Project Documentation" -SyncToItems $true
```

This sets an O365 label on the specified list or library and sets the label to all the items in the list and library as well.

### EXAMPLE 3
```powershell
Set-PnPRetentionLabel -List "Demo List" -ItemIds @(1,2,3) -Label "My demo label"
```

Sets "My demo label" retention label for items with ids 1, 2 and 3 on a list "Demo List"

## PARAMETERS

### -BatchSize
Optional batch size when setting a label on specified items.

```yaml
Type: Int32
Parameter Sets: (BulkItems)

Required: True
Position: Named
Default value: 25
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockDeletion
Block deletion of items in the library. This parameter has been deprecated because overriding Purview retention label settings has been deprecated in SharePoint Online. This parameter will be removed in the next major release.

```yaml
Type: Boolean
Parameter Sets: (List)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockEdit
Block editing of items in the library. This parameter has been deprecated because overriding Purview retention label settings has been deprecated in SharePoint Online. This parameter will be removed in the next major release.

```yaml
Type: Boolean
Parameter Sets: (List)

Required: False
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
### -ItemIds
List of iist item IDs to set label. 

```yaml
Type: List<Int32>
Parameter Sets: (BulkItems)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Label
The name of the retention label

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
The ID or Url of the list.

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
Apply label to existing items in the library

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

