---
Module Name: PnP.PowerShell
title: Get-PnPSiteScriptFromWeb
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteScriptFromWeb.html
---
 
# Get-PnPSiteScriptFromWeb

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Generates a Site Script from an existing site

## SYNTAX

### All components
```powershell
Get-PnPSiteScriptFromWeb -Url <String> [-Lists <String[]>] [-IncludeAll] [-Connection <PnPConnection>]
   [<CommonParameters>]
```

### Specific components
```powershell
Get-PnPSiteScriptFromWeb [-Url <String>] [-Lists <String[]>] [-IncludeBranding] [-IncludeLinksToExportedItems]
 [-IncludeRegionalSettings] [-IncludeSiteExternalSharingCapability] [-IncludeTheme]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This command allows a Site Script to be generated off of an existing site on your tenant. You need to provide at least one of the optional Include or Lists arguments. If you omit the URL, the Site Script will be created from the site to which you are connected.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteScriptFromWeb -Url "https://contoso.sharepoint.com/sites/teamsite" -IncludeAll
```

Returns the generated Site Script JSON containing all supported components from the site at the provided Url

### EXAMPLE 2
```powershell
Get-PnPSiteScriptFromWeb -Url "https://contoso.sharepoint.com/sites/teamsite" -IncludeAll -Lists "Shared Documents","Lists\MyList"
```

Returns the generated Site Script JSON containing all supported components from the site at the provided Url including the lists "Shared Documents" and "MyList"

### EXAMPLE 3
```powershell
Get-PnPSiteScriptFromWeb -Url "https://contoso.sharepoint.com/sites/teamsite" -IncludeBranding -IncludeLinksToExportedItems
```

Returns the generated Site Script JSON containing the branding and navigation links from the site at the provided Url

### EXAMPLE 4
```powershell
Get-PnPSiteScriptFromWeb -IncludeAll
```

Returns the generated Site Script JSON containing all the components from the currently connected to site

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

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
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

