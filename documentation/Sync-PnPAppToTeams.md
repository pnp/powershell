---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Sync-PnPAppToTeams.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Sync-PnPAppToTeams
---

# Sync-PnPAppToTeams

## SYNOPSIS

Synchronize an app from the tenant app catalog to the Microsoft Teams app catalog.

## SYNTAX

### Default (Default)

```
Sync-PnPAppToTeams [-Identity] <AppMetadataPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to synchronize an app from the tenant app catalog to the Microsoft Teams app catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Sync-PnPAppToTeams -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will synchronize the given app with the Microsoft Teams app catalog.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

Specifies the Id of the Add-In Instance.

```yaml
Type: AppMetadataPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
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
