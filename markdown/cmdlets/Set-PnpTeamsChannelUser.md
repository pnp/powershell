---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsChannelUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPTeamsChannelUser
---
  
# Set-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.ReadWrite.All

Updates the role of a user in an existing Microsoft Teams private channel.

## SYNTAX

```powershell
Set-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -Identity <TeamsChannelMemberPipeBind> -Role <String> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to update the role of a user in an existing Microsoft Teams private channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsChannelUser -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -Channel "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype" -Identity MCMjMiMjMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIyMxOTowMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMEB0aHJlYWQuc2t5cGUjIzAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMA== -Role Owner
```
Updates the user with specific membership ID as owner of the specified Teams private channel.

### EXAMPLE 2
```powershell
Set-PnPTeamsChannelUser -Team "My Team" -Channel "My Private Channel" -Identity john@doe.com -Role Member
```
Updates the user john@doe.com as member of the specified Teams private channel.

## PARAMETERS

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

### -Role
Specify the role of the user.

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
