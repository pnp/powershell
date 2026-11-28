---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsChannelApp.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTeamsChannelApp
---
  
# Add-PnPTeamsChannelApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: TeamsAppInstallation.ManageSelectedForTeam
  * For application access, use TeamsAppInstallation.ManageSelectedForTeam.All

Adds an app to an existing shared Channel. This operation is allowed only for channels with a membershipType value of shared.

## SYNTAX

```powershell
Add-PnPTeamsChannelApp -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -App <String> 
```

## DESCRIPTION

Allows to add an app to an existing shared Channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsChannelApp -Team "My Team" -Channel "My Channel" -App "2a527703-1f6f-4559-a332-d8a7d288cd88"
```

Adds the SharePoint app to the specified shared channel.

## PARAMETERS

### -Channel
Specify the channel id or name of the team to retrieve.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -App
Specify the app

```yaml
Type: String
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
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
