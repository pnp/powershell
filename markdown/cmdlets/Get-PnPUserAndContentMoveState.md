---
Module Name: PnP.PowerShell
title: Get-PnPUserAndContentMoveState
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUserAndContentMoveState.html
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
 
# Get-PnPUserAndContentMoveState

## SYNOPSIS
Returns the state of SharePoint Online user and OneDrive content move jobs.

## SYNTAX

### MoveReport

```powershell
Get-PnPUserAndContentMoveState [-Limit <UInt32>] [-MoveStartTime <DateTime>] [-MoveEndTime <DateTime>] [-MoveState <MoveState>] [-MoveDirection <MoveDirection>] [-Connection <PnPConnection>]
```

### UserPrincipalName

```powershell
Get-PnPUserAndContentMoveState -UserPrincipalName <String> [-Connection <PnPConnection>]
```

### OdbMoveId

```powershell
Get-PnPUserAndContentMoveState -OdbMoveId <Guid> [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns status information for SharePoint Online multi-geo user and OneDrive content move jobs. You can retrieve one move job by user principal name or OneDrive move ID, or retrieve a move report filtered by state, direction, time window, and limit. When no move state or direction is specified, all states and directions are returned.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPUserAndContentMoveState -UserPrincipalName user@contoso.com
```

Returns the move state for the specified user.

### EXAMPLE 2

```powershell
Get-PnPUserAndContentMoveState -OdbMoveId 8f6f8e3a-2c1f-4d5b-9a7e-6b3c2a1f0e9d
```

Returns the move state for the specified OneDrive move job ID.

### EXAMPLE 3

```powershell
Get-PnPUserAndContentMoveState -MoveState All -MoveDirection All -Limit 100
```

Returns up to 100 user and content move jobs regardless of state or direction.

### EXAMPLE 4

```powershell
Get-PnPUserAndContentMoveState -MoveStartTime (Get-Date).AddDays(-7) -MoveEndTime (Get-Date) -MoveState Failed -MoveDirection MoveOut -Verbose
```

Returns failed move-out jobs from the last seven days and includes additional diagnostic properties.

## PARAMETERS

### -UserPrincipalName
The user principal name of the user whose move state should be retrieved.

```yaml
Type: String
Parameter Sets: UserPrincipalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OdbMoveId
The OneDrive move job ID whose state should be retrieved.

```yaml
Type: Guid
Parameter Sets: OdbMoveId

Required: True
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

## OUTPUTS

### System.Management.Automation.PSObject
Returns objects with `UserPrincipalName`, `MoveJobId`, `SourceDataLocation`, `DestinationDataLocation`, `TimeStamp`, and `MoveState` properties. Validation-only move jobs return `ValidationState` instead of `TimeStamp` and `MoveState`. When `-Verbose` is specified, additional move job details are returned.

## RELATED LINKS

[Start-PnPUserAndContentMove](Start-PnPUserAndContentMove.md)

[Stop-PnPUserAndContentMove](Stop-PnPUserAndContentMove.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

