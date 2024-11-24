---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPDisableSpacesActivation.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPDisableSpacesActivation
---

# Set-PnPDisableSpacesActivation

## SYNOPSIS

Sets if SharePoint Spaces should be disabled.

## SYNTAX

### Default (Default)

```
Set-PnPDisableSpacesActivation -Disable <SwitchParameter> [-Scope <String>]
 [-Identity <SitePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet disables or enables SharePoint Spaces for a specific site collection or entire SharePoint tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPDisableSpacesActivation -Disable:$true -Scope Tenant
```

Disables SharePoint Spaces on the entire tenant.

### EXAMPLE 2

```powershell
Set-PnPDisableSpacesActivation -Disable -Scope Site -Identity "https://contoso.sharepoint.com"
```

Disables SharePoint Spaces on https://contoso.sharepoint.com

### EXAMPLE 3

```powershell
Set-PnPDisableSpacesActivation -Disable:$false -Scope Site -Identity "https://contoso.sharepoint.com"
```

Enables SharePoint Spaces on https://contoso.sharepoint.com

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

### -Disable

Sets if SharePoint Spaces should be enabled or disabled.

```yaml
Type: SwitchParameter
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

### -Identity

Specifies the URL of the SharePoint Site on which SharePoint Spaces should be disabled. Must be provided if Scope is set to Site.

```yaml
Type: SPOSitePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scope

Defines if SharePoint Spaces should be disabled for the entire tenant or for a specific site collection.

```yaml
Type: DisableSpacesScope
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
AcceptedValues:
- Tenant
- Site
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
