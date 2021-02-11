---
Module Name: PnP.PowerShell
title: Get-PnPTenantSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantSite.html
---
 
# Get-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieve site information.

## SYNTAX

### By Site
```powershell
Get-PnPTenantSite [-Identity] <string> [-Detailed] [-DisableSharingForNonOwnersStatus] [-Connection <PnPConnection>]
    [<CommonParameters>]
```

### All Sites
```powershell
Get-PnPTenantSite [-Template <string>] [-Detailed] [-IncludeOneDriveSites] [-GroupIdDefined <Boolean>] [-Filter <string>] [-Connection
    <PnPConnection>] [<CommonParameters>]
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
Get-PnPTenantSite -Identity "http://tenant.sharepoint.com/sites/projects"
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

### EXAMPLE 8
```powershell
Get-PnPTenantSite -GroupIdDefined $true
```

Returns all sites which have an underlying Microsoft 365 Group

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

### -Detailed
By default, not all returned attributes are populated. This switch populates all attributes. It can take several seconds to run. Without this, some attributes will show default values that may not be correct.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableSharingForNonOwnersStatus
This parameter will include the status for non owners sharing on the returned object. By default the value for this property is null.

```yaml
Type: SwitchParameter
Parameter Sets: By Site

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter
Specifies the script block of the server-side filter to apply. See https://technet.microsoft.com/en-us/library/fp161380.aspx

```yaml
Type: String
Parameter Sets: All sites

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GroupIdDefined
If specified allows you to filter on sites that have an underlying Microsoft 365 group defined.

```yaml
Type: Boolean
Parameter Sets: All sites

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeOneDriveSites
By default, the OneDrives are not returned. This switch includes all OneDrives.

```yaml
Type: SwitchParameter
Parameter Sets: All Sites

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
By default, all sites will be returned. Specify a template value alike "STS#0" here to filter on the template

```yaml
Type: String
Parameter Sets: All Sites

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The URL of the site

```yaml
Type: String
Parameter Sets: By Site
Aliases: Url

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

