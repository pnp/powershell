---
Module Name: PnP.PowerShell
title: Remove-PnPTeamsUser
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsUser.html
---
 
# Remove-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All, TeamMember.ReadWrite.All

Removes a user from a team.

## SYNTAX

```powershell
Remove-PnPTeamsUser -Team <TeamsTeamPipeBind> -User <String> [-Role <String>] [-Force] 
 
```

## DESCRIPTION

Allows to remove user from a team.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsUser -Team MyTeam -User john@doe.com
```

Removes the user specified from both owners and members of the team.

### EXAMPLE 2
```powershell
Remove-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Owner
```

Removes the user john@doe.com from the owners of the team, but retains the user as a member.

### EXAMPLE 3
```powershell
Remove-PnPTeamsUser -Team MyTeam -Users "john@doe.com","jane@doe.com","mark@doe.com"
```

Removes the users john@doe.com, jane@doe.com and mark@doe.com from the team.

## PARAMETERS

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

### -Role
Specify the role of the user you are removing from the team. Accepts "Owner" and "Member" as possible values.
        If specified as "Member" then the specified user is removed from the Team completely even if they were the owner of the Team. If "Owner" is specified in the -Role parameter then the
        specified user is removed as an owner of the team but stays as a team member. Defaults to "Member". Note: The last owner cannot be removed from the team.

```yaml
Type: String
Parameter Sets: (User)

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

### -Users
Specify the list of UPN (e.g. john@doe.com)

```yaml
Type: String
Parameter Sets: (Users)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

