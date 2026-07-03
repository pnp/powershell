---
Module Name: PnP.PowerShell
title: Set-PnPTeamsTeamPicture
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsTeamPicture.html
---
 
# Set-PnPTeamsTeamPicture

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Sets the picture of an existing team.

## SYNTAX

```powershell
Set-PnPTeamsTeamPicture -Team <TeamsTeamPipeBind> -Path <String>  [-Connection <PnPConnection>] 
```

## DESCRIPTION
Notice that this cmdlet will immediately return but it can take a few hours before the changes are reflected in Microsoft Teams.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTeamsTeamPicture -Team "MyTeam" -Path "c:\myimage.jpg"
```
Updates a picture for the team called 'MyTeam' with the available at "c:\myimage.jpg"

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path to the image file.

```yaml
Type: String
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
