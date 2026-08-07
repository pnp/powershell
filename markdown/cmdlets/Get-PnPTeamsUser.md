---
Module Name: PnP.PowerShell
title: Get-PnPTeamsUser
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsUser.html
---
 
# Get-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All
  * Microsoft Graph API : Directory.Read.All

Returns owners, members or guests from a team.

## SYNTAX

```powershell
Get-PnPTeamsUser -Team <TeamsTeamPipeBind> [-Channel <TeamsChannelPipeBind>] [-Role <String>]
  
```

## DESCRIPTION

Allows to retrieve list of owners, members or guests from a team.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsUser -Team MyTeam
```

Returns all owners, members or guests from the specified team.

### EXAMPLE 2
```powershell
Get-PnPTeamsUser -Team MyTeam -Role Owner
```

Returns all owners from the specified team.

### EXAMPLE 3
```powershell
Get-PnPTeamsUser -Team MyTeam -Role Member
```

Returns all members from the specified team.

### EXAMPLE 4
```powershell
Get-PnPTeamsUser -Team MyTeam -Role Guest
```

Returns all guests from the specified team.

## PARAMETERS

### -Channel
Specify the channel id or display name of the channel to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role
Specify to filter on the role of the user

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

