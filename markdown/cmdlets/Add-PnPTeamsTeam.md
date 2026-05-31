---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsTeam.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTeamsTeam
---
  
# Add-PnPTeamsTeam

## SYNOPSIS
Adds a Teams team to an existing, group connected, site collection

## SYNTAX

```powershell
Add-PnPTeamsTeam [-Connection <PnPConnection>] 
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


