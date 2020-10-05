---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpteamschannel
schema: 2.0.0
title: Remove-PnPTeamsChannel
---

# Remove-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes a channel from a Microsoft Teams instance.

## SYNTAX

```
Remove-PnPTeamsChannel -Team <TeamsTeamPipeBind> -Identity <TeamsChannelPipeBind> [-Force]
 [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsChannel -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -DisplayName "My Channel"
```

Removes the channel specified from the team specified

## PARAMETERS

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

### -Force
Specifying the Force parameter will skip the confirmation question.

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

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Team

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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)