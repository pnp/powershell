---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerConfiguration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPlannerConfiguration
---

# Get-PnPPlannerConfiguration

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Returns the Microsoft Planner configuration of the tenant

## SYNTAX

### Default (Default)

```
Get-PnPPlannerConfiguration [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns the Microsoft Planner admin configuration of the tenant. Note that after changing the configuration using `Set-PnPPlannerTenantConfiguration`, this cmdlet may return varying results which could deviate from your desired configuration while the new configuration is being propagated across the tenant.

## EXAMPLES

### Example 1

```powershell
Get-PnPPlannerConfiguration
```
Returns the Microsoft Planner configuration of the tenant

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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
