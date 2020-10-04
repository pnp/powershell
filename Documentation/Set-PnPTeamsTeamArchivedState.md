---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpteamsteamarchivedstate
schema: 2.0.0
title: Set-PnPTeamsTeamArchivedState
---

# Set-PnPTeamsTeamArchivedState

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Sets the archived state of a team.

## SYNTAX

```
Set-PnPTeamsTeamArchivedState -Identity <TeamsTeamPipeBind> -Archived <Boolean>
 [-SetSiteReadOnlyForMembers <Boolean>] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

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

Unarchives the team as identified.

### EXAMPLE 3
```powershell
Set-PnPTeamsTeamArchivedState -Identity "My Team" -Archived $true -SetSiteReadOnlyForMembers $true
```

Archives the team as identified and sets the underlying SharePoint Online Site Collection as read only for members

## PARAMETERS

### -Archived

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
Specify the name, id or external id of the app.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetSiteReadOnlyForMembers

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)