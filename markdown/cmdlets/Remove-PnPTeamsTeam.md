---
Module Name: PnP.PowerShell
title: Remove-PnPTeamsTeam
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsTeam.html
---
 
# Remove-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes a Microsoft Teams Team instance and its corresponding Microsoft 365 Group

## SYNTAX

```powershell
Remove-PnPTeamsTeam -Identity <TeamsTeamPipeBind> [-Force]  
```

## DESCRIPTION

Removes a Microsoft Teams Team. This also removes the associated Microsoft 365 Group, and is functionally identical to `Remove-PnPMicrosoft365Group`

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsTeam -Identity 5beb63c5-0571-499e-94d5-3279fdd9b6b5
```

Removes the specified Team

### EXAMPLE 2
```powershell
Remove-PnPTeamsTeam -Identity testteam
```

Removes the specified Team. If there are multiple teams with the same display name it will not proceed deleting the team.

## PARAMETERS

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Specify the group id, mailNickname or display name of the team to remove.

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

