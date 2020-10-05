---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpteamstab
schema: 2.0.0
title: Get-PnPTeamsTab
---

# Get-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets one or all tabs in a channel.

## SYNTAX

```
Get-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> [-Identity <TeamsTabPipeBind>]
 [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

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
Get-PnPTeamsTab -Team "My Team" -Channel "My Channel"
```

Retrieves the tabs for the specified Microsoft Teams instance and channel

### EXAMPLE 4
```powershell
Get-PnPTeamsTab "My Team" -Channel "My Channel" -Identity "Wiki"
```

Retrieves a tab with the display name 'Wiki' from the specified team and channel

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
Specify the channel id of the team to retrieve.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Identity
Identity

```yaml
Type: TeamsTabPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Team
Specify the group id of the team to retrieve.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)