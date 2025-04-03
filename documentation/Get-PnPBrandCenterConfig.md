---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPBrandCenterConfig.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPBrandCenterConfig
---
  
# Get-PnPBrandCenterConfig

## SYNOPSIS
Returns the Brand Center configuration for the current tenant

## SYNTAX

```powershell
Get-PnPBrandCenterConfig
```

## DESCRIPTION
Allows retrieval of the Brand Center configuration for the current tenant. The Brand Center is a centralized location for managing and sharing branding assets, such as logos, colors, and fonts, across Microsoft 365 applications.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPBrandCenterConfig
```

Returns the configuration of the Brand Center for the current tenant.

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