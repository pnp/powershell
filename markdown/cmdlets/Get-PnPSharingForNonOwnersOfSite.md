---
Module Name: PnP.PowerShell
title: Get-PnPSharingForNonOwnersOfSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSharingForNonOwnersOfSite.html
---
 
# Get-PnPSharingForNonOwnersOfSite

## SYNOPSIS
Returns $false if sharing of the site and items in the site is restricted only to owners or $true if members and owners are allowed to share

## SYNTAX

```powershell
Get-PnPSharingForNonOwnersOfSite [-Identity <SitePipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns $false if sharing of the site and items in the site is restricted only to owners or $true if members and owners are allowed to share. You can disable sharing by non owners by using Disable-PnPSharingForNonOwnersOfSite. At this point there is no interface available yet to enable sharing by owners and members again through script. You will have to do so through the user interface of SharePoint.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSharingForNonOwnersOfSite
```

Returns $false if sharing of the site and items in the site is restricted only to owners or $true if members and owners are allowed to share

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

```yaml
Type: SitePipeBind
Parameter Sets: (All)
Aliases: Url

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

