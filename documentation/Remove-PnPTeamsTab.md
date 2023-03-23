---
Module Name: PnP.PowerShell
title: Remove-PnPTeamsTab
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsTab.html
---
 
# Remove-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes a Microsoft Teams tab in a channel.

## SYNTAX

```powershell
Remove-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -Identity <TeamsTabPipeBind>
 [-Force]  [<CommonParameters>]
```

## DESCRIPTION

Allows to remove a tab from channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel "General" -Identity Wiki
```
Removes the tab with the display name 'Wiki' from the General channel using display name.

### EXAMPLE 2
```powershell
Remove-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity Wiki
```

Removes the tab with the display name 'Wiki' from the channel using id.

### EXAMPLE 3
```powershell
Remove-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity fcef815d-2e8e-47a5-b06b-9bebba5c7852
```

Removes a tab with the specified id from the channel

## PARAMETERS

### -Channel
Specify the channel id or display name of the channel to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

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

### -Identity
Specify the id of the tab 

```yaml
Type: TeamsTabPipeBind
Parameter Sets: (All)

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

