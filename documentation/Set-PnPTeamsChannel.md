---
Module Name: PnP.PowerShell
title: Set-PnPTeamsChannel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsChannel.html
---
 
# Set-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing Teams Channel

## SYNTAX

```powershell
Set-PnPTeamsChannel -Team <TeamsTeamPipeBind> -Identity <TeamsChannelPipeBind> [-DisplayName <String>]
 [-Description <String>]  [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsChannel -Team "MyTeam" -Channel "MyChannel" -DisplayName "My Channel"
```

Updates the channel called 'MyChannel' to have the display name set to 'My Channel'

## PARAMETERS

### -Description
Changes the description of the specified channel.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
Changes the display name of the specified channel.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Specify the channel id or display name of the channel to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

