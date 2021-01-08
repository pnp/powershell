---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/remove-pnpteamsapp
schema: 2.0.0
title: Remove-PnPTeamsApp
---

# Remove-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: AppCatalog.ReadWrite.All

Removes an app from the Teams AppCatalog.

## SYNTAX

```powershell
Remove-PnPTeamsApp -Identity <TeamsAppPipeBind> [-Force]  [<CommonParameters>]
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
The id, externalid or display name of the app.

```yaml
Type: TeamsAppPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)