---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPDeletedFlow.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPDeletedFlow
---

# Get-PnPDeletedFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

**Information**

* To use this command, you must be a Global or Power Platform administrator.

**Note**

* A Power Automate flow is soft-deleted when:
* It's a non-solution flow.
* It's been deleted less than 21 days ago.

Returns all soft-deleted Power Automate flows within an environment

## SYNTAX

### All (Default)

```
Get-PnPDeletedFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-Connection <PnPConnection>]
 [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns Deleted Power Automate Flows meeting the specified criteria.

## EXAMPLES

### Example 1

```powershell
Get-PnPDeletedFlow
```
Returns all the deleted flows in the default Power Platform environment belonging to any user

### Example 2

```powershell
Get-PnPPowerPlatformEnvironment -Identity "MyOrganization (default)" | Get-PnPDeletedFlow
```
Returns all the deleted  flows for a given Power Platform environment belonging to the any user

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

### -Environment

The name of the Power Platform environment or an Environment instance. If omitted, the default environment will be used.

```yaml
Type: PowerPlatformEnvironmentPipeBind
DefaultValue: The default environment
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
