---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerPlatformEnvironment.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPowerPlatformEnvironment
---

# Get-PnPPowerPlatformEnvironment

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Retrieves the Microsoft Power Platform environments for the current tenant.

## SYNTAX

### Default (Default)

```
Get-PnPPowerPlatformEnvironment [-IsDefault] [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

### By Identity

```
Get-PnPPowerPlatformEnvironment -Identity <PowerPlatformEnvironmentPipeBind>
 [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet retrieves all of the Microsoft Power Platform environments for the current tenant

## EXAMPLES

### Example 1

```powershell
Get-PnPPowerPlatformEnvironment
```

This cmdlets returns all of the Power Platform environments for the current tenant.

### Example 2

```powershell
Get-PnPPowerPlatformEnvironment -IsDefault $true
```

This cmdlets returns the default Power Platform environment for the current tenant.

### Example 3

```powershell
Get-PnPPowerPlatformEnvironment -Identity "MyOrganization (default)"
```

This cmdlets returns the Power Platform environment with the provided display name for the current tenant.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

Allows specifying an environment display name or internal name to retrieve a specific environment.

```yaml
Type: bool
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Identity
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IsDefault

Allows retrieval of the default Power Platform environment by passing in `-IsDefault $true`. When passing in `-IsDefault $false` you will get all non default environments. If not provided at all, all available environments, both default and non-default, will be returned.

```yaml
Type: bool
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Default
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

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
