---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantRestrictedSearchMode.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTenantRestrictedSearchMode
---

# Set-PnPTenantRestrictedSearchMode

## SYNOPSIS

**Required Permissions**

  *  Global Administrator or SharePoint Administrator

Returns Restricted Search mode.

## SYNTAX

### Default (Default)

```
Set-PnPTenantRestrictedSearchMode -Mode <RestrictedSearchMode> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns Restricted Search mode. Restricted SharePoint Search is disabled by default.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTenantRestrictedSearchMode -Mode Enabled
```

Sets or enables the Restricted Tenant Search mode for the tenant.

### EXAMPLE 2

```powershell
Set-PnPTenantRestrictedSearchMode -Mode Disabled
```

Disables the Restricted Tenant Search mode for the tenant.

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

### -Mode

Sets the mode for the Restricted Tenant Search.

```yaml
Type: RestrictedSearchMode
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Enabled
- Disabled
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
