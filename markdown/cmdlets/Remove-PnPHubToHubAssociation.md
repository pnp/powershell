---
Module Name: PnP.PowerShell
title: Remove-PnPHubToHubAssociation
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPHubToHubAssociation.html
---
 
# Remove-PnPHubToHubAssociation

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes the selected hub site from its parent hub.

## SYNTAX

### By Id

```powershell
Remove-PnPHubToHubAssociation -HubSiteId <Guid>
```

### By Url

```powershell
Remove-PnPHubToHubAssociation -HubSiteUrl <string>
```

## DESCRIPTION
Use this cmdlet to remove the selected hub site from its parent hub.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPHubToHubAssociation -HubSiteId 6638bd4c-d88d-447c-9eb2-c84f28ba8b15
```

This example removes the hubsite with id 6638bd4c-d88d-447c-9eb2-c84f28ba8b15 from its parent hub.

### EXAMPLE 2
```powershell
Remove-PnPHubToHubAssociation -HubSiteUrl "https://yourtenant.sharepoint.com/sites/sourcehub"
```
This example removes the hubsite with id https://yourtenant.sharepoint.com/sites/sourcehub from its parent hub.

## PARAMETERS

### -HubSiteId
Id of the hubsite to remove from its parent.

```yaml
Type: Guid
Parameter Sets: By Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HubSiteUrl
Url of the hubsite to remove from its parent.

```yaml
Type: String
Parameter Sets: By Url

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

