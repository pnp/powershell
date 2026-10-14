---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPBrandCenterFont.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPBrandCenterFont
---
  
# Get-PnPBrandCenterFont

## SYNOPSIS
Returns the available fonts uploaded to the Brand Center

## SYNTAX

```powershell
Get-PnPBrandCenterFont [-Connection <PnPConnection>]
```

## DESCRIPTION
Allows retrieval of the available fonts from the Brand Center.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPBrandCenterFont
```

Returns all the available fonts

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

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
