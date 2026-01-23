---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPEntraIDApp.html
schema: 2.0.0
applicable: SharePoint Online
title: Remove-PnPEntraIDApp
---

# Remove-PnPEntraIDApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.ReadWrite.All

Removes an Entra ID App registration.

## SYNTAX

```powershell
Remove-PnPEntraIDApp [-Identity] <EntraIDAppPipeBind> [-Force]
```

## DESCRIPTION
This cmdlet removes an Entra ID App registration.

## EXAMPLES

### Example 1
```powershell
Remove-PnPEntraIDApp -Identity MyApp
```

Removes the specified app.

### Example 2
```powershell
Remove-PnPEntraIDApp -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

Removes the specified app.

## PARAMETERS


### -Force
If specified the confirmation question will be skipped.

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
Specify the name, id or app id for the app to remove.

```yaml
Type: EntraIDAppPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
