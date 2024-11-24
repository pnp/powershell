---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantTheme.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTenantTheme
---

# Remove-PnPTenantTheme

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a theme.

## SYNTAX

### Default (Default)

```
Remove-PnPTenantTheme [-Identity] <ThemePipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes the specified theme from the tenant configuration.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTenantTheme -Name "MyCompanyTheme"
```

Removes the specified theme.

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

The name of the theme to remove.

```yaml
Type: ThemePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Name
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
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
