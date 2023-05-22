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

Retrieves site collection information

## SYNTAX

### By Site
```powershell
Get-PnPTenantSite [-Identity] <string> [-Detailed] [-DisableSharingForNonOwnersStatus] [-Connection <PnPConnection>]
    
```

### All Sites
```powershell
Get-PnPTenantSite [-Template <string>] [-Detailed] [-IncludeOneDriveSites] [-GroupIdDefined <Boolean>] [-Filter <string>] [-Connection
    <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows for retrieval of site collection information through the SharePoint Online tenant site. It requires having SharePoint Online administrator access.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantSite
```

Returns all site collections except the OneDrive for Business sites with basic information on them

### EXAMPLE 2
```powershell
Get-PnPTenantSite -Detailed
```

Returns all site collections except the OneDrive for Business sites with the full details on them

### EXAMPLE 3
```powershell
Get-PnPTenantSite -IncludeOneDriveSites
```

Returns all site collections including all OneDrive for Business sites

### EXAMPLE 4
```powershell
Get-PnPTenantSite -IncludeOneDriveSites -Filter "Url -like '-my.sharepoint.com/personal/'"
```

Returns only OneDrive for Business site collections

### EXAMPLE 5
```powershell
Get-PnPTenantSite -Identity "http://tenant.sharepoint.com/sites/projects"
```

Returns information of the site collection with the provided url

### EXAMPLE 6
```powershell
Get-PnPTenantSite -Identity 7e8a6f56-92fe-4b22-9364-41799e579e8a
```

Returns information of the site collection with the provided Id

### EXAMPLE 7
```powershell
Get-PnPTenantSite -Template SITEPAGEPUBLISHING#0
```

Returns all Communication site collections

### EXAMPLE 8
```powershell
Get-PnPTenantSite -Filter "Url -like 'sales'"
```

Returns all site collections having 'sales' in their url

### EXAMPLE 9
```powershell
Get-PnPTenantSite -GroupIdDefined $true
```

Returns all site collections which have an underlying Microsoft 365 Group

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
Specifies the script block of the server-side filter to apply. See https://learn.microsoft.com/powershell/module/sharepoint-online/get-sposite?view=sharepoint-ps#:~:text=SharePoint%20Online-,%2DFilter,-Specifies%20the%20script.

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
The URL or Id of the site collection. Requesting a site collection by its Id only works for modern SharePoint Online site collections.

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

