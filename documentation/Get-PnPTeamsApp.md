---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsApp
---

# Get-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of AppCatalog.Read.All, Directory.ReadWrite.All

Gets one Microsoft Teams App or a list of all apps.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsApp [-Identity <TeamsAppPipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve Microsoft Teams apps. By using `Identity` option it is possible to retrieve a specific app.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsApp
```

Retrieves all the Microsoft Teams Apps

### EXAMPLE 2

```powershell
Get-PnPTeamsApp -Identity a54224d7-608b-4839-bf74-1b68148e65d4
```

Retrieves a specific Microsoft Teams App

### EXAMPLE 3

```powershell
Get-PnPTeamsApp -Identity "MyTeamsApp"
```

Retrieves a specific Microsoft Teams App

## PARAMETERS

### -Identity

Specify the name, id or external id of the app.

```yaml
Type: TeamsAppPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
