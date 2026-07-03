---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPFolderArchiveState.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPFolderArchiveState
---

# Set-PnPFolderArchiveState

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API (Delegated only): Files.ReadWrite, Files.Read, Files.Read.All, or Files.ReadWrite.All to archive folders, and Files.Read or Files.Read.All to reactivate folders

Archives or reactivates a folder in a document library in the current web by using the Microsoft Graph beta archive APIs.

## OUTPUTS

### PnP.PowerShell.Commands.Model.SharePoint.FolderArchiveStateResult

Returns a result object containing the folder name, server relative URL, requested archive state, and the monitor URL returned by Microsoft Graph.

## SYNTAX

```powershell
Set-PnPFolderArchiveState -Identity <FolderPipeBind> -ArchiveState <FolderArchiveState> [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

The Set-PnPFolderArchiveState cmdlet archives or reactivates a folder in a document library in the current web through the Microsoft Graph beta driveItem archive endpoints.

Folder archive and reactivation are asynchronous operations. Microsoft Graph requires the Prefer: respond-async header for folders and returns a monitor URL in the Location header so progress can be tracked until completion.

Only folders in document libraries in the current web are supported.

## EXAMPLES

### Example 1
```powershell
Set-PnPFolderArchiveState -Identity "/sites/Marketing/Shared Documents/QuarterlyReports" -ArchiveState Archived
```

This example archives the specified folder and returns the monitor URL for the asynchronous operation.

### Example 2
```powershell
Set-PnPFolderArchiveState -Identity "/sites/Marketing/Shared Documents/QuarterlyReports" -ArchiveState Active
```

This example reactivates the specified folder and returns the monitor URL for the asynchronous operation.

### Example 3
```powershell
Get-PnPFolder -Url "/sites/Marketing/Shared Documents/QuarterlyReports" | Set-PnPFolderArchiveState -ArchiveState Archived -Force
```

This example archives the specified folder coming from the pipeline without prompting for confirmation.

## PARAMETERS

### -Identity
Specifies the server relative URL, Folder instance or Id of the folder in a document library in the current web for which to change the archive state.

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -ArchiveState
Specifies the desired archive state of the folder. Valid values are Archived and Active.

```yaml
Type: FolderArchiveState
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