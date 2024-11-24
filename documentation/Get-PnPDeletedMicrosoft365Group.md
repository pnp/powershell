---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPDeletedMicrosoft365Group.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPDeletedMicrosoft365Group
---

# Get-PnPDeletedMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets one deleted Microsoft 365 Group or a list of deleted Microsoft 365 Groups

## SYNTAX

### Default (Default)

```
Get-PnPDeletedMicrosoft365Group [-Identity <Microsoft365GroupPipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to get list of deleted Microsoft 365 Groups. Use the `Identity` option to specify the exact group.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPDeletedMicrosoft365Group
```

Retrieves all deleted Microsoft 365 Groups

### EXAMPLE 2

```powershell
Get-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
```

Retrieves a specific deleted Microsoft 365 Group based on its ID

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
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
