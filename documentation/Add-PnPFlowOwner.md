---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFlowOwner.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFlowOwner
---

# Add-PnPFlowOwner

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Assigns/updates permissions to a Power Automate flow

## SYNTAX

### Default (Default)

```
Add-PnPFlowOwner -Identity <PowerPlatformPipeBind> -User <String> -Role <FlowAccessRole>
 [-Environment <PowerAutomateEnvironmentPipeBind>] [-AsAdmin] [-Verbose]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet assigns/updates permissions for a user to a Power Automate flow.

## EXAMPLES

### Example 1

```powershell
Add-PnPFlowOwner -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -Role CanEdit
```
Assigns the specified user with 'CanEdit' access level to the specified flow in the default environment

### Example 2

```powershell
Add-PnPFlowOwner -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04 -Role CanView
```
Assigns the specified user with 'CanView' access level to the specified flow in the default environment

### Example 3

```powershell
Add-PnPFlowOwner -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04 -Role CanViewWithShare
```
Assigns the specified user with 'CanViewWithShare' access level to the specified flow in the specified environment

### Example 4

```powershell
Add-PnPFlowOwner -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -AsAdmin -Role CanEdit
```
Assigns the specified user with 'CanEdit' access level to the specified flow as admin in the specified environment

## PARAMETERS

### -AsAdmin

If specified, the permission will be set as an admin. If not specified only the flows to which the current user already has access can be modified.

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

The Name, Id or instance of the Power Automate Flow to add the permissions to.

```yaml
Type: PowerPlatformPipeBind
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

### -Role

The type of permissions to assign to the user on the Power Automate Flow. Valid values: CanView, CanViewWithShare, CanEdit.

```yaml
Type: FlowUseFlowAccessRolerRoleName
DefaultValue: CanView
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

### -User

The user principal name or Id of the user to assign permissions to the Power Automate Flow.

```yaml
Type: String
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
