---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPPowerAppByPassConsent.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPPowerAppByPassConsent
---

# Set-PnPPowerAppByPassConsent

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Sets the consent bypass flag of a Power Apps for a given environment

## SYNTAX

### Default (Default)

```
Set-PnPPowerAppByPassConsent -Identity <PowerAppPipeBind> -ByPassConsent <Boolean>
 [-Environment <PowerPlatformEnvironmentPipeBind>] [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command can be used to set the bypassConsent flag of an PowerApps to true or false. Set the value as true so users aren't required to authorize API connections for the targeted app. To Remove the consent set the value false so users are required to authorize API connections for the targeted app

## EXAMPLES

### Example 1

```powershell
Set-PnPPowerAppByPassConsent -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -ByPassConsent true
```
This sets the bypassConsent flag on the specified Power App in the provided environment to true

### Example 2

```powershell
Set-PnPPowerAppByPassConsent -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -ByPassConsent false
```
This sets the bypassConsent flag on the specified Power App in the default environment

## PARAMETERS

### -ByPassConsent

The value to set for the bypassConsent flag of the app.

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

The Id of the app to retrieve.

```yaml
Type: PowerAppPipeBind
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

- [Set-AdminPowerAppApisToBypassConsent](https://learn.microsoft.com/powershell/module/microsoft.powerapps.administration.powershell/set-adminpowerappapistobypassconsent)
- [Clear-AdminPowerAppApisToBypassConsent](https://learn.microsoft.com/powershell/module/microsoft.powerapps.administration.powershell/clear-adminpowerappapistobypassconsent)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
