---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpteamsapp
schema: 2.0.0
title: Remove-PnPTeamsApp
---

# Remove-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: AppCatalog.ReadWrite.All

Removes an app from the Teams AppCatalog.

## SYNTAX

```
Remove-PnPTeamsApp -Identity <TeamsAppPipeBind> [-Force] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsApp -Identity ac139d8b-fa2b-4ffe-88b3-f0b30158b58b
```

Adds a new channel to the specified Teams instance

### EXAMPLE 2
```powershell
Remove-PnPTeamsApp -Identity "My Teams App"
```

Adds a new channel to the specified Teams instance

### EXAMPLE 3
```powershell
Add-PnPTeamsChannel -Team MyTeam -DisplayName "My Channel" -Private
```

Adds a new private channel to the specified Teams instance

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
The id, externalid or display name of the app.

```yaml
Type: TeamsAppPipeBind
Parameter Sets: (All)
Aliases:

Required: True
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