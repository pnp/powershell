---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Start-PnPSiteContentMove.html
title: Start-PnPSiteContentMove
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
 
# Start-PnPSiteContentMove

## SYNOPSIS
Starts a SharePoint Online multi-geo site content move job.

## SYNTAX

### UrlAndDestinationDataLocation

```powershell
Start-PnPSiteContentMove [-SourceSiteUrl] <String> [-DestinationDataLocation] <String> [[-PreferredMoveBeginDate] <DateTime>] [[-PreferredMoveEndDate] <DateTime>] [[-Reserved] <String>] [-ValidationOnly] [-Force] [-SuppressMarketplaceAppCheck] [-SuppressWorkflow2013Check] [-SuppressAllWarnings] [-SuppressBcsCheck] [-Connection <PnPConnection>]
```

### UrlAndDestinationUrl

```powershell
Start-PnPSiteContentMove [-SourceSiteUrl] <String> [-DestinationUrl] <String> [[-PreferredMoveBeginDate] <DateTime>] [[-PreferredMoveEndDate] <DateTime>] [[-Reserved] <String>] [-ValidationOnly] [-Force] [-SuppressMarketplaceAppCheck] [-SuppressWorkflow2013Check] [-SuppressAllWarnings] [-SuppressBcsCheck] [-Connection <PnPConnection>]
```

## DESCRIPTION
Starts a SharePoint Online multi-geo move job for a site collection. The move can target a destination data location or a destination site URL.

Use `-ValidationOnly` to validate whether the site content can be moved without starting the move.

## EXAMPLES

### EXAMPLE 1

```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/project" -DestinationDataLocation EUR
```

Starts a move job for the specified site to the `EUR` data location.

### EXAMPLE 2

```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/project" -DestinationUrl "https://contoso.sharepoint.de/sites/project"
```

Starts a move job for the specified site to the destination URL.

### EXAMPLE 3

```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/project" -DestinationDataLocation EUR -PreferredMoveBeginDate "2026-06-20T22:00:00" -PreferredMoveEndDate "2026-06-21T04:00:00"
```

Starts a move job with a preferred move window. The preferred dates are converted to UTC before being sent to SharePoint Online.

### EXAMPLE 4

```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/project" -DestinationDataLocation EUR -ValidationOnly
```

Validates whether the site content can be moved to the `EUR` data location without starting the move.

### EXAMPLE 5

```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/project" -DestinationDataLocation EUR -SuppressMarketplaceAppCheck -SuppressWorkflow2013Check
```

Starts a move job and suppresses marketplace app and SharePoint 2013 workflow checks.

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

### -DestinationDataLocation
The destination SharePoint Online multi-geo data location code, such as `NAM` or `EUR`.

```yaml
Type: String
Parameter Sets: UrlAndDestinationDataLocation

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationUrl
The destination site URL for the site content move job.

```yaml
Type: String
Parameter Sets: UrlAndDestinationUrl

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Suppresses all warnings returned by SharePoint Online for the move job request.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 6
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
Position: 2
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
Position: 3
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
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceSiteUrl
The URL of the source site collection to move.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressAllWarnings
Suppresses all warnings returned by SharePoint Online for the move job request.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 9
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressBcsCheck
Suppresses Business Connectivity Services checks for the move job request.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 10
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressMarketplaceAppCheck
Suppresses marketplace app checks for the move job request.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 7
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressWorkflow2013Check
Suppresses SharePoint 2013 workflow checks for the move job request.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 8
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidationOnly
Validates whether the site content can be moved without starting the move.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.Management.Automation.PSObject
Returns an object with `SourceSiteUrl`, `TargetSiteUrl`, `MoveJobId`, `SourceDataLocation`, `DestinationDataLocation`, and `MoveState` properties. `TimeStamp` is included for non-validation move jobs when supported by the tenant Multi-Geo API version. Validation-only move jobs return `ValidationState` instead of `TimeStamp` and `MoveState`. When `-Verbose` is specified, additional move job details are returned.

## RELATED LINKS

[Get-PnPMultiGeoCompanyAllowedDataLocation](Get-PnPMultiGeoCompanyAllowedDataLocation.md)

[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

