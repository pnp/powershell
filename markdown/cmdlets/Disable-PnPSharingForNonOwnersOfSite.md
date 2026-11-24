---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disable-PnPSharingForNonOwnersOfSite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disable-PnPSharingForNonOwnersOfSite
---
  
# Disable-PnPSharingForNonOwnersOfSite

## SYNOPSIS
Configures the site to only allow sharing of the site and items in the site by owners

## SYNTAX

```powershell
Disable-PnPSharingForNonOwnersOfSite [-Identity <SitePipeBind>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Configures the site to only allow sharing of the site and items in the site by owners. At this point there is no interface available yet to undo this action through script. You will have to do so through the user interface of SharePoint.

## EXAMPLES

### EXAMPLE 1
```powershell
Disable-PnPSharingForNonOwnersOfSite
```

Restricts sharing of the site and items in the site only to owners

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


