---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPHideDefaultThemes.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPHideDefaultThemes
---

# Get-PnPHideDefaultThemes

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns if the default / OOTB themes should be visible to users or not.

## SYNTAX

### Default (Default)

```
Get-PnPHideDefaultThemes [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns if the default themes are visible. Use Set-PnPHideDefaultThemes to change this value.

You must be a SharePoint Online global administrator to run the cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPHideDefaultThemes
```

This example returns the current setting if the default themes should be visible

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
