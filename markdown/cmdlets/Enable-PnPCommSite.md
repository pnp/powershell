---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPCommSite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Enable-PnPCommSite
---
  
# Enable-PnPCommSite

## SYNOPSIS
Enables the modern communication site experience on a classic team site.

## SYNTAX

```powershell
Enable-PnPCommSite [-DesignPackageId <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command will enable the modern site experience on a classic team site. The site must be the root site of the site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
Enable-PnPCommSite
```

Enables the modern communication site experience on a classic team site

### EXAMPLE 2
```powershell
Enable-PnPCommSite -DesignPackageId 6142d2a0-63a5-4ba0-aede-d9fefca2c767
```

Enables the modern communication site experience on a classic team site, allowing to specify the design package to be applied

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

### -DesignPackageId
The id (guid) of the design package to apply: 96c933ac-3698-44c7-9f4a-5fd17d71af9e (Topic = default), 6142d2a0-63a5-4ba0-aede-d9fefca2c767 (Showcase) or f6cc5403-0d63-442e-96c0-285923709ffc (Blank).

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


