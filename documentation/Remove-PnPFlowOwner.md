---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPFlowOwner.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPFlowOwner
---

# Remove-PnPFlowOwner

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Removes owner permissions to a Power Automate flow

## SYNTAX

### Default (Default)

```
Remove-PnPFlowOwner -Identity <PowerPlatformPipeBind> -User <String>
 [-Environment <PowerAutomateEnvironmentPipeBind>] [-AsAdmin] [-Force] [-Verbose]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes owner permissions for a user from a Power Automate flow.

## EXAMPLES

### Example 1

```powershell
Remove-PnPFlowOwner -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com
```
Removes the specified user from the specified Power Automate flow located in the default environment

### Example 2

```powershell
Remove-PnPFlowOwner -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User 6844c04a-8ee7-40ad-af66-28f6e948cd04
```
Removes the specified user from the specified Power Automate flow located in the default environment

### Example 3

```powershell
Remove-PnPFlowOwner (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -AsAdmin
```
Removes the specified user from the specified Power Automate flow as an admin in the specified environment

### Example 4

```powershell
Remove-PnPFlowOwner (Get-PnPPowerPlatformEnvironment -Identity "myenvironment) -Identity f07c34a9-a586-4e58-91fb-e7ea19741b61 -User username@tenant.onmicrosoft.com -AsAdmin -Force
```
Removes the specified user from the specified Power Automate Flow as admin, without asking for confirmation, in the specified environment

## PARAMETERS

### -AsAdmin

If specified, the permission will be removed as an admin. If not specified only the flows to which the current user already has access can be modified.

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

### -Force

Providing the Force parameter will skip the confirmation question.

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

### -User

The user principal name or Id of the user to remove its permissions from the Power Automate Flow.

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
