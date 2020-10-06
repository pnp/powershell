---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/update-pnpapp
schema: 2.0.0
title: Update-PnPApp
---

# Update-PnPApp

## SYNOPSIS
Updates an available app from the app catalog

## SYNTAX

```
Update-PnPApp [-Identity] <AppMetadataPipeBind> [-Scope <AppCatalogScope>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Update-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will update an already installed app if a new version is available in the tenant app catalog. Retrieve a list all available apps and the installed and available versions with Get-PnPApp

### EXAMPLE 2
```powershell
Update-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Site
```

This will update an already installed app if a new version is available in the site collection app catalog. Retrieve a list all available apps and the installed and available versions with Get-PnPApp -Scope Site

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

Only applicable to: SharePoint Online, SharePoint Server 2019

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
Specifies the Id or an actual app metadata instance

Only applicable to: SharePoint Online, SharePoint Server 2019

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

Only applicable to: SharePoint Online, SharePoint Server 2019

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