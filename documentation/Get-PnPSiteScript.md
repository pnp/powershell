---
Module Name: PnP.PowerShell
title: Get-PnPSiteScript
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteScript.html
---
 
# Get-PnPSiteScript

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieve Site Scripts that have been registered on the current tenant.

## SYNTAX

```powershell
Get-PnPSiteScript [[-Identity] <TenantSiteScriptPipeBind>] [[-SiteDesign] <TenantSiteDesignPipeBind>]
 [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to retrieve site scripts registered on the current tenant. By using `Identity` option it is possible to retrieve specified site script

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteScript
```

Returns all registered site scripts

### EXAMPLE 2
```powershell
Get-PnPSiteScript -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Returns a specific registered site script

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
If specified will retrieve the specified site script

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteDesign
If specified will retrieve the site scripts for this design

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)