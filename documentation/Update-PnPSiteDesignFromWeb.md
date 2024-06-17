---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Update-PnPSiteDesignFromWeb.html
external help file: PnP.PowerShell.dll-Help.xml
title: Update-PnPSiteDesignFromWeb
---
  
# Update-PnPSiteDesignFromWeb

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates an existing Site Design on the current tenant based on the site provided through -Url or the currently connected to site if -Url is omitted.

## SYNTAX

### Specific components
```powershell
Update-PnPSiteDesignFromWeb -Identity <TenantSiteDesignPipeBind> -Url <String> [-Lists <String[]>] [-IncludeBranding <SwitchParameter>] [-IncludeLinksToExportedItems <SwitchParameter>]
 [-IncludeRegionalSettings <SwitchParameter>] [-IncludeSiteExternalSharingCapability <SwitchParameter>] [-IncludeTheme <SwitchParameter>] [-Connection <PnPConnection>]
```

### All components
```powershell
Update-PnPSiteDesignFromWeb -Identity <TenantSiteDesignPipeBind> -Url <String> [-Lists <String[]>] [-IncludeAll <SwitchParameter>]
 [-Connection <PnPConnection>]
```

## DESCRIPTION

Updates an existing Site Design on the current tenant based on the site provided through -Url or the currently connected to site if -Url is omitted. It combines the steps of `Get-PnPSiteScriptFromWeb` and `Set-PnPSiteScript` to generate a site script from a web and update an existing site script with it into one cmdlet. The information returned from running the cmdlet is the information of the Site Design that has been updated.

## EXAMPLES

### EXAMPLE 1
```powershell
Update-PnPSiteDesignFromWeb -Identity "Contoso Project" -IncludeAll
```

Generates a site script based on all the components of the currently connected to site, excluding its lists and libraries and based on the generated script it will update the site script in the site design with the provided name.

### EXAMPLE 2
```powershell
Update-PnPSiteDesignFromWeb -Identity "Contoso Project" -IncludeAll -Lists ("/lists/Issue list", "Shared Documents)
```

Generates a site script based on all the components of the currently connected to site, including the list "Issue list" and the default document library "Shared Documents" and based on the generated script it will update the site script in the site design with the provided name.

### EXAMPLE 3
```powershell
Update-PnPSiteDesignFromWeb -Url https://contoso.sharepoint.com/sites/template -Identity "Contoso Project" -Lists "/lists/Issue list"
```

Generates a site script based on the list "Issue list" in the site provided through Url. Based on the generated script it will update the site script in the site design with the provided name.

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

### -IncludeAll
If specified will include all supported components into the Site Script except for the lists and document libraries, these need to be explicitly be specified through -Lists

```yaml
Type: SwitchParameter
Parameter Sets: All components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeBranding
If specified will include the branding of the site into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeLinksToExportedItems
If specified will include navigation links into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeRegionalSettings
If specified will include the regional settings into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSiteExternalSharingCapability
If specified will include the external sharing configuration into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeTheme
If specified will include the branding of the site into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lists
Allows specifying one or more site relative URLs of lists that should be included into the Site Script, i.e. "Shared Documents","List\MyList"

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
Specifies the URL of the site to generate a Site Script from. If omitted, the currently connected to site will be used.

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
