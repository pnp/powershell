---
Module Name: PnP.PowerShell
title: Remove-PnPTeamsChannelUser
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsChannelUser.html
---
 
# Remove-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.ReadWrite.All

Removes a specified user of a specified Microsoft Teams private Channel.

## SYNTAX

```powershell
Remove-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -Identity <TeamsChannelMemberPipeBind> [-Force]
  
```

## DESCRIPTION

Allows to remove a user from specified private channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity MCMjMiMjMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIyMxOTowMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMEB0aHJlYWQuc2t5cGUjIzAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMA==
```

Removes the user with specific membership ID from the specified Teams channel.

### EXAMPLE 2

```powershell
Remove-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity 00000000-0000-0000-0000-000000000000
```

Removes the user with ID "00000000-0000-0000-0000-000000000000" from the specified Teams channel.

### EXAMPLE 3

```powershell
Remove-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity john.doe@contoso.com -Force
```

Removes the user "john.doe@contoso.com" from the specified Teams channel without confirmation prompt.

## PARAMETERS

### -Identity
Specify membership id, UPN or user ID of the channel member.

```yaml
Type: TeamsChannelMemberPipeBind
Parameter Sets: (All)

Required: True
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

### -Force
Specifying the Force parameter will skip the confirmation question.

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
