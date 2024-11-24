---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPPropertyBagValue.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPPropertyBagValue
---

# Set-PnPPropertyBagValue

## SYNOPSIS

Adds a new or updates an existing property bag value.

## SYNTAX

### Web

```
Set-PnPPropertyBagValue -Key <String> -Value <String> [-Indexed] [-Connection <PnPConnection>]
```

### Folder

```
Set-PnPPropertyBagValue -Key <String> -Value <String> [-Folder <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Updates an existing property bag value or adds it as a new key\value pair if it doesn't exist yet. If working with a modern SharePoint Online site or having noscript enabled, you will have to disable this yourself temporarily using `Set-PnPTenantSite -Url <url> -NoScriptSite:$false` to be able to make the change.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPPropertyBagValue -Key MyKey -Value MyValue
```

This sets or adds a value to the current web property bag.

### EXAMPLE 2

```powershell
Set-PnPPropertyBagValue -Key MyKey -Value MyValue -Folder /
```

This sets or adds a value to the root folder of the current web.

### EXAMPLE 3

```powershell
Set-PnPPropertyBagValue -Key MyKey -Value MyValue -Folder /MyFolder
```

This sets or adds a value to the folder MyFolder which is located in the root folder of the current web.

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
- Name: Folder
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Indexed

Sets the key to be indexed, which makes the property bag value searchable.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Web
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Key

Key of the property to set.

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

### -Value

Value to set.

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
