---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpteamschannel
schema: 2.0.0
title: Add-PnPTeamsChannel
---

# Add-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a channel to an existing Microsoft Teams instance.

## SYNTAX

### Public channel
```
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> [-Description <String>]
 [-ByPassPermissionCheck] [<CommonParameters>]
```

### Private channel
```
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> [-Description <String>] [-Private]
 -OwnerUPN <String> [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsChannel -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -DisplayName "My Channel"
```

Adds a new channel to the specified Teams instance

### EXAMPLE 2
```powershell
Add-PnPTeamsChannel -Team MyTeam -DisplayName "My Channel"
```

Adds a new channel to the specified Teams instance

### EXAMPLE 3
```powershell
Add-PnPTeamsChannel -Team MyTeam -DisplayName "My Channel" -Private
```

Adds a new private channel to the specified Teams instance

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

### -Description
An optional description of the channel.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

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
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OwnerUPN
{{ Fill OwnerUPN Description }}

```yaml
Type: String
Parameter Sets: Private channel
Aliases:

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
Aliases:

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
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)