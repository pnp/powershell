---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteScript.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteScript
---

# Get-PnPSiteScript

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieve Site Scripts that have been registered on the current tenant.

## SYNTAX

### Default (Default)

```
Get-PnPSiteScript [[-Identity] <TenantSiteScriptPipeBind>]
 [[-SiteDesign] <TenantSiteDesignPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve site scripts registered on the current tenant. By using `Identity` option it is possible to retrieve specified site script

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteScript
```

Returns all registered site scripts

### EXAMPLE 2

```powershell
Get-PnPSiteScript -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Returns a specific registered site script

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

If specified will retrieve the specified site script

```yaml
Type: TenantSiteScriptPipeBind
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
AcceptedValues: []
HelpMessage: ''
```

### -SiteDesign

If specified will retrieve the site scripts for this design

```yaml
Type: TenantSiteDesignPipeBind
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
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
