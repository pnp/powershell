---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpteamsteam
schema: 2.0.0
title: Get-PnPTeamsTeam
---

# Get-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets one Microsoft Teams Team or a list of Teams.

## SYNTAX

```powershell
Get-PnPTeamsTeam [-Identity <TeamsTeamPipeBind>]  [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsTeam
```

Retrieves all the Microsoft Teams instances

### EXAMPLE 2
```powershell
Get-PnPTeamsTeam -Identity $groupId
```

Retrieves a specific Microsoft Teams instance

## PARAMETERS

### -Identity
Specify the group id of the team to retrieve.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)