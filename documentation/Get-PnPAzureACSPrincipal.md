---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureACSPrincipal.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureACSPrincipal
---

# Get-PnPAzureACSPrincipal

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the list of Azure ACS Principals

## SYNTAX

### Default (Default)

```
Get-PnPAzureACSPrincipal [-Scope <AzureACSPrincipalScope>] [-IncludeSubsites <SwitchParameter>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet provides insight into the app registrations that have been done over time using AppRegNew.aspx. You could look at it as a (detailed) equivallent of AppPrincipals.aspx. This information can help to get insights into the app registrations that have been done on a tenant and prepare for the Azure Access Control Services (ACS) deprecation that will follow in the future.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureACSPrincipal
```

Returns the Azure ACS principals

### EXAMPLE 2

```powershell
Get-PnPAzureACSPrincipal -IncludeSubsites
```

Returns the lists of Azure ACS principals installed in your site collection as well as the subsites. The ValidUntil property will not be populated in this scenario.

### EXAMPLE 3

```powershell
Get-PnPAzureACSPrincipal -Scope Tenant
```

Returns the lists of Azure ACS principals installed in your Tenant. This a very heavy operation, so it might take some time before we get the results.

### EXAMPLE 4

```powershell
Get-PnPAzureACSPrincipal -Scope All -IncludeSubsites
```

Returns the lists of all Azure ACS principals installed in your Tenant including subsites. This a very heavy operation, so it might take some time before we get the results.

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

### -IncludeSubsites

When specified, it determines whether we should use also search the subsites of the connected site collection and lists the Azure ACS principals.

```yaml
Type: SwitchParameter
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

### -Scope

When specified, it determines the scope of the Azure ACS principals.
Supported values are `List, Web, Site, Tenant , All`.
Only with `Tenant` and `All` the `ValidUntil` property will be populated. For the other options it will not, this is by design and makes the cmdlet perform faster.

```yaml
Type: Enum (AzureACSPrincipalScope)
DefaultValue: List
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
