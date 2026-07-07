---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPUserOneDriveLocation
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUserOneDriveLocation.html
Module Name: PnP.PowerShell
schema: 2.0.0
---
  
# Get-PnPUserOneDriveLocation

## SYNOPSIS
Returns the SharePoint Online multi-geo location details for a user's OneDrive personal site.

## SYNTAX

```powershell
Get-PnPUserOneDriveLocation -UserPrincipalName <String> [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns the SharePoint Online multi-geo location, OneDrive personal site URL, site ID, and user principal name for the specified user.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPUserOneDriveLocation -UserPrincipalName user@contoso.com
```

Returns the OneDrive personal site location details for the specified user.

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

### -UserPrincipalName
The user principal name of the user whose OneDrive personal site location details should be returned.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### PnP.PowerShell.Commands.Model.UserPersonalSiteLocation
Returns an object with `UserPrincipalName`, `Location`, `MySiteUrl`, and `SiteId` properties.

## RELATED LINKS

[Get-PnPUserAndContentMoveState](Get-PnPUserAndContentMoveState.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

