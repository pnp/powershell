---
Module Name: PnP.PowerShell
title: Remove-PnPTeamsApp
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsApp.html
---
 
# Remove-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: AppCatalog.ReadWrite.All

Removes an app from the Teams AppCatalog.

## SYNTAX

```powershell
Remove-PnPTeamsApp -Identity <TeamsAppPipeBind> [-Force]  
```

## DESCRIPTION

Allows to remove an app from the Teams AppCatalog.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsApp -Identity ac139d8b-fa2b-4ffe-88b3-f0b30158b58b
```

Removes an app from the Teams AppCatalog by using the id.

### EXAMPLE 2
```powershell
Remove-PnPTeamsApp -Identity "My Teams App"
```

Removes the app "My teams App" from the Teams AppCatalog by using display name.


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
The id, external id or display name of the app.

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

