---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPCustomAction.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPCustomAction
---

# Get-PnPCustomAction

## SYNOPSIS

Return user custom actions

## SYNTAX

### Default (Default)

```
Get-PnPCustomAction [-Identity <Guid>] [-Scope <CustomActionScope>]
 [-ThrowExceptionIfCustomActionNotFound] [-Connection <PnPConnection>] [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns all or a specific user custom action

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPCustomAction
```

Returns all custom actions of the current site.

### EXAMPLE 2

```powershell
Get-PnPCustomAction -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2
```

Returns the custom action with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2'.

### EXAMPLE 3

```powershell
Get-PnPCustomAction -Scope web
```

Returns all custom actions for the current web object.

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

Identity of the CustomAction to return. Omit to return all CustomActions.

```yaml
Type: Guid
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

### -Includes

Optionally allows properties to be retrieved for the returned custom action which are not included in the response by default

```yaml
Type: String[]
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

### -Scope

Scope of the CustomAction, either Web, Site or All to return both

```yaml
Type: CustomActionScope
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
- All
HelpMessage: ''
```

### -ThrowExceptionIfCustomActionNotFound

Switch parameter if an exception should be thrown if the requested CustomAction does not exist (true) or if omitted, nothing will be returned in case the CustomAction does not exist

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
