---
Module Name: PnP.PowerShell
title: Set-PnPHubSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPHubSite.html
---
 
# Set-PnPHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets hub site properties.

## SYNTAX

```powershell
Set-PnPHubSite [-Identity] <HubSitePipeBind> [-Title <String>] [-LogoUrl <String>] [-Description <String>]
 [-SiteDesignId <Guid>] [-HideNameInNavigation] [-RequiresJoinApproval] [-EnablePermissionsSync] [-ParentHubSiteId <Guid>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Allows configuring a hub site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPHubSite -Identity "https://tenant.sharepoint.com/sites/myhubsite" -Title "My New Title"
```

Sets the title of the hub site.

### EXAMPLE 2
```powershell
Set-PnPHubSite -Identity "https://tenant.sharepoint.com/sites/myhubsite" -Description "My updated description"
```

Sets the description of the hub site.

### EXAMPLE 3
```powershell
Set-PnPHubSite -Identity "https://tenant.sharepoint.com/sites/myhubsite" -SiteDesignId df8a3ef1-9603-44c4-abd9-541aea2fa745
```

Sets the site design which should be applied to sites joining the hub site.

### EXAMPLE 4
```powershell
Set-PnPHubSite -Identity "https://tenant.sharepoint.com/sites/myhubsite" -LogoUrl "https://tenant.sharepoint.com/SiteAssets/Logo.png"
```

Sets the logo of the hub site.

### EXAMPLE 5
```powershell
Set-PnPHubSite -Identity "https://tenant.sharepoint.com/sites/myhubsite" -EnablePermissionsSync
```

Syncs hub permissions to associated sites.

### EXAMPLE 6
```powershell
Set-PnPHubSite -Identity "https://tenant.sharepoint.com/sites/myhubsite" -RequiresJoinApproval:$false
```
Disables the join approval requirement.

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
Description of the hub site collection.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideNameInNavigation
Hides the name of the hub site in the navigation.
```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The URL of the hub site collection.

```yaml
Type: HubSitePipeBind
Parameter Sets: (All)
Aliases: HubSite

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -LogoUrl
The logoUrl of the Hub site.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequiresJoinApproval
Requires new associated sites to obtain approval to join the hub site. Note that if set to `$true`, sites will be able to join the hub only if there is an active approval flow available.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteDesignId
GUID of the SharePoint Site Design which should be applied when a site joins the hub site.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the hub site collection.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnablePermissionsSync
Sync hub permissions to associated sites.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ParentHubSiteId
The ID of the parent hub site.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

