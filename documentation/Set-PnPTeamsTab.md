---
Module Name: PnP.PowerShell
title: Set-PnPTeamsTab
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsTab.html
---
 
# Set-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates Teams Tab settings

## SYNTAX

```powershell
Set-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> [-Identity <TeamsTabPipeBind>]
 [-DisplayName <String>]  
```

## DESCRIPTION

Allows to update Teams Tab settings.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsTab -Team "MyTeam" -Channel "My Channel" -Identity "Wiki" -DisplayName "Channel Wiki"
```

Updates the tab named 'Wiki' and changes the display name of the tab to 'Channel Wiki'

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

### -DisplayName
The new name of the tab.

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
Identity of the tab.

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

