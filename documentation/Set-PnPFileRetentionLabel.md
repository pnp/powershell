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

Retrieves the retention label information for a file in SharePoint.

## SYNTAX
```powershell
Set-PnPFileRetentionLabel -Url <String> [-RecordLocked <Boolean>] [-retentionLabel <String>] [-Connection <PnPConnection>]
```

## DESCRIPTION

The Set-PnPFileRetentionLabel cmdlet updates the retention label information or locks/unlocks a file in SharePoint using Microsoft Graph. It takes a URL as input, decodes it, and specifically encodes the '+' character if it is part of the filename.

## EXAMPLES

### Example 1
This example locks the file at the specified URL.

```powershell
Set-PnPFileRetentionLabel -Url "/sites/Marketing/Shared Documents/Report.pptx" -RecordLocked $true
```

### Example 2
This example updates the retention label information for the file at the specified URL.

```powershell
Set-PnPFileRetentionLabel -Url "/sites/Marketing/Shared Documents/Report.pptx" -retentionLabel "Finance"
```

## PARAMETERS

### -Url
Specifies the URL of the file for which to retrieve the retention label information.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -RecordLocked
Specifies whether to lock or unlock the file.
```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -retentionLabel
Specifies the retention label to apply to the file.
```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
