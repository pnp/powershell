---
Module Name: PnP.PowerShell
title: Set-PnPTeamsTag
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsTag.html
---
 
# Set-PnPTeamsTag

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: TeamworkTag.ReadWrite, Group.Read.All

Sets the Microsoft Teams tag in a Team.

## SYNTAX

```powershell
Set-PnPTeamsTag -Team <TeamsTeamPipeBind> -Identity <TeamsTagPipeBind> -DisplayName <String>
```

## DESCRIPTION

Allows to set a Teams tag in Microsoft Teams.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Identity "ZmY1ZjdmMjctZDhiNy00MWRkLTk2ZDQtYzcyYmVhMWIwOGYxIyM3ZTVhNGRmZS1kNWNlLTRkOTAtODM4MC04ZDIxM2FkYzYzOGIjI3RiVlVpR01rcg==" -DisplayName "Updated Tag"
```
Sets the tag with the specified Id from the Teams team.

## PARAMETERS

### -DisplayName
The updated display name of the Teams tag.

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
Specify the id of the Tag.

```yaml
Type: TeamsTagPipeBind
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

