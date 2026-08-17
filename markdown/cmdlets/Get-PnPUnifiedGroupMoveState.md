---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUnifiedGroupMoveState.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPUnifiedGroupMoveState
Module Name: PnP.PowerShell
---
 
# Get-PnPUnifiedGroupMoveState

## SYNOPSIS
Returns the state of a SharePoint Online Microsoft 365 group move job.

## SYNTAX

```powershell
Get-PnPUnifiedGroupMoveState [-GroupAlias] <String> [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns status information for a SharePoint Online multi-geo Microsoft 365 group move job by group alias.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPUnifiedGroupMoveState -GroupAlias "contoso-marketing"
```

Returns the move state for the specified Microsoft 365 group.

### EXAMPLE 2

```powershell
Get-PnPUnifiedGroupMoveState -GroupAlias "contoso-marketing" -Verbose
```

Returns the move state for the specified Microsoft 365 group and includes additional diagnostic properties.

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

### -GroupAlias
The alias of the Microsoft 365 group whose move state should be retrieved.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.Management.Automation.PSObject
Returns an object with `GroupName`, `MoveJobId`, `SourceDataLocation`, `DestinationDataLocation`, `TimeStamp`, and `MoveState` properties. Validation-only move jobs return `ValidationState` instead of `TimeStamp` and `MoveState`. When `-Verbose` is specified, additional move job details are returned.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

