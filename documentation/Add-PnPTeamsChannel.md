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

Adds a channel to an existing Microsoft Teams instance.

## SYNTAX

### Public channel
```powershell
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> [-Description <String>] [-IsFavoriteByDefault <Boolean>] [<CommonParameters>]
```

### Private channel
```powershell
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> -OwnerUPN <String> [-Description <String>] [-Private] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsChannel -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -DisplayName "My Channel" -IsFavoriteByDefault $true
```

Adds a new channel to the specified Teams instance and marks the channel as by default visible for members.

### EXAMPLE 2
```powershell
Add-PnPTeamsChannel -Team MyTeam -DisplayName "My Channel"
```

Adds a new channel to the specified Teams instance

### EXAMPLE 3
```powershell
Add-PnPTeamsChannel -Team MyTeam -DisplayName "My Channel" -Private -OwnerUPN user1@domain.com
```

Adds a new private channel to the specified Teams instance

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
The display name of the new channel. Letters, numbers and spaces are allowed.

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
Allows you to specify if the channel is by default visible for members

```yaml
Type: Boolean
Parameter Sets: Public channel

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
Parameter Sets: Private channel

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Private
Specify to mark the channel as private.

```yaml
Type: SwitchParameter
Parameter Sets: Private channel

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
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


