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
New-PnPSite -Type TeamSite -Title <String> -Alias <String> [-Description <String>] [-Classification <String>] [-IsPublic] [-Lcid <UInt>] [-Owners <String[]>] [-PreferredDataLocation <Office365Geography>] [-SensitivityLabel <String>] [-HubSiteId <Guid>] [-SiteAlias <String>] [-TimeZone <PnP.Framework.Enums.TimeZone>] [-Members <String[]>] [-WelcomeEmailDisabled <SwitchParameter>] [-SubscribeNewGroupMembers <SwitchParameter>] [-AllowOnlyMembersToPost <SwitchParameter>] [-CalendarMemberReadOnly <SwitchParameter>] 
[-ConnectorsDisabled <SwitchParameter>] [-HideGroupInOutlook <SwitchParameter>] [-SubscribeMembersToCalendarEventsDisabled <SwitchParameter>] [-SiteDesignId <Guid>] [-Wait] [-Connection <PnPConnection>]
 
```

### CommunicationSite
```powershell
New-PnPSite -Type CommunicationSite -Title <String> -Url <String> [-HubSiteId <Guid>] [-Classification <String>] [-SiteDesign <SiteDesign>] [-SiteDesignId <Guid>] [-Lcid <UInt>] [-Owner <String>] [-PreferredDataLocation <Office365Geography>] [-SensitivityLabel <String>] [-TimeZone <PnP.Framework.Enums.TimeZone>]
```

### TeamSiteWithoutMicrosoft365Group
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title <String> -Url <String> [-HubSiteId <Guid>] [-Classification <String>] [-SiteDesignId <Guid>] [-Lcid <UInt>] [-Owner <String>] [-PreferredDataLocation <Office365Geography>] [-SensitivityLabel <String>] [-TimeZone <PnP.Framework.Enums.TimeZone>]
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
New-PnPSite -Type TeamSite -Title 'Team Contoso' -Alias contoso -SiteAlias contoso-site
```

This will create a new Modern Team Site collection with the title 'Team Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso-site' or 'https://tenant.sharepoint.com/teams/contoso-site' based on the managed path configuration in the SharePoint Online Admin portal. The underlying M365 Group will have 'contoso' as the alias.

### EXAMPLE 11
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'

### EXAMPLE 12
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -SiteDesignId ae2349d5-97d6-4440-94d1-6516b72449ac
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. It will use the specified custom site design for the site.

### EXAMPLE 13
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -Classification "HBI"
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. The classification for the site will be set to "HBI"

### EXAMPLE 14
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -ShareByEmailEnabled
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. Allows owners to invite users outside of the organization.

### EXAMPLE 15
```powershell
New-PnPSite -Type TeamSiteWithoutMicrosoft365Group -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -Lcid 1040
```

This will create a new Modern team site collection not connected to M365 group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' and sets the default language to Italian (LCID 1040).

### EXAMPLE 16
```powershell
New-PnPSite -Type TeamSite -TimeZone UTCPLUS0200_HELSINKI_KYIV_RIGA_SOFIA_TALLINN_VILNIUS -Title "Contoso" -Alias "Contoso"
```

This will create a new Modern team site collection connected to a Microsoft 365 Group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' and sets the timezone to UTC + 2 which is the Eastern European time zone.

### EXAMPLE 17
```powershell
New-PnPSite -Type TeamSite -TimeZone UTCPLUS0200_HELSINKI_KYIV_RIGA_SOFIA_TALLINN_VILNIUS -Title "Contoso" -Alias "Contoso" -WelcomeEmailDisabled -SubscribeNewGroupMembers -AllowOnlyMembersToPost -CalendarMemberReadOnly -ConnectorsDisabled -HideGroupInOutlook -SubscribeMembersToCalendarEventsDisabled
```

This will create a new Modern team site collection connected to a Microsoft 365 Group with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' and sets the timezone to UTC + 2 which is the Eastern European time zone. In addition to that, **if application permissions are used** , it will also set resource behavior options to disable welcome mails, make calendar read only , hide the group visibility in outlook and other options

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
If specified the site will be associated to the hubsite as identified by this id.
**Note: Only applicable when delegated permissions are used.**

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
Identifies whether the corresponding Microsoft365 group type is Private or Public. If not specified, group is considered Private.
Content in a Public group can be seen by anybody in the organization, and anybody in the organization is able to join the group. 
Content in a Private group can only be seen by the members of the group and people who want to join a private group have to be approved by a group owner.

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
The language to use for the site. For more information, see Locale IDs supported by SharePoint at https://github.com/pnp/powershell/wiki/Supported-LCIDs-by-SharePoint. To get the list of supported languages on a SharePoint environment use: Get-PnPAvailableLanguage.

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
Specifies one or more users with full access on the site and owner permissions on the corresponding Microsoft 365 Group. Can be used when Team Site is being created.  Requires user object. If this parameter is skipped or a group object is provided, the user running New-PnPSite command will be set as a site owner. Required in case of the app-only connection.

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
Specifies a Site Collection Administrator for the new site. Can be used when Communication Site is being created or Teams Site should not get a corresponding Microsoft 365 group.  Requires user object. If this parameter is skipped or a group object is provided, the user running New-PnPSite command will be set as a site owner. Required in case of the app-only connection.

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

### -SiteAlias
The site alias to use for the team site URL. If specified, a site collection will be created based on its value, otherwise the value specified in Alias parameter will be used.

```yaml
Type: String
Parameter Sets: TeamSite

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteDesign
Allows to specify an OOTB (out of the box) site design

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
Parameter Sets: CommunicationSite, TeamSiteWithoutMicrosoft365Group, TeamSite

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

### -TimeZone
Specifies the timezone of the site to create.
To get the full list of timezone that you can select, you can visit [https://pnp.github.io/pnpframework/api/PnP.Framework.Enums.TimeZone.html](https://pnp.github.io/pnpframework/api/PnP.Framework.Enums.TimeZone.html)

```yaml
Type: Framework.Enums.TimeZone
Parameter Sets: CommunicationSite, TeamSiteWithoutMicrosoft365Group, TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Members

Set the members of the team site connected group. Specify the UPN values in a string array.
**Note: Only applicable when application permissions are used.** 

```yaml
Type: String[]
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WelcomeEmailDisabled

If true, welcome emails are not sent to new members. 
**Note: Only applicable when application permissions are used.**

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubscribeNewGroupMembers

If true, group members are subscribed to receive group conversations. 
**Note: Only applicable when application permissions are used.**

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubscribeMembersToCalendarEventsDisabled

If true, members are not subscribed to the group's calendar events in Outlook. 
**Note: Only applicable when application permissions are used.**

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideGroupInOutlook

If true, members are not subscribed to the group's calendar events in Outlook. 
**Note: Only applicable when application permissions are used.**

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConnectorsDisabled

If true, changes made to the group in Exchange Online are not synced back to on-premises Active Directory. **Note: Only applicable when application permissions are used.**

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CalendarMemberReadOnly

If true, members can view the group calendar in Outlook but cannot make changes. 
**Note: Only applicable when application permissions are used.**

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowOnlyMembersToPost

If true, only group members can post conversations to the group. 
**Note: Only applicable when application permissions are used.**

```yaml
Type: SwitchParameter
Parameter Sets: TeamSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteDesignId

The ID of the Site Design to apply.
**Note: Only applicable when delegated permissions are used.**

```yaml
Type: GUID
Parameter Sets: TeamSite

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
[https://pnp.github.io/pnpframework/api/PnP.Framework.Enums.TimeZone.html](https://pnp.github.io/pnpframework/api/PnP.Framework.Enums.TimeZone.html)
