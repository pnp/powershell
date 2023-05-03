---
Module Name: PnP.PowerShell
title: Get-PnPTeamsChannelUser
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelUser.html
---
 
# Get-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.Read.All

Returns members from the specified Microsoft Teams private Channel.

## SYNTAX

```powershell
Get-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> [-Identity <TeamsChannelMemberPipeBind>] [-Role <String>]
  
```

## DESCRIPTION

Allows to retrieve list of members of the specified private channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel"
```

Returns all owners, members and guests from the specified channel.

### EXAMPLE 2

```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Role Member
```

Returns all members from the specified channel.

### EXAMPLE 3

```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity john.doe@contoso.com
```

Returns membership of the user "john.doe@contoso.com" for the specified channel.

### EXAMPLE 4

```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity 00000000-0000-0000-0000-000000000000
```

Returns membership of the user with ID "00000000-0000-0000-0000-000000000000" for the specified channel.

## PARAMETERS

### -Identity
Specify membership id, UPN or user ID of the channel member.

```yaml
Type: TeamsChannelMemberPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Channel
Specify id or name of the channel to use.

```yaml
Type: TeamsChannelPipeBind
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
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role
Specify to filter on the role of the user.

```yaml
Type: String
Parameter Sets: (All)
Accepted values: Owner, Member, Guest

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
