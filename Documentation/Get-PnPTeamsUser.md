---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpteamsuser
schema: 2.0.0
title: Get-PnPTeamsUser
---

# Get-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Returns owners, members or guests from a team.

## SYNTAX

```
Get-PnPTeamsUser -Team <TeamsTeamPipeBind> [-Channel <TeamsChannelPipeBind>] [-Role <String>]
 [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

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

Returns all guestss from the specified team.

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Channel
{{ Fill Channel Description }}

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)
Aliases:

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
Aliases:
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
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)