---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpsitegroup
schema: 2.0.0
title: Get-PnPSiteGroup
---

# Get-PnPSiteGroup

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets all the groups in the current or specified site collection.

## SYNTAX

### All (Default)
```powershell
Get-PnPGroup [-Web <WebPipeBind>] [-Connection <PnPConnection>] [-Includes <String[]>] [<CommonParameters>]
```

### ByName
```powershell
Get-PnPGroup [[-Identity] <GroupPipeBind>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [-Includes <String[]>] [<CommonParameters>]
```

### Members
```powershell
Get-PnPGroup [-AssociatedMemberGroup] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [-Includes <String[]>]
 [<CommonParameters>]
```

### Visitors
```powershell
Get-PnPGroup [-AssociatedVisitorGroup] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [-Includes <String[]>] [<CommonParameters>]
```

### Owners
```powershell
Get-PnPGroup [-AssociatedOwnerGroup] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [-Includes <String[]>]
 [<CommonParameters>]
```

## DESCRIPTION
Use the Get-PnPSiteGroup cmdlet to get all the groups on the specified or currently connected to site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteGroup
```

Returns all SharePoint groups in the current connected to site

### EXAMPLE 2
```powershell
Get-PnPSiteGroup -Site https://contoso.sharepoint.com/sites/siteA
```
This will return all SharePoint groups in the specified site

### EXAMPLE 3
```powershell
Get-PnPSiteGroup -Group "SiteA Members"
```
This will return the specified group for the current connected to site

### EXAMPLE 3
```powershell
Get-PnPSiteGroup -Group "SiteA Members" -Site https://contoso.sharepoint.com/sites/siteA
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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)