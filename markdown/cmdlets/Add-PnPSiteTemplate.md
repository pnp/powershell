---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteTemplate.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPSiteTemplate
---
  
# Add-PnPSiteTemplate

## SYNOPSIS
Adds a PnP Site Template object to a tenant template

## SYNTAX

```powershell
Add-PnPSiteTemplate -SiteTemplate <SiteTemplate> -TenantTemplate <ProvisioningHierarchy> 
```

## DESCRIPTION

Allows to add PnP Site Template object to a tenant template.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteTemplate -TenantTemplate $tenanttemplate -SiteTemplate $sitetemplate
```

Adds an existing site template to an existing tenant template object

## PARAMETERS

### -SiteTemplate
The template to add to the tenant template

```yaml
Type: SiteTemplate
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TenantTemplate
The tenant template to add the template to

```yaml
Type: ProvisioningHierarchy
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)