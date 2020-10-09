---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpteamschannelmessage
schema: 2.0.0
title: Get-PnPTeamsChannelMessage
---

# Get-PnPTeamsChannelMessage

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Sends a message to a Microsoft Teams Channel.

## SYNTAX

```powershell
Get-PnPTeamsChannelMessage -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> [-IncludeDeleted]
 [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsChannelMessage -Team MyTestTeam -Channel "My Channel"
```

Gets all messages of the specified channel

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Channel
Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeDeleted
Specify to include deleted messages

```yaml
Type: SwitchParameter
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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)