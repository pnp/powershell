---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPApp.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPApp
---
  
# Get-PnPApp

## SYNOPSIS
Returns the available apps from the app catalog

## SYNTAX

```powershell
Get-PnPApp [[-Identity] <AppMetadataPipeBind>] [-Scope <AppCatalogScope>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to retrieve available apps from the app catalog. In order to get apps from site collection scoped app catalog set `Scope` option to `Site`.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPApp
```

This will return all available apps from the tenant app catalog. It will list the installed version in the current site.

### EXAMPLE 2
```powershell
Get-PnPApp -Scope Site
```

This will return all available apps from the site collection scoped app catalog. It will list the installed version in the current site.

### EXAMPLE 3
```powershell
Get-PnPApp -Identity 2646ccc3-6a2b-46ef-9273-81411cbbb60f
```

This will retrieve the specific app from the app catalog.

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
Specifies the Id of an app which is available in the app catalog

```yaml
Type: AppMetadataPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scope
Defines which app catalog to use. Defaults to Tenant

```yaml
Type: AppCatalogScope
Parameter Sets: (All)
Accepted values: Tenant, Site

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


