---
Module Name: PnP.PowerShell
title: Get-PnPSiteGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteGroup.html
---
 
# Get-PnPSiteGroup

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets all the groups in the current or specified site collection.

## SYNTAX

```powershell
Get-PnPSiteGroup
   [-Group <String>]
   [-Site <SitePipeBind>]
```

## DESCRIPTION
Use the Get-PnPSiteGroup cmdlet to get all the groups on the specified or currently connected site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteGroup
```

Returns all SharePoint groups in the current connected to site

### EXAMPLE 2
```powershell
Get-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/siteA"
```
This will return all SharePoint groups in the specified site

### EXAMPLE 3
```powershell
Get-PnPSiteGroup -Group "SiteA Members"
```
This will return the specified group for the current connected to site

### EXAMPLE 4
```powershell
Get-PnPSiteGroup -Group "SiteA Members" -Site "https://contoso.sharepoint.com/sites/siteA"
```
This will return the specified group for the specified site.

## PARAMETERS

### -Site
Retrieve the associated member group.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
Specifies the group name.

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

