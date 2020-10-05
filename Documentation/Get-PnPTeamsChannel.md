---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpteamschannel
schema: 2.0.0
title: Get-PnPTeamsChannel
---

# Get-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets the channels for a specified Team.

## SYNTAX

```
Get-PnPTeamsChannel -Team <TeamsTeamPipeBind> [-Identity <TeamsChannelPipeBind>] [-ByPassPermissionCheck]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsChannel -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8
```

Retrieves all channels for the specified team

### EXAMPLE 2
```powershell
Get-PnPTeamsChannel -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Identity "Test Channel"
```

Retrieves the channel called 'Test Channel'

### EXAMPLE 3
```powershell
Get-PnPTeamsChannel -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Identity "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype"
```

Retrieves the channel specified by its channel id

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

### -Identity
The identity of the channel to retrieve.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)
Aliases:

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
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)