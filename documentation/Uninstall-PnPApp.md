---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/uninstall-pnpapp
schema: 2.0.0
title: Uninstall-PnPApp
---

# Uninstall-PnPApp

## SYNOPSIS
Uninstalls an available add-in from the site

## SYNTAX

```
Uninstall-PnPApp [-Identity] <AppMetadataPipeBind> [-Scope <AppCatalogScope>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Uninstall-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will uninstall the specified app from the current site.

### EXAMPLE 2
```powershell
Uninstall-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Site
```

This will uninstall the specified app from the current site. Notice that the app was original installed from the site collection appcatalog.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

Only applicable to: SharePoint Online

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
Specifies the Id of the Addin Instance

Only applicable to: SharePoint Online

```yaml
Type: AppMetadataPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scope
Defines which app catalog to use. Defaults to Tenant

Only applicable to: SharePoint Online

```yaml
Type: AppCatalogScope
Parameter Sets: (All)
Aliases:
Accepted values: Tenant, Site

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)