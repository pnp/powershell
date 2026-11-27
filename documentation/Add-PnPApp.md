---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPApp.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPApp
---
  
# Add-PnPApp

## SYNOPSIS

**Required Permissions**

* SharePoint: depends on `-Scope` and the authentication mode. App-only uploads and replacements in the tenant app catalog require `SharePointTenantSettings.ReadWrite.All` on **Office 365 SharePoint Online** for the tenant App Catalog write check, in addition to the other permissions required by the workflow. This additional requirement does not apply to delegated authentication or `-Scope Site`. See [Microsoft's tenant App Catalog app-only permission requirements](https://learn.microsoft.com/en-us/sharepoint/dev/spfx/tenant-app-catalog-app-only-permissions).

Add/uploads an available app to the app catalog

## SYNTAX

```powershell
Add-PnPApp [-Path] <String> [-Scope <AppCatalogScope>] [-Overwrite] [-Timeout <Int32>] [-Publish [-SkipFeatureDeployment]] [-Connection <PnPConnection>] [-Force <SwitchParameter>]
```

## DESCRIPTION

Allows to upload an app to the app catalog at tenant or site collection level. By specifying `-Publish` option it is possible to deploy/trust it at the same time.

### App-only tenant app catalog uploads

Microsoft's planned cutoff for using `Sites.ReadWrite.All` alone for affected tenant app catalog writes is **January 31, 2027**. This affects both new uploads and replacements using `-Overwrite`, including packages that do not register a tenant-wide extension.

Before the cutoff, add the **application** permission `SharePointTenantSettings.ReadWrite.All` under **Office 365 SharePoint Online** on the application registration, grant tenant administrator consent, reconnect to acquire a new SharePoint access token, and retest the upload. The permission with the same name under **Microsoft Graph** does not satisfy this SharePoint check. Retain the other site or tenant permissions required by your workflow.

Delegated authentication and site collection app catalogs (`-Scope Site`) are not affected by this additional requirement. It does not apply to read-only or delete operations, or to the runtime use of already deployed solutions.

If an app-only tenant upload is denied, verify the SharePoint resource, administrator consent, the new token's `roles` claim, and the existing workflow permissions. See [Configure app-only permissions for tenant App Catalog operations](https://learn.microsoft.com/en-us/sharepoint/dev/spfx/tenant-app-catalog-app-only-permissions).

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPApp -Path ./myapp.sppkg
```

This will upload the specified app package to the tenant app catalog

### EXAMPLE 2
```powershell
Add-PnPApp -Path ./myapp.sppkg -Publish
```

This will upload the specified app package to the tenant app catalog and deploy/trust it at the same time.

### EXAMPLE 3
```powershell
Add-PnPApp -Path ./myapp.sppkg -Scope Site -Publish
```

This will upload the specified app package to the site collection app catalog and deploy/trust it at the same time.

### EXAMPLE 4
```powershell
Add-PnPApp -Path ./myapp.sppkg -Publish -SkipFeatureDeployment
```

This will upload the specified app package to the tenant app catalog, deploy/trust it and make it globally available on all site collections.

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

### -Overwrite
When provided, it will overwrite the existing app package if it already exists

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path to the app package to deploy to the App Catalog

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Publish
This will deploy/trust an app into the App Catalog

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Defines which app catalog to use: the site collection scoped App Catalog or the tenant wide App Catalog. Defaults to Tenant.

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
When provided, the solution will be globally deployed, meaning one does not have to go into every site to add it as an app to have its components available. Instead they will be available rightaway.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timeout
Specifies the timeout in seconds. Defaults to 200.

```yaml
Type: Int32
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

[Configure app-only permissions for tenant App Catalog operations](https://learn.microsoft.com/en-us/sharepoint/dev/spfx/tenant-app-catalog-app-only-permissions)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
