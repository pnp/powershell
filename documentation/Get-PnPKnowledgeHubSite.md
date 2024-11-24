---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPKnowledgeHubSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPKnowledgeHubSite
---

# Get-PnPKnowledgeHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets the Knowledge Hub Site URL for your tenant

## SYNTAX

### Default (Default)

```
Get-PnPKnowledgeHubSite [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPKnowledgeHubSite
```

Returns the Knowledge Hub Site Url for your tenant

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPKnowledgeHubSite
```

Returns the Knowledge Hub Site Url for your tenant

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

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
