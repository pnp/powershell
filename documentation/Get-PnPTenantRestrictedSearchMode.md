---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantRestrictedSearchMode.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantRestrictedSearchMode
---

# Get-PnPTenantRestrictedSearchMode

## SYNOPSIS

**Required Permissions**

  *  Global Administrator or SharePoint Administrator

Returns Restricted Search mode.

## SYNTAX

### Default (Default)

```
Get-PnPTenantRestrictedSearchMode [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns Restricted Search mode. Restricted SharePoint Search is disabled by default.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantRestrictedSearchMode
```

Returns Restricted Search mode.

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
