---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnptenantsite
schema: 2.0.0
title: Get-PnPTenantSite
---

# Get-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieve site information.

## SYNTAX

```powershell
Get-PnPTenantSite [[-Url] <String>] [-Template <String>] [-Detailed] [-IncludeOneDriveSites] [-Filter <String>]
 [-Connection <PnPConnection>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Use this cmdlet to retrieve site information from your tenant administration.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantSite
```

Returns all site collections

### EXAMPLE 2
```powershell
Get-PnPTenantSite -Url http://tenant.sharepoint.com/sites/projects
```

Returns information about the project site

### EXAMPLE 3
```powershell
Get-PnPTenantSite -Detailed
```

Returns all sites with the full details of these sites

### EXAMPLE 4
```powershell
Get-PnPTenantSite -IncludeOneDriveSites
```

Returns all sites including all OneDrive for Business sites

### EXAMPLE 5
```powershell
Get-PnPTenantSite -IncludeOneDriveSites -Filter "Url -like '-my.sharepoint.com/personal/'"
```

Returns all OneDrive for Business sites

### EXAMPLE 6
```powershell
Get-PnPTenantSite -Template SITEPAGEPUBLISHING#0
```

Returns all Communication sites

### EXAMPLE 7
```powershell
Get-PnPTenantSite -Filter "Url -like 'sales'"
```

Returns all sites including 'sales' in the url

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

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Detailed
By default, not all returned attributes are populated. This switch populates all attributes. It can take several seconds to run. Without this, some attributes will show default values that may not be correct.

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter
Specifies the script block of the server-side filter to apply. See https://technet.microsoft.com/en-us/library/fp161380.aspx

Only applicable to: SharePoint Online

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeOneDriveSites
By default, the OneDrives are not returned. This switch includes all OneDrives.

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
By default, all sites will be returned. Specify a template value alike "STS#0" here to filter on the template

Only applicable to: SharePoint Online

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The URL of the site

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

```yaml
Type: String
Parameter Sets: (All)
Aliases: Identity

Required: False
Position: 0
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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)