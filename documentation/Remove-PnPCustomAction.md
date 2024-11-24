---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPCustomAction.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPCustomAction
---

# Remove-PnPCustomAction

## SYNOPSIS

Removes a custom action.

## SYNTAX

### Default (Default)

```
Remove-PnPCustomAction [[-Identity] <UserCustomActionPipeBind>] [-Scope <CustomActionScope>]
 [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a custom action.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPCustomAction -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2
```

Removes the custom action with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2'.

### EXAMPLE 2

```powershell
Remove-PnPCustomAction -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2 -Scope web
```

Removes the custom action with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' from the current web.

### EXAMPLE 3

```powershell
Remove-PnPCustomAction -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2 -Force
```

Removes the custom action with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' without asking for confirmation.

### EXAMPLE 4

```powershell
Get-PnPCustomAction -Scope All | ? Location -eq ScriptLink | Remove-PnPCustomAction
```

Removes all custom actions that are ScriptLinks.

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

### -Force

Use the -Force flag to bypass the confirmation question.

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

The id or name of the CustomAction that needs to be removed or a CustomAction instance itself.

```yaml
Type: UserCustomActionPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scope

Define if the CustomAction is to be found at the web or site collection scope. Specify All to allow deletion from either web or site collection.

```yaml
Type: CustomActionScope
DefaultValue: Web
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
