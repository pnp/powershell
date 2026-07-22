---
Module Name: PnP.PowerShell
title: Publish-PnPApp
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Publish-PnPApp.html
---
 
# Publish-PnPApp

## SYNOPSIS
Publishes/Deploys/Trusts an available app in the app catalog

## SYNTAX

```powershell
Publish-PnPApp [-Identity] <AppMetadataPipeBind> [-SkipFeatureDeployment] [-Scope <AppCatalogScope>] [-Connection <PnPConnection>] [-Force <SwitchParameter>]
```

## DESCRIPTION

Allows to deploy/trust an available app in the app catalog.

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
Specifies the Id of the app

```yaml
Type: AppMetadataPipeBind
Parameter Sets: (All)

Required: True
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

### -SkipFeatureDeployment

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
If provided, no confirmation will be asked to change no-script setting.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

