---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPTeamsApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPTeamsApp
---

# New-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of AppCatalog.ReadWrite.All, Directory.ReadWrite.All

Adds an app to the Teams App Catalog.

## SYNTAX

### Default (Default)

```
New-PnPTeamsApp -Path <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add an app to the Teams App Catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPTeamsApp -Path c:\myapp.zip
```

Adds the app as defined in the zip file to the Teams App Catalog

## PARAMETERS

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
