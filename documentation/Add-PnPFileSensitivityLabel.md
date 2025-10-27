---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFileSensitivityLabel.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFileSensitivityLabel
---

# Add-PnPFileSensitivityLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Files.ReadWrite.All, Sites.ReadWrite.All

**Required Billing**

  * Microsoft Graph API: The `assignSensitivityLabel` API used by this cmdlet won't function without billing setup. Consult the [RELATED LINKS](#related-links) section.

Add the sensitivity label information for a file in SharePoint.

## SYNTAX
```powershell
Add-PnPFileSensitivityLabel -Identity <String> -SensitivityLabelId <Guid> [-AssignmentMethod <Enum>] [-JustificationText <string>] [-Batch <PnPBatch>]
```

## DESCRIPTION

The Add-PnPFileSensitivityLabel cmdlet adds the sensitivity label information for a file in SharePoint using Microsoft Graph. It takes a URL as input, decodes it, and specifically encodes the '+' character if it is part of the filename. It also takes the sensitivity label Id , assignment method and justification text values as input. You can optionally provide a PnP batch so the label assignment is queued and executed together with other batched Graph calls.

## EXAMPLES

### Example 1
This example adds the sensitivity label information for the file at the specified URL.

```powershell
Add-PnPFileSensitivityLabel -Identity "/sites/Marketing/Shared Documents/Report.pptx" -SensitivityLabelId "b5b11b04-05b3-4fe4-baa9-b7f5f65b8b64" -JustificationText "Previous label no longer applies" -AssignmentMethod Privileged
```

### Example 2
This example removes the sensitivity label information for the file at the specified URL.

```powershell
Add-PnPFileSensitivityLabel -Identity "/sites/Marketing/Shared Documents/Report.pptx" -SensitivityLabelId "" -JustificationText "Previous label no longer applies" -AssignmentMethod Privileged
```

### Example 3
This example queues two label assignments in a single Microsoft Graph batch for improved throughput.

```powershell
$batch = New-PnPBatch

Get-PnPFile -Folder "/sites/Marketing/Shared Documents" -Recursive -Filter "*.pptx" | ForEach-Object {
  Add-PnPFileSensitivityLabel -Identity $_ -SensitivityLabelId "b5b11b04-05b3-4fe4-baa9-b7f5f65b8b64" -AssignmentMethod Privileged -Batch $batch
}

Invoke-PnPBatch -Batch $batch
```

## PARAMETERS

### -Identity
The server relative path to the file, the unique identifier of the file, the listitem representing the file, or the file object itself on which we are adding the sensitivity label.

```yaml
Type: FilePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -SensitivityLabelId
ID of the sensitivity label to be assigned, or empty string to remove the sensitivity label.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -AssignmentMethod
The assignment method of the label on the document. Indicates whether the assignment of the label was done automatically, standard, or as a privileged operation (the equivalent of an administrator operation).

```yaml
Type: Guid
Parameter Sets: (All)
Accepted values: Standard, Privileged, Auto
Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -JustificationText
Justification text for audit purposes, and is required when downgrading/removing a label.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Batch
Allows queueing the label assignment in an existing PnP batch. Use `Invoke-PnPBatch` to execute all queued operations.

```yaml
Type: PnPBatch
Parameter Sets: Batch

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

* [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

* [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/driveitem-assignsensitivitylabel)

* [Overview of metered APIs and services in Microsoft Graph](https://learn.microsoft.com/en-us/graph/metered-api-overview)

* [Metered APIs and services in Microsoft Graph](https://learn.microsoft.com/en-us/graph/metered-api-list)
