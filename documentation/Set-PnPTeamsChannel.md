---
Module Name: PnP.PowerShell
title: Set-PnPTeamsChannel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsChannel.html
---
 
# Set-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing Teams Channel.

## SYNTAX

```powershell
Set-PnPTeamsChannel -Team <TeamsTeamPipeBind> -Identity <TeamsChannelPipeBind> [-DisplayName <String>] [-Description <String>] [-IsFavoriteByDefault <Boolean>] [-AllowNewMessageFromBots <Boolean>] [-AllowNewMessageFromConnectors <Boolean>] [-ReplyRestriction <TeamChannelModerationSettingReplyRestriction>] [-UserNewMessageRestriction <TeamChannelModerationSettingNewMessageRestriction>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to update an existing Teams Channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsChannel -Team "MyTeam" -Channel "MyChannel" -DisplayName "My Channel"
```

Updates the channel called 'MyChannel' to have the display name set to 'My Channel'.

### EXAMPLE 2
```powershell
Set-PnPTeamsChannel -Team "MyTeam" -Channel "MyChannel" -IsFavoriteByDefault $true
```

Updates the channel called 'MyChannel' to make it visible to members.

## PARAMETERS

### -AllowNewMessageFromBots
Allows configuring if bots are allowed to post messages in the channel.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowNewMessageFromConnectors
Allows configuring if connectors are allowed to post messages in the channel.

```yaml
Type: Boolean
Parameter Sets: (All)

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
Changes the description of the specified channel.

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
Changes the display name of the specified channel.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Specify the channel id or display name of the channel to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -IsFavoriteByDefault
Allows you to specify if the channel is by default visible for members.

```yaml
Type: Boolean
Parameter Sets: Public channel

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReplyRestriction
Allows configuring who can reply to posts in the channel.

```yaml
Type: TeamChannelModerationSettingReplyRestriction
Parameter Sets: (All)

Required: False
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

### -UserNewMessageRestriction
Allows configuring who can post new messages in the channel.

```yaml
Type: TeamChannelModerationSettingNewMessageRestriction
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
