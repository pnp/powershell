---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTeamsApp
---

# Remove-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: AppCatalog.ReadWrite.All

Removes an app from the Teams AppCatalog.

## SYNTAX

### Default (Default)

```
Remove-PnPTeamsApp -Identity <TeamsAppPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The id, external id or display name of the app.

```yaml
Type: TeamsAppPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
