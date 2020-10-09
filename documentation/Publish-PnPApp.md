---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/publish-pnpapp
schema: 2.0.0
title: Publish-PnPApp
---

# Publish-PnPApp

## SYNOPSIS
Publishes/Deploys/Trusts an available app in the app catalog

## SYNTAX

```powershell
Publish-PnPApp [-Identity] <AppMetadataPipeBind> [-SkipFeatureDeployment] [-Scope <AppCatalogScope>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Publish-PnPApp -Identity 2646ccc3-6a2b-46ef-9273-81411cbbb60f
```

This will deploy/trust an app into the app catalog. Notice that the app needs to be available in the tenant scoped app catalog

### EXAMPLE 2
```powershell
Publish-PnPApp -Identity 2646ccc3-6a2b-46ef-9273-81411cbbb60f -Scope Site
```

This will deploy/trust an app into the app catalog. Notice that the app needs to be available in the site collection scoped app catalog

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
Specifies the Id of the app

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

### -SkipFeatureDeployment

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)