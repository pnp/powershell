---
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
title: Get-PnPSiteContentMoveState
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteContentMoveState.html
---
 
# Get-PnPSiteContentMoveState

## SYNOPSIS
Returns the state of SharePoint Online site content move jobs.

## SYNTAX

### MoveReport

```powershell
Get-PnPSiteContentMoveState [-Limit <UInt32>] [-MoveStartTime <DateTime>] [-MoveEndTime <DateTime>] [-MoveState <MoveState>] [-MoveDirection <MoveDirection>] [-Connection <PnPConnection>]
```

### SourceSiteUrl

```powershell
Get-PnPSiteContentMoveState -SourceSiteUrl <String> [-Connection <PnPConnection>]
```

### SiteMoveId

```powershell
Get-PnPSiteContentMoveState -SiteMoveId <Guid> [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns status information for SharePoint Online multi-geo site content move jobs. You can retrieve one move job by source site URL or site move ID, or retrieve a move report filtered by state, direction, time window, and limit. When no move state or direction is specified, all states and directions are returned.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteContentMoveState -SourceSiteUrl https://contoso.sharepoint.com/sites/project
```

Returns the move state for the specified source site.

### EXAMPLE 2

```powershell
Get-PnPSiteContentMoveState -SiteMoveId 8f6f8e3a-2c1f-4d5b-9a7e-6b3c2a1f0e9d
```

Returns the move state for the specified site move job ID.

### EXAMPLE 3

```powershell
Get-PnPSiteContentMoveState -MoveState All -MoveDirection All -Limit 100
```

Returns up to 100 site content move jobs regardless of state or direction.

### EXAMPLE 4

```powershell
Get-PnPSiteContentMoveState -MoveStartTime (Get-Date).AddDays(-7) -MoveEndTime (Get-Date) -MoveState Failed -MoveDirection MoveOut -Verbose
```

Returns failed site move-out jobs from the last seven days and includes additional diagnostic properties.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Limit
Limits the number of move report entries returned. The value must be between 1 and 1000.

```yaml
Type: UInt32
Parameter Sets: MoveReport

Required: False
Position: Named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveDirection
Filters move report entries by move direction. Valid values are `MoveOut`, `MoveIn`, and `All`.

```yaml
Type: MoveDirection
Parameter Sets: MoveReport

Required: False
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveEndTime
Filters move report entries to moves ending at or before the specified date and time. The value is converted to UTC before it is sent to SharePoint Online.

```yaml
Type: DateTime
Parameter Sets: MoveReport

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveStartTime
Filters move report entries to moves starting at or after the specified date and time. The value is converted to UTC before it is sent to SharePoint Online.

```yaml
Type: DateTime
Parameter Sets: MoveReport

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveState
Filters move report entries by move state. Valid values are `NotStarted`, `InProgress`, `Success`, `Failed`, `Stopped`, `Queued`, `NotSupported`, `Rescheduled`, and `All`.

```yaml
Type: MoveState
Parameter Sets: MoveReport

Required: False
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteMoveId
The site move job ID whose state should be retrieved.

```yaml
Type: Guid
Parameter Sets: SiteMoveId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceSiteUrl
The source URL of the site collection whose move state should be retrieved.

```yaml
Type: String
Parameter Sets: SourceSiteUrl

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.Management.Automation.PSObject
Returns objects with `SourceSiteUrl`, `TargetSiteUrl`, `MoveJobId`, `SourceDataLocation`, `DestinationDataLocation`, `TimeStamp`, and `MoveState` properties. Validation-only move jobs return `ValidationState` instead of `TimeStamp` and `MoveState`. When `-Verbose` is specified, additional move job details are returned.

## RELATED LINKS

[Get-PnPUserAndContentMoveState](Get-PnPUserAndContentMoveState.md)

[Get-PnPUnifiedGroupMoveState](Get-PnPUnifiedGroupMoveState.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

