---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Update-PnPTeamsUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Update-PnPTeamsUser
---
  
# Update-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates a user's role in an existing Microsoft Teams instance.

## SYNTAX

```powershell
Update-PnPTeamsUser -Team <TeamsTeamPipeBind> -User <String> -Role <String> [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Update-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Owner
```

Updates a user as an owner to the team

### EXAMPLE 2
```powershell
Update-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Member
```

Updates a user as a member in the team

## PARAMETERS

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
[Microsoft Graph documentation](https://docs.microsoft.com/graph/api/team-update-members)


