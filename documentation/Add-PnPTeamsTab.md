---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsTab.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTeamsTab
---
  
# Add-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a tab to an existing Channel. Note that the `-ContentUrl` is a 'dynamic' parameter and will only be valid for tab types that support it.

## SYNTAX

```powershell
Add-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -DisplayName <String>
 -Type <TeamTabType> -ContentUrl <String> [<CommonParameters>]
```

## DESCRIPTION

Allows to add a tab to an existing Channel. By using `ContentUrl` option you may specify the content of the tab.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsTab -Team "My Team" -Channel "My Channel" -DisplayName "My Tab Name" -Type WebSite -ContentUrl "https://aka.ms/m365pnp"
```

Adds a web site tab to the specified channel.

### EXAMPLE 2
```powershell
Add-PnPTeamsTab -Team "My Team" -Channel "My Channel" -DisplayName "My Tab Name" -Type PDF -ContentUrl "https://contoso.sharepoint.com/sites/Marketing/Shared Documents/General/MyFile.pdf" -EntityId "null"
```

Adds the specified PDF file from the "Documents" library as a tab.

### EXAMPLE 3
```powershell
Add-PnPTeamsTab -Team "My Team" -Channel "My Channel" -DisplayName "My Tab Name" -Type SharePointPageAndList -WebSiteUrl "https://contoso.sharepoint.com/sites/Marketing/SitePages/Home.aspx"
```

Adds the specified SharePoint page as a tab in Teams. Note that the ContentUrl will automatically be generated and cannot be specified, and the `-WebsiteUrl` parameter is only available from version `2.x` onwards.

## PARAMETERS

### -Channel
Specify the channel id or name of the team to retrieve.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ContentUrl
The Url to use to render the content inside the tab within Microsoft Teams.

When using Type SharePointPageAndList, the ContentUrl will automatically be generated and cannot be specified.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebsiteUrl
The Url to use when the user clicks on the dropdown of the tab in Microsoft Teams and clicks on "Go to website".

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
Specify the tab type

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Team
Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Type
Specify the tab type

```yaml
Type: TeamTabType
Parameter Sets: (All)
Accepted values: WebSite, DocumentLibrary, Wiki, Planner, MicrosoftStream, MicrosoftForms, Word, Excel, PowerPoint, PDF, OneNote, PowerBI, SharePointPageAndList, Custom

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveUrl
Specifies the URL to be called by Teams client when a Tab is removed using the Teams Client.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EntityId
Specifies the Identifier for the entity hosted by the tab provider.

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
