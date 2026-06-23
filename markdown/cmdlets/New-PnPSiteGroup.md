---
Module Name: PnP.PowerShell
title: New-PnPSiteGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSiteGroup.html
---
 
# New-PnPSiteGroup

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new group in a SharePoint Online site collection.

## SYNTAX

```powershell
New-PnPSiteGroup
   -Name <String>
   -PermissionLevels <String[]>
   [-Site <SitePipeBind>]
```

## DESCRIPTION
A SharePoint group is a set of individual users. SharePoint groups enable you to manage sets of users instead of individual users.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/siteA" -Name "Project Leads" -PermissionLevels "Full Control"
```

This example creates a group named Project Leads with the Full Control permission level on the site collection https://contoso.sharepoint.com/sites/siteA.

### EXAMPLE 2
```powershell
New-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/marketing" -Name "NewGroupName" -PermissionLevels "Design"
```
This example creates a group named NewGroupName with the Design permission level on the site collection https://contoso.sharepoint.com/sites/marketing.

## PARAMETERS

### -Name
Specifies the name of the group to add

```yaml
Type: String
Parameter Sets: (All)
Aliases: Group
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PermissionLevels
Specifies the permission levels to grant to the newly created group. It can be any permission level that exists on the site collection on which the group is being created.

> [!NOTE]
> Permission Levels, are defined on the top-level site of the site collection, please see [How to create and edit permission levels](https://learn.microsoft.com/sharepoint/how-to-create-and-edit-permission-levels) for more information.

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Specifies the site collection to add the group to. If not specified the currently connected site collection will be used.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

