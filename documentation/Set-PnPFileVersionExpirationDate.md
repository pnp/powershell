---
Module Name: PnP.PowerShell
title: Set-PnPFileVersionExpirationDate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPFileVersionExpirationDate.html
---

# Set-PnPFileVersionExpirationDate

## SYNOPSIS

Keeps the file version with option to set an expiry date.

## Syntax

```powershell
Set-PnPFileVersionExpirationDate [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Version] <ListItemVersionPipeBind> [[-ExpirationDate] <DateTime>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet keeps the file version with option to set an expiry date.

## Examples

### Example 1
```powershell
Set-PnPFileVersionExpirationDate -List "Documents" -Identity 1 -Version "1.0" -ExpirationDate "2025-12-31"
```

Sets the expiration date for version 1.0 of list item with ID 1 in the Documents library to December 31, 2025.

### Example 2
```powershell
Set-PnPFileVersionExpirationDate -List "Documents" -Identity 1 -Version "1.0"
```

Clears the expiration date for version 1.0 of list item with ID 1 in the Documents library, setting the version to `Never expires`.

## Parameters

### -List

The library to retrieve the file from.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Identity

The ID of the file to update.

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```


### -Version

The version of the file to modify. This can be specified by version label (e.g. "1.0") or version ID.

```yaml
Type: ListItemVersionPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -ExpirationDate

The new expiration date for the version. If not specified, the expiration date will be cleared.

```yaml
Type: DateTime
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

## Outputs

This cmdlet does not produce any output.

## Related Links

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

