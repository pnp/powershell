---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Test-PnPSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Test-PnPSite
---

# Test-PnPSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Checks the site collection and its contents.

## SYNTAX

### Default (Default)

```
Test-PnPSite -Identity <SitePipeBind> [-RuleId <Guid>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Test-PnPSite cmdlet runs one or all site collection health checks on the site collection and its contents. Tests are intended not to make any changes except in repair mode, which can be initiated by running the Repair-PnPSite cmdlet. This cmdlet reports the rules together with a summary of the results.

## EXAMPLES

### EXAMPLE 1

```powershell
Test-PnPSite -Identity "https://contoso.sharepoint.com/sites/marketing"
```

This example runs all the site collection health checks on the https://contoso.sharepoint.com/sites/marketing site collection.

### EXAMPLE 2

```powershell
Test-PnPSite -Identity "https://contoso.sharepoint.com/sites/marketing" -RuleID "ee967197-ccbe-4c00-88e4-e6fab81145e1"
```

This example runs the Missing Galleries Check rule in test mode on the https://contoso.sharepoint.com/sites/marketing site collection.

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

Specifies the SharePoint Online site collection on which to run the repairs.

```yaml
Type: SitePipeBind
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

### -RuleId

Specifies a health check rule to run.

For example:

* `"ee967197-ccbe-4c00-88e4-e6fab81145e1"` for Missing Galleries.
* `"befe203b-a8c0-48c2-b5f0-27c10f9e1622"` for Conflicting Content Types.
* `"a9a6769f-7289-4b9f-ae7f-5db4b997d284"` for Missing Parent Content Types.
* `"5258ccf5-e7d6-4df7-b8ae-12fcc0513ebd"` for Missing Site Templates.
* `"99c946f7-5751-417c-89d3-b9c8bb2d1f66"` for Unsupported Language Pack References.
* `"6da06aab-c539-4e0d-b111-b1da4408859a"` for Unsupported MUI References.

```yaml
Type: Guid
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
