---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPDeletedTeam.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPDeletedTeam
---

# Get-PnPDeletedTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Team.ReadBasic.All

Gets list of deleted Teams teams.

## SYNTAX

### Default (Default)

```
Get-PnPDeletedTeam
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve a list of deleted Microsoft Teams teams

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPDeletedTeam
```

Retrieves all the deleted Microsoft Teams teams.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/teamwork-list-deletedteams)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
