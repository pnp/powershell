---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsChannelUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTeamsChannelUser
---
  
# Add-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.ReadWrite.All

Adds a user to an existing Microsoft Teams private channel.

## SYNTAX

```powershell
Add-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -User <String> -Role <String> 
```

## DESCRIPTION

Allows to add a user to a private channel in Microsoft Teams.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsChannelUser -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -Channel "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype" -User john@doe.com -Role Owner
```

Adds user as an owner to the private channel.

### EXAMPLE 2
```powershell
Add-PnPTeamsChannelUser -Team "My Team" -Channel "My Private Channel" -User john@doe.com -Role Member
```

Adds user as a member to the private channel.

## PARAMETERS

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

### -Channel
The id or name of the channel to retrieve.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
Specify the UPN (e.g. john@doe.com)

```yaml
Type: String
Parameter Sets: (User)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role
Specify the role of the user

```yaml
Type: String
Parameter Sets: (All)
Accepted values: Owner, Member

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)