---
Module Name: PnP.PowerShell
title: Get-PnPTeamsTeam
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsTeam.html
---
 
# Get-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets one Microsoft Teams Team or a list of Teams.

## SYNTAX

```powershell
Get-PnPTeamsTeam [-Identity <TeamsTeamPipeBind>] [-Filter <String>]  
```

## DESCRIPTION

Allows to retrieve list of Microsoft Teams teams. By using `Identity` it is possible to retrieve a specific team, and by using `Filter` you can supply any filter queries supported by the Graph API.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsTeam
```

Retrieves all the Microsoft Teams instances

### EXAMPLE 2
```powershell
Get-PnPTeamsTeam -Identity "PnP PowerShell"
```

Retrieves a specific Microsoft Teams instance using display name.

### EXAMPLE 3
```powershell
Get-PnPTeamsTeam -Identity "baba9192-55be-488a-9fb7-2e2e76edbef2"
```

Retrieves a specific Microsoft Teams instance using group id.

### EXAMPLE 4
```powershell
Get-PnPTeamsTeam -Filter "startswith(mailNickName, 'contoso')"
```

Retrieves all Microsoft Teams instances with MailNickName starting with "contoso". 

### EXAMPLE 5
```powershell
Get-PnPTeamsTeam -Filter "startswith(description, 'contoso')"
```

Retrieves all Microsoft Teams instances with Description starting with "contoso". This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/en-us/graph/aad-advanced-queries?tabs=http#group-properties).

## PARAMETERS

### -Identity
Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: Identity

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter
Specify the query to pass to Graph API in $filter.

```yaml
Type: String
Parameter Sets: Filter

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

