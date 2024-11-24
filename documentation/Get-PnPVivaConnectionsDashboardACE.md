---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPVivaConnectionsDashboardACE.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPVivaConnectionsDashboardACE
---

# Get-PnPVivaConnectionsDashboardACE

## SYNOPSIS

Returns the Adaptive card extensions from the Viva connections dashboard page. This requires that you connect to a SharePoint Home site and have configured the Viva connections page.

## SYNTAX

### Default (Default)

```
Get-PnPVivaConnectionsDashboardACE [-Identity <VivaACEPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve the adaptive card extensions from the Viva connections dashboard page.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPVivaConnectionsDashboardACE
```

Returns all the adaptive card extensions present in the Viva Connections dashboard page.

### EXAMPLE 2

```powershell
Get-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef"
```

Returns the adaptive card extensions with specified Instance Id from the Viva Connections dashboard page.

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

The instance Id of the Adaptive Card extension present on the Viva connections dashboard page. This parameter takes either the Instance Id, the Id or the Title property. But as the latter two are not necessarily unique within the dashboard, the preferred value is to use the Instance Id of the ACE.

```yaml
Type: VivaACEPipeBind
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
