---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSharePointAddIn.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSharePointAddIn
---

# Get-PnPSharePointAddIn

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the list of SharePoint addins installed in the site collection

## SYNTAX

### Default (Default)

```
Get-PnPSharePointAddIn [-IncludeSubsites <SwitchParameter>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSharePointAddIn
```

Returns the SharePoint addins installed in your site collection

### EXAMPLE 2
```powershell
Get-PnPSharePointAddIn -IncludeSubsites
```

Returns the SharePoint addins installed in your site collection as well as the subsites.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSharePointAddIn
```

Returns the SharePoint addins installed in your site collection

### EXAMPLE 2

```powershell
Get-PnPSharePointAddIn -IncludeSubsites
```

Returns the SharePoint addins installed in your site collection as well as the subsites.

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

When specified, it determines whether we should use also search the subsites of the connected site collection and lists the installed AddIns.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
