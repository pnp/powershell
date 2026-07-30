---
Module Name: PnP.PowerShell
schema: 2.0.0
title: Start-PnPUserAndContentMove
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Start-PnPUserAndContentMove.html
external help file: PnP.PowerShell.dll-Help.xml
---
 
# Start-PnPUserAndContentMove

## SYNOPSIS
Starts a SharePoint Online multi-geo user and OneDrive content move job.

## SYNTAX

```powershell
Start-PnPUserAndContentMove [-UserPrincipalName] <String> [-DestinationDataLocation] <String> [[-PreferredMoveBeginDate] <DateTime>] [[-PreferredMoveEndDate] <DateTime>] [[-Notify] <String>] [[-Reserved] <String>] [-ValidationOnly] [-Connection <PnPConnection>]
```

## DESCRIPTION
Starts a SharePoint Online multi-geo move job for a user and the user's OneDrive content to the specified destination data location.

Use `-ValidationOnly` to validate whether the user and content can be moved without starting the move. Use `Get-PnPUserAndContentMoveState` to retrieve move job status after the job has been created.

## EXAMPLES

### EXAMPLE 1

```powershell
Start-PnPUserAndContentMove -UserPrincipalName user@contoso.com -DestinationDataLocation EUR
```

Starts a move job for the specified user and OneDrive content to the `EUR` data location.

### EXAMPLE 2

```powershell
Start-PnPUserAndContentMove -UserPrincipalName user@contoso.com -DestinationDataLocation EUR -PreferredMoveBeginDate "2026-06-20T22:00:00" -PreferredMoveEndDate "2026-06-21T04:00:00"
```

Starts a move job with a preferred move window. The preferred dates are converted to UTC before being sent to SharePoint Online.

### EXAMPLE 3

```powershell
Start-PnPUserAndContentMove -UserPrincipalName user@contoso.com -DestinationDataLocation EUR -ValidationOnly
```

Validates whether the user and OneDrive content can be moved to the `EUR` data location without starting the move.

### EXAMPLE 4

```powershell
Start-PnPUserAndContentMove -UserPrincipalName user@contoso.com -DestinationDataLocation EUR -Verbose
```

Starts a move job and includes additional diagnostic properties in the returned object.

## PARAMETERS

### -UserPrincipalName
The user principal name of the user whose user profile and OneDrive content should be moved.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationDataLocation
The destination SharePoint Online multi-geo data location code, such as `NAM` or `EUR`.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreferredMoveBeginDate
The preferred date and time at which the move should begin. The value is converted to UTC before it is sent to SharePoint Online.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreferredMoveEndDate
The preferred date and time at which the move should end. The value is converted to UTC before it is sent to SharePoint Online.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Notify
Specifies notification information to send with the move job request.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Reserved
Reserved for future use.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidationOnly
Validates whether the user and OneDrive content can be moved without starting the move.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 7
Default value: None
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
Returns an object with `UserPrincipalName`, `MoveJobId`, `SourceDataLocation`, `DestinationDataLocation`, `TimeStamp`, and `MoveState` properties. Validation-only move jobs return `ValidationState` instead of `TimeStamp` and `MoveState`. When `-Verbose` is specified, additional move job details are returned.

## RELATED LINKS

[Get-PnPUserAndContentMoveState](Get-PnPUserAndContentMoveState.md)

[Stop-PnPUserAndContentMove](Stop-PnPUserAndContentMove.md)

[Get-PnPMultiGeoCompanyAllowedDataLocation](Get-PnPMultiGeoCompanyAllowedDataLocation.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

