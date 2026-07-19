---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPListDesign.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPListDesign
---
  
# Add-PnPListDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new List Design on the current tenant

## SYNTAX

### By SiteScript Instance (Default)

```powershell
Add-PnPListDesign -Title <String> -SiteScript <TenantSiteScriptPipeBind> [-Description <String>] 
 [-ListColor<TenantListDesignIcon>] [-ListIcon <TenantListDesignColor>] [-ThumbnailUrl <String>] 
 [-Connection <PnPConnection>]
```

### By SiteScript Ids

```powershell
Add-PnPListDesign -Title <String> -SiteScriptIds <Guid[]> [-Description <String>] 
 [-ListColor<TenantListDesignIcon>] [-ListIcon <TenantListDesignColor>] [-ThumbnailUrl <String>] 
 [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to add new List Design to tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPListDesign -Title "My Custom List" -SiteScriptIds "e84dcb46-3ab9-4456-a136-66fc6ae3d3c5"   
```

Adds a new List Design, with the specified title and description. When applied it will run the scripts as referenced by the IDs. Use Get-PnPSiteScript to receive site Scripts. 

### EXAMPLE 2
```powershell
Add-PnPListDesign -Title "My Company Design" -SiteScriptIds "6def687f-0e08-4f1e-999c-791f3af9a600" -Description "My description" -ListColor Orange -ListIcon BullseyeTarget -ThumbnailUrl "https://contoso.sharepoint.com/SiteAssets/site-thumbnail.png"
```

Adds a new List Design, with the specified title, description and list color, list icon and thumbnail to be shown in the template picker. When applied it will run the scripts as referenced by the IDs. Use Get-PnPSiteScript to receive Site Scripts. 

### EXAMPLE 3
```powershell
Get-PnPSiteScript -Identity "My List Script" | Add-PnPListDesign -Title "My Custom List"
```

Adds a new List Design, with the specified title based on a site script with the title "My List Script"

### EXAMPLE 4
```powershell
Get-PnPList -Identity "My List" | Get-PnPSiteScriptFromList | Add-PnPSiteScript -Title "My List Script" | Add-PnPListDesign -Title "My List"
```

Adds a new List Design and site script based on a list with the title "My List"

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

### -Description
The description of the list design

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteScript
An instance, id or title of a site script to use for the list design

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: By SiteScript Instance

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -SiteScriptIds
An array of guids of site scripts to use for the list design

```yaml
Type: Guid[]
Parameter Sets: By SiteScript Ids

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the list design

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListColor
The list color from the create list experience.

```yaml
Type: TenantListDesignColor
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListIcon
The list icon from the create list experience. 

```yaml
Type: TenantListDesignIcon
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ThumbnailUrl
The full URL of a thumbnail image, i.e. https://contoso.sharepoint/siteassets/image.png. If none is specified, SharePoint uses a generic image. This is the image that will be shown in the "From your organization" section of the "Create" List screen.

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