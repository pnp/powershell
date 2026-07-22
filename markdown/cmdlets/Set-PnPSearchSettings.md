---
Module Name: PnP.PowerShell
title: Set-PnPSearchSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchSettings.html
---
 
# Set-PnPSearchSettings

## SYNOPSIS
Sets search settings for a site.

## SYNTAX

```powershell
Set-PnPSearchSettings [-SearchBoxInNavBar <SearchBoxInNavBarType>] [-SearchPageUrl <String>]
 [-SearchBoxPlaceholderText <String>] [-SearchScope <SearchScopeType>] [-Scope <SearchSettingsScope>] [-Force]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to modify search settings for a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSearchSettings -SearchBoxInNavBar Hidden -Scope Site
```

This example hides the suite bar search box on all pages and sites in the site collection.

### EXAMPLE 2
```powershell
Set-PnPSearchSettings -SearchBoxInNavBar Hidden -Scope Web
```

Example 2 hides the suite bar search box on all pages in the current site.

### EXAMPLE 3
```powershell
Set-PnPSearchSettings -SearchPageUrl "https://contoso.sharepoint.com/sites/mysearch/SitePages/search.aspx"
```

Redirects the suite bar search box in the site to a custom URL

### EXAMPLE 4
```powershell
Set-PnPSearchSettings -SearchPageUrl ""
```

This example clears the suite bar search box redirect URL and reverts to the default behavior.

### EXAMPLE 5
```powershell
Set-PnPSearchSettings -SearchPageUrl "https://contoso.sharepoint.com/sites/mysearch/SitePages/search.aspx" -Scope Site
```

Redirects classic search to a custom URL.

### EXAMPLE 6
```powershell
Set-PnPSearchSettings -SearchScope Tenant
```

Example 6 sets default behavior of the suite bar search box to show tenant wide results instead of site or hub scoped results.

### EXAMPLE 7
```powershell
Set-PnPSearchSettings -SearchScope Hub
```

Sets default behavior of the suite bar search box to show hub results instead of site results on an associated hub site.

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

### -Force
Do not ask for confirmation.

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
Scope to apply the setting to. Possible values: Web (default), Site. For a root site, the scope does not matter.

```yaml
Type: SearchSettingsScope
Parameter Sets: (All)
Accepted values: Site, Web

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SearchBoxInNavBar
Set the scope of which the suite bar search box shows. Possible values: Inherit, AllPages, ModernOnly, Hidden.

```yaml
Type: SearchBoxInNavBarType
Parameter Sets: (All)
Accepted values: Inherit, AllPages, ModernOnly, Hidden

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SearchBoxPlaceholderText
Set the placeholder text displayed in the search box.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SearchPageUrl
Set the URL where the search box should redirect to.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SearchScope
Set the search scope of the suite bar search box. Possible values: DefaultScope, Tenant, Hub, Site.

```yaml
Type: SearchScopeType
Parameter Sets: (All)
Accepted values: DefaultScope, Tenant, Hub, Site

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

