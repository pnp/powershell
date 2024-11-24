---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPJavaScriptLink.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPJavaScriptLink
---

# Remove-PnPJavaScriptLink

## SYNOPSIS

Removes a JavaScript link or block from a web or sitecollection

## SYNTAX

### Default (Default)

```
Remove-PnPJavaScriptLink [[-Identity] <UserCustomActionPipeBind>] [-Force]
 [-Scope <CustomActionScope>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove JavaScript link or block from a web or sitecollection.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPJavaScriptLink -Identity jQuery
```

Removes the injected JavaScript file with the name jQuery from the current web after confirmation

### EXAMPLE 2

```powershell
Remove-PnPJavaScriptLink -Identity jQuery -Scope Site
```

Removes the injected JavaScript file with the name jQuery from the current site collection after confirmation

### EXAMPLE 3

```powershell
Remove-PnPJavaScriptLink -Identity jQuery -Scope Site -Confirm:$false
```

Removes the injected JavaScript file with the name jQuery from the current site collection and will not ask for confirmation

### EXAMPLE 4

```powershell
Remove-PnPJavaScriptLink -Scope Site
```

Removes all the injected JavaScript files from the current site collection after confirmation for each of them

### EXAMPLE 5

```powershell
Remove-PnPJavaScriptLink -Identity faea0ce2-f0c2-4d45-a4dc-73898f3c2f2e -Scope All
```

Removes the injected JavaScript file with id faea0ce2-f0c2-4d45-a4dc-73898f3c2f2e from both the Web and Site scopes

### EXAMPLE 6

```powershell
Get-PnPJavaScriptLink -Scope All | ? Sequence -gt 1000 | Remove-PnPJavaScriptLink
```

Removes all the injected JavaScript files from both the Web and Site scope that have a sequence number higher than 1000

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

Use the -Force flag to bypass the confirmation question

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

Name or id of the JavaScriptLink to remove. Omit if you want to remove all JavaScript Links.

```yaml
Type: UserCustomActionPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Key
- Name
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

Define if the JavaScriptLink is to be found at the web or site collection scope. Specify All to allow deletion from either web or site collection.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
