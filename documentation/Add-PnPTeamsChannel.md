---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsChannel.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTeamsChannel
---
  
# Add-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a channel to an existing Microsoft Teams team.

## SYNTAX

### Standard channel
```powershell
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> [-ChannelType Standard] [-Description <String>] [-IsFavoriteByDefault <Boolean>]
```

### Private channel
```powershell
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> -ChannelType Private -OwnerUPN <String> [-Description <String>]
```

### Shared channel
```powershell
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> -ChannelType Shared -OwnerUPN <String> [-Description <String>] [-IsFavoriteByDefault <Boolean>]
```

## DESCRIPTION

Allows to add channel to an existing team in Microsoft Teams. By using the `IsFavoriteByDefault` it is possible to specify if the channel will be visible for members by default.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsChannel -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -DisplayName "My Channel" -IsFavoriteByDefault $true
```

Adds a new standard channel to the Team specified by its identifier and marks the channel as by default visible for members.

### EXAMPLE 2
```powershell
Add-PnPTeamsChannel -Team "My Team" -DisplayName "My standard channel"
```

Adds a new standard channel to the Team specified by its name.

### EXAMPLE 3
```powershell
Add-PnPTeamsChannel -Team "HR" -DisplayName "My private channel" -ChannelType Private -OwnerUPN user1@domain.com
```

Adds a new private channel to the Team specified by its name and sets the provided user as the owner of the channel.

### EXAMPLE 4
```powershell
Add-PnPTeamsChannel -Team "Logistical Department" -DisplayName "My shared channel" -ChannelType Shared -OwnerUPN user1@domain.com
```

Adds a new shared channel to the Team specified by its name and sets the provided user as the owner of the channel.

## PARAMETERS

### -Description
An optional description of the channel.

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
The display name of the new channel. Letters, numbers, and spaces are allowed.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsFavoriteByDefault
Allows you to specify if the channel is by default visible for members.
** This parameter is obsolete. [ Microsoft Graph API docs](https://learn.microsoft.com/en-us/graph/api/resources/channel?view=graph-rest-1.0#properties) mention that it only works when you create a channel in Teams creation request. It will be removed in a future version. **

```yaml
Type: Boolean
Parameter Sets: Standard channel, Shared channel

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OwnerUPN
The User Principal Name (email) of the owner of the channel.

```yaml
Type: String
Parameter Sets: Private channel, Shared channel

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ChannelType
Allows specifying the type of channel to be created. Possible values are Standard, Private, and Shared.

```yaml
Type: SwitchParameter
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
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)