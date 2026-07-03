---
Module Name: PnP.PowerShell
title: Get-PnPTeamsTab
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsTab.html
---
 
# Get-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets one or all tabs in a channel.

## SYNTAX

```powershell
Get-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> [-Identity <TeamsTabPipeBind>]
  
```

## DESCRIPTION

Allows to retrieve tabs in channel. By using `Identity` it is possible to retrieve a specific single tab.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype
```

Retrieves the tabs for the specified Microsoft Teams instance and channel

### EXAMPLE 2
```powershell
Get-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity "Wiki"
```

Retrieves a tab with the display name 'Wiki' from the specified team and channel

### EXAMPLE 3
```powershell
Get-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity d8740a7a-e44e-46c5-8f13-e699f964fc25
```

Retrieves a tab with an id from the specified team and channel

### EXAMPLE 4
```powershell
Get-PnPTeamsTab -Team "My Team" -Channel "My Channel"
```

Retrieves the tabs for the specified Microsoft Teams instance and channel

### EXAMPLE 5
```powershell
Get-PnPTeamsTab -Team "My Team" -Channel "My Channel" -Identity "Wiki"
```

Retrieves a tab with the display name 'Wiki' from the specified team and channel

## PARAMETERS

### -Channel
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

### -Identity
Specify the id or display name of the tab

```yaml
Type: TeamsTabPipeBind
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
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

