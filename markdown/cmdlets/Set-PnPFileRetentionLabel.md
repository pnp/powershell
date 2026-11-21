---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPFileRetentionLabel.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPFileRetentionLabel
---

# Set-PnPFileRetentionLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Files.Read.All, Sites.Read.All, Files.ReadWrite.All, Sites.ReadWrite.All

Allows setting a retention label on a file in SharePoint or locking/unlocking it.

## SYNTAX

### Lock or unlock a file
```powershell
Set-PnPFileRetentionLabel -Identity <FilePipeBind> -RecordLocked <Boolean> [-Connection <PnPConnection>]
```

### Set a retention label on a file
```powershell
Set-PnPFileRetentionLabel -Identity <FilePipeBind> -RetentionLabel <String> [-Connection <PnPConnection>]
```

## DESCRIPTION

The Set-PnPFileRetentionLabel cmdlet updates the retention label information or locks/unlocks a file in SharePoint using Microsoft Graph. It takes a URL as input, decodes it, and specifically encodes the '+' character if it is part of the filename.

## EXAMPLES

### Example 1
```powershell
Set-PnPFileRetentionLabel -Url "/sites/Marketing/Shared Documents/Report.pptx" -RecordLocked $true
```

This example locks the file at the specified URL.

### Example 2
```powershell
Set-PnPFileRetentionLabel -Identity "/sites/Marketing/Shared Documents/Report.pptx" -RetentionLabel "Finance"
```

This example updates the retention label information for the file at the specified URL.

### Example 3
```powershell
Set-PnPFileRetentionLabel -Identity "/sites/Marketing/Shared Documents/Report.pptx" -RetentionLabel ""
```

This example removes the retention label information from the file at the specified URL.

## PARAMETERS

### -Identity
Specifies the server relative URL, File instance, listitem instance or Id of the file for which to set the retention label information or change the locking state.

```yaml
Type: FilePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -RecordLocked
Specifies whether to lock or unlock the file. If omitted, the file is not locked or unlocked.

```yaml
Type: Boolean
Parameter Sets: Lock or unlock a file
Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -RetentionLabel
Specifies the retention label to apply to the file. Provide an empty string or $null to remove the existing label.

```yaml
Type: String
Parameter Sets: Set a retention label on a file
Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Setting a retention label through Microsoft Graph](https://learn.microsoft.com/graph/api/driveitem-setretentionlabel)
[Removing a retention label through Microsoft Graph](https://learn.microsoft.com/graph/api/driveitem-removeretentionlabel)
[Locking or unlocking a file through Microsoft Graph](https://learn.microsoft.com/graph/api/driveitem-lockorunlockrecord)