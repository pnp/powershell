---
Module Name: PnP.PowerShell
title: Get-PnPTeamsPrimaryChannel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsPrimaryChannel.html
---
 
# Get-PnPTeamsPrimaryChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Channel.ReadBasic.All, ChannelSettings.Read.All, ChannelSettings.ReadWrite.All

Gets the default channel, General, of a team.

## SYNTAX

```powershell
Get-PnPTeamsPrimaryChannel -Team <TeamsTeamPipeBind> [-Identity <TeamsChannelPipeBind>] 
 [<CommonParameters>]
```

## DESCRIPTION
Gets the default channel, General, of a team.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsPrimaryChannel -Team ee0f40fc-b2f7-45c7-b62d-11b90dd2ea8e
```

Gets the default channel of the Team with the provided Id

### EXAMPLE 2
```powershell
Get-PnPTeamsPrimaryChannel -Team Sales
```

Gets the default channel of the Sales Team

## PARAMETERS

### -Team
The group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/team-get-primarychannel)
