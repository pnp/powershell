---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPFileArchiveState.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPFileArchiveState
---

# Set-PnPFileArchiveState

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API (Delegated only): Files.ReadWrite, Files.Read, Files.Read.All, or Files.ReadWrite.All to archive files, and Files.Read or Files.Read.All to reactivate files

Archives or reactivates a SharePoint file using the Microsoft Graph beta archive APIs.

## OUTPUTS

### PnP.PowerShell.Commands.Model.SharePoint.FileArchiveStateResult

Returns a result object containing the file name, server relative URL, requested archive state, and the resulting archive status.

## SYNTAX

```powershell
Set-PnPFileArchiveState -Identity <FilePipeBind> -ArchiveState <FileArchiveState> [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

The Set-PnPFileArchiveState cmdlet archives or reactivates a file in SharePoint through the Microsoft Graph beta driveItem archive endpoints.

When a file is archived, its metadata remains available but its contents can't be opened or downloaded until it is reactivated.
When a file is reactivated, the operation can complete immediately for recently archived files, or it can transition into a reactivation period that may take up to 24 hours.

This cmdlet targets files only. The Microsoft Graph archive endpoints support asynchronous folder operations through the Prefer: respond-async header, but that behavior is not used by this cmdlet.

## EXAMPLES

### Example 1
```powershell
Set-PnPFileArchiveState -Identity "/sites/Marketing/Shared Documents/Report.docx" -ArchiveState Archived
```

This example archives the specified file.

### Example 2
```powershell
Set-PnPFileArchiveState -Identity "/sites/Marketing/Shared Documents/Report.docx" -ArchiveState Active
```

This example reactivates the specified file. Depending on how long the file has been archived, it might become available immediately or enter a reactivation period.

### Example 3
```powershell
Get-PnPFileInFolder -FolderSiteRelativeUrl "Shared Documents" | Set-PnPFileArchiveState -ArchiveState Archived -Force
```

This example archives files coming from the pipeline without prompting for confirmation.

## PARAMETERS

### -Identity
Specifies the server relative URL, File instance, listitem instance or Id of the file for which to change the archive state.

```yaml
Type: FilePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -ArchiveState
Specifies the desired archive state of the file. Valid values are Archived and Active.

```yaml
Type: FileArchiveState
Parameter Sets: (All)

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
If provided, no confirmation will be asked before changing the archive state.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Archive driveItem through Microsoft Graph beta](https://learn.microsoft.com/en-us/graph/api/driveitem-archive?view=graph-rest-beta&tabs=http)
[Unarchive driveItem through Microsoft Graph beta](https://learn.microsoft.com/en-us/graph/api/driveitem-unarchive?view=graph-rest-beta&tabs=http)