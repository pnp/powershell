---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Update-PnPTeamsApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Update-PnPTeamsApp
---

# Update-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing app in the Teams App Catalog.

## SYNTAX

### Default (Default)

```
Update-PnPTeamsApp -Identity <TeamsAppPipeBind> -Path <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update an existing app in the Teams App Catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Update-PnPTeamsApp -Identity 4efdf392-8225-4763-9e7f-4edeb7f721aa -Path c:\myapp.zip
```

Updates the specified app in the teams app catalog with the contents of the referenced zip file.

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
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

The path pointing to the packaged/zip file containing the app

```yaml
Type: String
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
