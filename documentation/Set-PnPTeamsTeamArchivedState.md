---
Module Name: PnP.PowerShell
title: Set-PnPTeamsTeamArchivedState
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsTeamArchivedState.html
---
 
# Set-PnPTeamsTeamArchivedState

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Sets the archived state of a team.

## SYNTAX

```powershell
Set-PnPTeamsTeamArchivedState -Identity <TeamsTeamPipeBind> -Archived <Boolean>
 [-SetSiteReadOnlyForMembers <Boolean>]  
```

## DESCRIPTION

Allows to update the archived state of a team.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsTeamArchivedState -Identity "My Team" -Archived $true
```

Archives the team as identified.

### EXAMPLE 2
```powershell
Set-PnPTeamsTeamArchivedState -Identity "My Team" -Archived $false
```

Un-archives the team as identified.

### EXAMPLE 3
```powershell
Set-PnPTeamsTeamArchivedState -Identity "My Team" -Archived $true -SetSiteReadOnlyForMembers $true
```

Archives the team as identified and sets the underlying SharePoint Online Site Collection as read only for members

## PARAMETERS

### -Archived
Setting that determines whether the Team is archived.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
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

### -SetSiteReadOnlyForMembers
Setting that determines whether the connected SharePoint site is set to read only.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

