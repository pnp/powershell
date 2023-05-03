---
Module Name: PnP.PowerShell
title: Set-PnPTeamsTeam
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsTeam.html
---
 
# Set-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing Team.

## SYNTAX

```powershell
Set-PnPTeamsTeam -Identity <TeamsTeamPipeBind> [-DisplayName <String>] [-Description <String>]
 [-Visibility <TeamVisibility>] [-AllowAddRemoveApps <Boolean>] [-AllowChannelMentions <Boolean>]
 [-AllowCreateUpdateChannels <Boolean>] [-AllowCreateUpdateRemoveConnectors <Boolean>]
 [-AllowCreateUpdateRemoveTabs <Boolean>] [-AllowCustomMemes <Boolean>] [-AllowDeleteChannels <Boolean>]
 [-AllowGiphy <Boolean>] [-AllowGuestCreateUpdateChannels <Boolean>] [-AllowGuestDeleteChannels <Boolean>]
 [-AllowOwnerDeleteMessages <Boolean>] [-AllowStickersAndMemes <Boolean>] [-AllowTeamMentions <Boolean>]
 [-AllowUserDeleteMessages <Boolean>] [-AllowUserEditMessages <Boolean>]
 [-GiphyContentRating <TeamGiphyContentRating>] [-ShowInTeamsSearchAndSuggestions <Boolean>]
 [-Classification <String>]  
```

## DESCRIPTION

Allows to update team settings.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsTeam -Identity 'MyTeam' -DisplayName 'My Team'
```

Updates the team called 'MyTeam' to have the display name set to 'My Team'.

### EXAMPLE 2
```powershell
Set-PnPTeamsTeam -Identity "baba9192-55be-488a-9fb7-2e2e76edbef2" -Visibility Public
```

Updates the team by using id to have the visibility Public.

### EXAMPLE 3
```powershell
Set-PnPTeamsTeam -Identity "My Team" -AllowTeamMentions $false -AllowChannelMentions $true -AllowDeleteChannels $false
```

Updates the team 'My Team' to disallow Team @mentions, allow Channel @mentions and disallow members from deleting channels.

### EXAMPLE 4
```powershell
Set-PnPTeamsTeam -Identity "My Team" -GiphyContentRating Moderate
```

Updates the team 'My Team' to have a Moderate level of sensitivity for giphy usage.

## PARAMETERS

### -AllowAddRemoveApps
Boolean value that determines whether or not members (not only owners) are allowed to add apps to the team.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowChannelMentions
Boolean value that determines whether or not channels in the team can be @ mentioned so that all users who follow the channel are notified.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowCreateUpdateChannels
Setting that determines whether or not members (and not just owners) are allowed to create channels.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowCreateUpdateRemoveConnectors
Setting that determines whether or not members (and not only owners) can manage connectors in the team.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowCreateUpdateRemoveTabs
Setting that determines whether or not members (and not only owners) can manage tabs in channels.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowCustomMemes
Setting that determines whether or not members can use the custom memes functionality in teams.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowDeleteChannels
Setting that determines whether or not members (and not only owners) can delete channels in the team.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowGiphy
Setting that determines whether or not giphy can be used in the team.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowGuestCreateUpdateChannels
Setting that determines whether or not guests can create channels in the team.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowGuestDeleteChannels
Setting that determines whether or not guests can delete in the team.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowOwnerDeleteMessages
Setting that determines whether or not owners can delete messages that they or other members of the team have posted.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowStickersAndMemes
Setting that determines whether stickers and memes usage is allowed in the team.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowTeamMentions
Setting that determines whether the entire team can be @ mentioned (which means that all users will be notified)

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowUserDeleteMessages
Setting that determines whether or not members can delete messages that they have posted.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowUserEditMessages
Setting that determines whether or not users can edit messages that they have posted.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Classification

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Changes the description of the specified team.

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
Changes the display name of the specified team.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GiphyContentRating
Setting that determines the level of sensitivity of giphy usage that is allowed in the team. Accepted values are "Strict" or "Moderate"

```yaml
Type: TeamGiphyContentRating
Parameter Sets: (All)
Accepted values: moderate, strict

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
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

### -ShowInTeamsSearchAndSuggestions
Setting that determines whether or not private teams should be searchable from Teams clients for users who do not belong to that team. Set to $false to make those teams not discoverable from Teams clients.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Visibility
Changes the visibility of the specified team.

```yaml
Type: TeamVisibility
Parameter Sets: (All)
Accepted values: Private, Public

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

