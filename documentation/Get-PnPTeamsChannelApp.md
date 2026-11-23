---
Module Name: PnP.PowerShell
title: Get-PnPTeamsChannelApp
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelApp.html
---
 
# Get-PnPTeamsChannelApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : TeamsAppInstallation.ReadForTeam
  * For application access use TeamsAppInstallation.ManageSelectedForTeam.All

Gets one or all apps in a channel.

## SYNTAX

```powershell
Get-PnPTeamsChannelApp -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> [-Identity <TeamsChannelAppPipeBind>]
  
```

## DESCRIPTION

Allows to retrieve apps in channel. By using `Identity` it is possible to retrieve a specific single app.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsChannelApp -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype
```

Retrieves the apps for the specified Microsoft Teams instance and channel

### EXAMPLE 2
```powershell
Get-PnPTeamsChannelApp -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity "SharePoint"
```

Retrieves an app with the display name 'SharePoint' from the specified team and channel

### EXAMPLE 3
```powershell
Get-PnPTeamsChannelApp -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity 2a527703-1f6f-4559-a332-d8a7d288cd88
```

Retrieves an app with an id 2a527703-1f6f-4559-a332-d8a7d288cd88 from the specified team and channel

### EXAMPLE 4
```powershell
Get-PnPTeamsChannelApp -Team "My Team" -Channel "My Channel"
```

Retrieves the tabs for the specified Microsoft Teams instance and channel

### EXAMPLE 5
```powershell
Get-PnPTeamsChannelApp -Team "My Team" -Channel "My Channel" -Identity "SharePoint"
```

Retrieves an app with the display name 'SharePoint' from the specified team and channel

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
Specify the id or display name of the app

```yaml
Type: TeamsChannelAppPipeBind
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

