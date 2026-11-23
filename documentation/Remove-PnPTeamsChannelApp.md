---
Module Name: PnP.PowerShell
title: Remove-PnPTeamsChannelApp
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsChannelApp.html
---
 
# Remove-PnPTeamsChannelApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: TeamsAppInstallation.ManageSelectedForTeam
  * For application access, use TeamsAppInstallation.ManageSelectedForTeam.All

Removes a Microsoft Teams app in a shared channel. This operation is allowed only for channels with a membershipType value of shared.

## SYNTAX

```powershell
Remove-PnPTeamsChannelApp -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -Identity <TeamsChannelAppPipeBind>
 [-Force]  
```

## DESCRIPTION

Allows to remove a app from shared channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsChannelApp -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel "SharedTest" -Identity SharePoint
```
Removes the app with the display name 'SharePoint' from the SharedTest shared channel using display name.

### EXAMPLE 2
```powershell
Remove-PnPTeamsChannelApp -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity SharePoint
```

Removes the app with the display name 'SharePoint' from the shared channel using id.

### EXAMPLE 3
```powershell
Remove-PnPTeamsChannelApp -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity 2a527703-1f6f-4559-a332-d8a7d288cd88
```

Removes the app with the specified id from the channel

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
Specify the id of the app 

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

