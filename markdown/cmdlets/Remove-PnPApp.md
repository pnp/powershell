---
Module Name: PnP.PowerShell
title: Remove-PnPApp
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPApp.html
---
 
# Remove-PnPApp

## SYNOPSIS
Removes an app from the app catalog.

## SYNTAX

```powershell
Remove-PnPApp [-Identity] <AppMetadataPipeBind> [-Scope <AppCatalogScope>] [-Connection <PnPConnection>] [-Force <SwitchParameter>]
```

## DESCRIPTION

Allows to remove an app from the app catalog.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will remove the specified app from the tenant app catalog.

### EXAMPLE 2
```powershell
Remove-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Site
```

This will remove the specified app from the site collection app catalog.

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
Specifies the Id of the Add-in Instance.

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
Defines which app catalog to use. Defaults to Tenant.

```yaml
Type: AppCatalogScope
Parameter Sets: (All)
Accepted values: Tenant, Site

Required: False
Position: Named
Default value: Tenant
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

