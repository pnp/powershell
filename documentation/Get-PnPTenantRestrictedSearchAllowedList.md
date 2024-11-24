---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantRestrictedSearchAllowedList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantRestrictedSearchAllowedList
---

# Get-PnPTenantRestrictedSearchAllowedList

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site and Copilot for M365 license

Retrieves existing list of URLs in the allowed list.

## SYNTAX

### Default (Default)

```
Get-PnPTenantRestrictedSearchAllowedList [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command will return all the existing list of URLs in the allowed list

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantRestrictedSearchAllowedList
```

Retrieves existing list of URLs in the allowed list

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
