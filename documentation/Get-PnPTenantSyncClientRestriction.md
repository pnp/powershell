---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantSyncClientRestriction.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantSyncClientRestriction
---

# Get-PnPTenantSyncClientRestriction

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns organization-level OneDrive synchronization restriction settings

## SYNTAX

### Default (Default)

```
Get-PnPTenantSyncClientRestriction [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns organization-level OneDrive synchronization restriction properties such as BlockMacSync,
OptOutOfGrooveBlock, and TenantRestrictionEnabled.

Currently, there are no parameters for this cmdlet.

You must have the SharePoint Online admin or Global admin role to run the cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantSyncClientRestriction
```

This example returns all tenant OneDrive synchronization restriction settings

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
