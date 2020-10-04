---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/disable-pnpsharingfornonownersofsite
schema: 2.0.0
title: Disable-PnPSharingForNonOwnersOfSite
---

# Disable-PnPSharingForNonOwnersOfSite

## SYNOPSIS
Configures the site to only allow sharing of the site and items in the site by owners

## SYNTAX

```
Disable-PnPSharingForNonOwnersOfSite [-Identity <SitePipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
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
Aliases:

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)