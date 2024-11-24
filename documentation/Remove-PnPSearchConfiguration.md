---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPSearchConfiguration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPSearchConfiguration
---

# Remove-PnPSearchConfiguration

## SYNOPSIS

Removes the search configuration.

## SYNTAX

### Config

```
Remove-PnPSearchConfiguration -Configuration <String> [-Scope <SearchConfigurationScope>]
 [-Connection <PnPConnection>]
```

### Path

```
Remove-PnPSearchConfiguration -Path <String> [-Scope <SearchConfigurationScope>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes the search configuration from a single web, site collection or a tenant, using path or a configuration string.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPSearchConfiguration -Configuration $config
```

Removes the search configuration for the current web (does not remove managed property mappings).

### EXAMPLE 2

```powershell
Remove-PnPSearchConfiguration -Configuration $config -Scope Site
```

Removes the search configuration for the current site collection (does not remove managed property mappings).

### EXAMPLE 3

```powershell
Remove-PnPSearchConfiguration -Configuration $config -Scope Subscription
```

Removes the search configuration for the current tenant (does not remove managed property mappings).

### EXAMPLE 4

```powershell
Remove-PnPSearchConfiguration -Path searchconfig.xml -Scope Subscription
```

Reads the search configuration from the specified XML file and removes it for the current tenant (does not remove managed property mappings).

## PARAMETERS

### -Configuration

Search configuration string.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Config
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

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

### -Path

Path to the search configuration.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Path
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scope

Scope to remove the configuration from. The default is Web.

```yaml
Type: SearchConfigurationScope
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
AcceptedValues:
- Web
- Site
- Subscription
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
