---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPListDesign.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPListDesign
---

# Get-PnPListDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieve List Designs that have been registered on the current tenant.

## SYNTAX

### Default (Default)

```
Get-PnPListDesign [[-Identity] <TenantListDesignPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Retrieve List Designs that have been registered on the current tenant. When providing a name with -Identity, it returns all list designs with that name.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPListDesign
```

Returns all registered list designs

### EXAMPLE 2

```powershell
Get-PnPListDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```
Returns a specific registered list design by id

### EXAMPLE 3

```powershell
Get-PnPListDesign -Identity ListEvent
```

Returns a specific registered list design by name

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

If specified, it will retrieve the specified list design

```yaml
Type: TenantListDesignPipeBind
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
