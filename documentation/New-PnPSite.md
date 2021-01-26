---
Module Name: PnP.PowerShell
title: New-PnPSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSite.html
---
 
# New-PnPSite

## SYNOPSIS
Creates either a communication site or a Microsoft 365 group-connected team site

## SYNTAX

```powershell
New-PnPSite -Type <SiteType> [-HubSiteId <Guid>] [-Wait] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
The New-PnPSite cmdlet creates a new site collection for the current tenant. Currently only 'modern' sites like Communication Site and the Modern Microsoft 365 group-connected team sites are supported. If you want to create a classic site, use New-PnPTenantSite.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso
```

This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'

### EXAMPLE 2
```powershell
New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -SiteDesign Showcase
```

This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. It will use the 'Showcase' design for the site.

### EXAMPLE 3
```powershell
New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -SiteDesignId ae2349d5-97d6-4440-94d1-6516b72449ac
```

This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. It will use the specified custom site design for the site.

### EXAMPLE 4
```powershell
New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -Classification "HBI"
```

This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. The classification for the site will be set to "HBI"

### EXAMPLE 5
```powershell
New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -ShareByEmailEnabled
```

This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. Allows owners to invite users outside of the organization.

### EXAMPLE 6
```powershell
New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -Lcid 1040
```

This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' and sets the default language to Italian.

### EXAMPLE 7
```powershell
New-PnPSite -Type TeamSite -Title 'Team Contoso' -Alias contoso
```

This will create a new Modern Team Site collection with the title 'Team Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' or 'https://tenant.sharepoint.com/teams/contoso' based on the managed path configuration in the SharePoint Online Admin portal.

### EXAMPLE 8
```powershell
New-PnPSite -Type TeamSite -Title 'Team Contoso' -Alias contoso -IsPublic
```

This will create a new Modern Team Site collection with the title 'Team Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' or 'https://tenant.sharepoint.com/teams/contoso' based on the managed path configuration in the SharePoint Online Admin portal and sets the site to public.

### EXAMPLE 9
```powershell
New-PnPSite -Type TeamSite -Title 'Team Contoso' -Alias contoso -Lcid 1040
```

This will create a new Modern Team Site collection with the title 'Team Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' or 'https://tenant.sharepoint.com/teams/contoso' based on the managed path configuration in the SharePoint Online Admin portal and sets the default language of the site to Italian.

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

### -HubSiteId
If specified the site will be associated to the hubsite as identified by this id

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
Specifies with type of site to create.

```yaml
Type: SiteType
Parameter Sets: (All)
Accepted values: CommunicationSite, TeamSite

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Wait
If specified the cmdlet will wait until the site has been fully created and all site artifacts have been provisioned by SharePoint. Notice that this can take a while.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

