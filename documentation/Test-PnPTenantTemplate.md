---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Test-PnPTenantTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Test-PnPTenantTemplate
---

# Test-PnPTenantTemplate

## SYNOPSIS

Tests a tenant template for invalid references.

## SYNTAX

### Default (Default)

```
Test-PnPTenantTemplate -Template <ProvisioningHierarchy> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to check if the tenant template has invalid references.

## EXAMPLES

### EXAMPLE 1

```powershell
Test-PnPTenantTemplate -Template $myTemplate
```

Checks for valid template references.

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

### -Template

The in-memory template to test.

```yaml
Type: ProvisioningHierarchy
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
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
