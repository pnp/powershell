---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTeamsUser
---
  
# Add-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a user to an existing Microsoft Teams instance.

## SYNTAX

### User
```powershell
Add-PnPTeamsUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -User <String> -Role <String> 
```

### Users
```powershell
Add-PnPTeamsUser -Team <TeamsTeamPipeBind> -Users <String[]> -Role <String> 
```

## DESCRIPTION
This cmdlet adds one or more users to an existing Team. 

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Owner
```

Adds a user as an owner to the team.

### EXAMPLE 2
```powershell
Add-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Member
```

Adds a user as a member to the team.

### EXAMPLE 3
```powershell
Add-PnPTeamsUser -Team MyTeam -Users "john@doe.com","jane@doe.com" -Role Member
```

Adds multiple users as members to the team.

### EXAMPLE 4
```powershell
Add-PnPTeamsUser -Team MyTeam -User "jane@doe.com" -Role Member -Channel Private
```

Adds user as a member to a private channel named Private in MyTeam team.

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

### -Channel
Specify the channel id or name of the team to retrieve.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (User)

Required: False
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
Specify the users UPN (e.g. john@doe.com, jane@doe.com)

```yaml
Type: String array
Parameter Sets: (Users)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


