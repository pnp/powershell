---
Module Name: PnP.PowerShell
schema: 2.0.0
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPTeamsTag
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsTag.html
---
  
# Get-PnPTeamsTag

## SYNOPSIS

**Required Permissions**

* Microsoft Graph API: One of TeamworkTag.Read or TeamworkTag.ReadWrite (delegated), or one of TeamworkTag.Read.All or TeamworkTag.ReadWrite.All (application). Additionally Group.Read.All when -Team is supplied as a mail nickname or display name rather than as a group id, as resolving those reads the group from Microsoft Graph.

Gets one or all tags in a team.

## SYNTAX

```powershell
Get-PnPTeamsTag -Team <TeamsTeamPipeBind> [-Identity <string>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5
```

Retrieves all the tags for the specified Microsoft Teams instance.

### EXAMPLE 2

```powershell
Get-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Identity "ZmY1ZjdmMjctZDhiNy00MWRkLTk2ZDQtYzcyYmVhMWIwOGYxIyM3ZTVhNGRmZS1kNWNlLTRkOTAtODM4MC04ZDIxM2FkYzYzOGIjI3RiVlVpR01rcg=="
```

Retrieves a tag with the specified Id from the specified team.

## PARAMETERS

### -Identity

Specify the id of the tag

```yaml
Type: TeamsTagPipeBind
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

