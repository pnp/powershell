---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFlow.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFlow
---

# Get-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com
* Azure Service Management : user_impersonation
* Dynamics CRM : user_impersonation
* PowerApps Service : User
* Link to Required permissions reference : https://pnp.github.io/powershell/articles/determinepermissions.html#help-i-cant-figure-out-which-permissions-i-need

Returns Power Automate Flows

## SYNTAX

### All (Default)

```
Get-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-AsAdmin]
 [-SharingStatus <FlowSharingStatus>] [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

### By Identity

```
Get-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-AsAdmin]
 [-Identity <PowerPlatformPipeBind>] [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns Power Automate Flows meeting the specified criteria.

## EXAMPLES

### Example 1

```powershell
Get-PnPFlow -AsAdmin
```
Returns all the flows in the default Power Platform environment belonging to any user

### Example 2

```powershell
Get-PnPPowerPlatformEnvironment -Identity "MyOrganization (default)" | Get-PnPFlow
```
Returns all the flows for a given Power Platform environment belonging to the current user

### Example 3

```powershell
Get-PnPFlow -SharingStatus SharedWithMe
```
Returns all the flows which have been shared with the current user in the default Power Platform environment

### Example 4

```powershell
Get-PnPFlow -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
Returns a specific flow from the default Power Platform environment

## PARAMETERS

### -AsAdmin

If specified returns all the flows as admin. If not specified only the flows for the current user will be returned.

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

### -Identity

The Name/Id of the flow to retrieve.

```yaml
Type: PowerPlatformPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SharingStatus

Allows specifying the type of Power Automate Flows that should be returned. Valid values: All, SharedWithMe, Personal.

```yaml
Type: FlowSharingStatus
DefaultValue: All
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All
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
