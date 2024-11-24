---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPPropertyBagValue.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPPropertyBagValue
---

# Remove-PnPPropertyBagValue

## SYNOPSIS

Removes a value from the property bag.

## SYNTAX

### Default (Default)

```
Remove-PnPPropertyBagValue [-Key] <String> [-Folder <String>] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes a value from the property bag. If working with a modern SharePoint Online site or having noscript enabled, you will have to disable this yourself temporarily using `Set-PnPTenantSite -Url <url> -NoScriptSite:$false` to be able to make the change.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPPropertyBagValue -Key MyKey
```

This will remove the value with key MyKey from the current web property bag.

### EXAMPLE 2

```powershell
Remove-PnPPropertyBagValue -Key MyKey -Folder /MyFolder
```

This will remove the value with key MyKey from the folder MyFolder which is located in the root folder of the current web.

### EXAMPLE 3

```powershell
Remove-PnPPropertyBagValue -Key MyKey -Folder /
```

This will remove the value with key MyKey from the root folder of the current web.

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

### -Folder

Site relative url of the folder. See examples for use.

```yaml
Type: String
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

If provided, no confirmation will be asked to remove the value from the property bag.

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

### -Key

Key of the property bag value to be removed.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
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
