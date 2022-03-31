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
Specifies the Url to add

```yaml
Type: String
Parameter Sets: (All)

Required: True
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


