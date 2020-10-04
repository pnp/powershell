---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpteamsteam
schema: 2.0.0
title: Add-PnPTeamsTeam
---

# Add-PnPTeamsTeam

## SYNOPSIS
Adds a Teams team to an existing, group connected, site collection

## SYNTAX

```
Add-PnPTeamsTeam [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This command allows you to add a Teams team to an existing, Microsoft 365 group connected, site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsTeam
```

This create a teams team for the connected site collection

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)