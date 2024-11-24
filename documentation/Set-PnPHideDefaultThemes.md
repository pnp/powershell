---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPHideDefaultThemes.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPHideDefaultThemes
---

# Set-PnPHideDefaultThemes

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Defines if the default out of the box themes should be visible to users or not.

## SYNTAX

### Default (Default)

```
Set-PnPHideDefaultThemes -HideDefaultThemes <Boolean> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Use this cmdlet to hide or show the default themes to users as an option to change the look of their site.

You must be a SharePoint Online Administrator to run the cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPHideDefaultThemes -HideDefaultThemes $true
```

The out of the box themes will be hidden.

### EXAMPLE 2

```powershell
Set-PnPHideDefaultThemes -HideDefaultThemes $false
```

The out of the box themes will be shown.

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

### -HideDefaultThemes

Defines if the default themes should be visible or hidden.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
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
