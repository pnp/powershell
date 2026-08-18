---
Module Name: PnP.PowerShell
title: Get-PnPTeamsChannelFilesFolder
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelFilesFolder.html
---
 
# Get-PnPTeamsChannelFilesFolder

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Group.Read.All

Gets the metadata for the location where the files of a channel are stored.

## SYNTAX

```powershell
Get-PnPTeamsChannel [-Team <TeamsTeamPipeBind>] [-Channel <TeamsChannelPipeBind>] 
 
```

## DESCRIPTION

Allows to retrieve folder metadata for specified channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsChannelFilesFolder -Team "Sales Team" -Channel "Test Channel"
```

Retrieves the folder metadata for the channel called 'Test Channel' located in the Team named 'Sales Team'

### EXAMPLE 2
```powershell
Get-PnPTeamsChannelFilesFolder -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Channel "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype"
```

Retrieves the folder metadata for the channel specified by its channel id

## PARAMETERS

### -Channel
The id or name of the channel to retrieve.

```yaml
Type: TeamsChannelPipeBind
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

