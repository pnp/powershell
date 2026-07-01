---
Module Name: PnP.PowerShell
title: Get-PnPTenantTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantTemplate.html
---
 
# Get-PnPTenantTemplate

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Generates a provisioning tenant template from a site. If the site is a hubsite any connected site will be included.

## SYNTAX

### Extract a template to a file
```powershell
Get-PnPTenantTemplate [-SiteUrl <String>] [-Out <String>] [-Force]
 [-Configuration <ExtractConfigurationPipeBind>] [-Connection <PnPConnection>]  
 
```

### Extract a template as an object
```powershell
Get-PnPTenantTemplate [-SiteUrl <String>] [-AsInstance] [-Configuration <ExtractConfigurationPipeBind>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to generate a provisioning tenant template from a site. If the site is a hubsite any connected site will be included.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantTemplate -Out tenanttemplate.xml
```

Extracts a tenant template of the currently connected to site.

### EXAMPLE 2
```powershell
Get-PnPTenantTemplate -Out tenanttemplate.xml -SiteUrl https://m365x123456.sharepoint.com/sites/HomeSite
```

Extracts a tenant template for the site https://m365x123456.sharepoint.com/sites/HomeSite and places the schema XML into the file "tenanttemplate.xml".

### EXAMPLE 3
```powershell
Get-PnPTenantTemplate -Out tenanttemplate.xml -SiteUrl https://m365x123456.sharepoint.com/sites/HomeSite -Force
```

Extracts a tenant template for the site https://m365x123456.sharepoint.com/sites/HomeSite and places the schema XML into the file "tenanttemplate.xml". The xml file will be overwritten if it already exists.

## PARAMETERS

### -AsInstance
Returns the template as an in-memory object, which is an instance of the ProvisioningHierarchy type of the PnP Core Component. It cannot be used together with the -Out parameter.

```yaml
Type: SwitchParameter
Parameter Sets: Extract a template as an object

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Configuration
Specify a JSON configuration file to configure the extraction progress.

```yaml
Type: ExtractConfigurationPipeBind
Parameter Sets: (All)

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

### -Force
Overwrites the output file if it exists.

```yaml
Type: SwitchParameter
Parameter Sets: Extract a template to a file

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Out
Filename to write to, optionally including full path

```yaml
Type: String
Parameter Sets: Extract a template to a file

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteUrl
The URL of the site collection to create a tenant template out of. If omitted, the currently connected to site will be used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

