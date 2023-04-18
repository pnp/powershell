---
Module Name: PnP.PowerShell
title: Set-PnPLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPLabel.html
---
 
# Set-PnPLabel

## SYNOPSIS
Sets a retention label on the specified list or library. Use Reset-PnPLabel to remove the label again.

## SYNTAX

```powershell
Set-PnPLabel [-List] <ListPipeBind> -Label <String> [-SyncToItems <Boolean>] [-BlockDeletion <Boolean>]
 [-BlockEdit <Boolean>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows setting a retention label on a list or library and its items. Does not work for sensitivity labels.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPLabel -List "Demo List" -Label "Project Documentation"
```

This sets an O365 label on the specified list or library. 

### EXAMPLE 2
```powershell
Set-PnPLabel -List "Demo List" -Label "Project Documentation" -SyncToItems $true
```

This sets an O365 label on the specified list or library and sets the label to all the items in the list and library as well.

## PARAMETERS

### -BlockDeletion
Block deletion of items in the library. This parameter has been deprecated because overriding Purview retention label settings has been deprecated in SharePoint Online. This parameter will be removed in the next major release.

```yaml
Type: Boolean
Parameter Sets: (All)

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
Parameter Sets: (All)

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

