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
Creates a communication site, Microsoft 365 group-connected team site or Modern team site not connected to M365 group.

## SYNTAX

### TeamSite
```powershell
New-PnPSite -Type TeamSite -Title <String> -Alias <String> [-Description <String>] [-Classification <String>] [-IsPublic] [-Lcid <UInt>] [-Owners <String[][]>] [-PreferredDataLocation <Office365Geography>] [-SensitivityLabel <String>] [-HubSiteId <Guid>] [-Wait] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### CommunicationSite
```powershell
New-PnPSite -Type CommunicationSite -Title <String> -Url <String> [-HubSiteId <Guid>] [-Classification <String>] [-SiteDesign <SiteDesign>] [-SiteDesignId <Guid>] [-Lcid <UInt>] [-Owner <String>] [-PreferredDataLocation <Office365Geography>] [-SensitivityLabel <String>]
```

### TeamSiteWithoutMicrosoft365Group
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title <String> -Url <String> [-HubSiteId <Guid>] [-Classification <String>] [-SiteDesignId <Guid>] [-Lcid <UInt>] [-Owner <String>] [-PreferredDataLocation <Office365Geography>] [-SensitivityLabel <String>]
```

## DESCRIPTION
The New-PnPSite cmdlet creates a new site collection for the current tenant. Currently only 'modern' sites like Communication Site , Modern Microsoft 365 group-connected team sites and Modern Team sites not connected to M365 groups are supported. If you want to create a classic site, use New-PnPTenantSite. Note that the -Type parameter is mandatory to be used to indicate which type of site you would like to create. Based on the type of site you specify, you will be able to provide the additional arguments that are valid for that site type, so it is recommended to provide this as the first argument.

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

This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' and sets the default language to Italian (LCID 1040).

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

### EXAMPLE 10
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'

### EXAMPLE 11
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -SiteDesignId ae2349d5-97d6-4440-94d1-6516b72449ac
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. It will use the specified custom site design for the site.

### EXAMPLE 12
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -Classification "HBI"
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. The classification for the site will be set to "HBI"

### EXAMPLE 13
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -ShareByEmailEnabled
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. Allows owners to invite users outside of the organization.

### EXAMPLE 14
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -Lcid 1040
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' and sets the default language to Italian (LCID 1040).

## PARAMETERS

### -Alias
The alias to use for the team site.

```yaml
Type: String
Parameter Sets: TeamSite

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Classification
The classification to use for the new site.

```yaml
Type: String
Parameter Sets: CommunicationSite, TeamSite, TeamSiteWithoutMicrosoft365Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description of the site to create

```yaml
Type: String
Parameter Sets: CommunicationSite, TeamSite, TeamSiteWithoutMicrosoft365Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
The description of the site to create
```yaml
Type: String
Parameter Sets: CommunicationSite, TeamSite, TeamSiteWithoutMicrosoft365Group

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

### -IsPublic
Specifies if the site is public.

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lcid
The language to use for the site.

```yaml
Type: SwitchParameter
Parameter Sets: CommunicationSite, TeamSite, TeamSiteWithoutMicrosoft365Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owners
One or more owners to specify for the site. Defaults to the current user. Required if you use an app-only connection.

```yaml
Type: String[]
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owner
The owner to specify for the site. Defaults to the current user. Required if you use an app-only connection.

```yaml
Type: String
Parameter Sets: CommunicationSite, TeamSiteWithoutMicrosoft365Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreferredDataLocation
In case of a multi-geo environment you can specify the preferred data location

```yaml
Type: String
Parameter Sets: CommunicationSite, TeamSite, TeamSiteWithoutMicrosoft365Group
Accepted values: APC, ARE, AUS, CAN, CHE, DEU, EUR, FRA, GBR, IND, JPN, KOR, NAM, NOR, ZAF

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SensitivityLabel
The sensitivity label to specify for the new site.

```yaml
Type: String
Parameter Sets: CommunicationSite, TeamSite, TeamSiteWithoutMicrosoft365Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShareByEmailEnabled
If specified sharing content by email will be enabled.

```yaml
Type: SwitchParameter
Parameter Sets: CommunicationSite, TeamSiteWithoutMicrosoft365Group

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteDesign
Allows to specify an OOTB site design

```yaml
Type: SwitchParameter
Parameter Sets: CommunicationSite
Accepted values: Blank, Topic, Showcase

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteDesignId
Allows to specify a custom site design

```yaml
Type: Guid
Parameter Sets: CommunicationSite, TeamSiteWithoutMicrosoft365Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Specifies the title of the site to create

```yaml
Type: String
Parameter Sets: CommunicationSite, TeamSiteWithoutMicrosoft365Group

Required: True
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
Accepted values: CommunicationSite, TeamSite, TeamSiteWithoutMicrosoft365Group

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

