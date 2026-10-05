---
Module Name: PnP.PowerShell
title: Get-PnPTenantAppCatalogUrl
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantAppCatalogUrl.html
---
 
# Get-PnPTenantAppCatalogUrl

## SYNOPSIS
Retrieves the url of the tenant scoped app catalog

## SYNTAX

```powershell
Get-PnPTenantAppCatalogUrl [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to retrieve the url of the tenant scoped app catalog.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantAppCatalogUrl
```

Returns the url of the tenant scoped app catalog site collection

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

