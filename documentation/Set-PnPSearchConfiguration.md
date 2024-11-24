---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchConfiguration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSearchConfiguration
---

# Set-PnPSearchConfiguration

## SYNOPSIS

Sets the search configuration.

## SYNTAX

### Config

```
Set-PnPSearchConfiguration -Configuration <String> [-Scope <SearchConfigurationScope>]
 [-Connection <PnPConnection>]
```

### Path

```
Set-PnPSearchConfiguration -Path <String> [-Scope <SearchConfigurationScope>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet sets the search configuration for a single web, site collection or a tenant, using a file or a configuration string.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSearchConfiguration -Configuration $config
```

Sets the search configuration for the current web.

### EXAMPLE 2

```powershell
Set-PnPSearchConfiguration -Configuration $config -Scope Site
```

Sets the search configuration for the current site collection.

### EXAMPLE 3

```powershell
Set-PnPSearchConfiguration -Configuration $config -Scope Subscription
```

Sets the search configuration for the current tenant.

### EXAMPLE 4

```powershell
Set-PnPSearchConfiguration -Path searchconfig.xml -Scope Subscription
```

Reads the search configuration from the specified XML file and sets it for the current tenant.

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

Path to a search configuration.

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

Scope to apply the setting to. The default is Web.

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
