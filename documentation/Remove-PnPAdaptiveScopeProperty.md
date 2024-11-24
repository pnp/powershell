---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPAdaptiveScopeProperty.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPAdaptiveScopeProperty
---

# Remove-PnPAdaptiveScopeProperty

## SYNOPSIS

Removes a value from the current web property bag

## SYNTAX

### Default (Default)

```
Remove-PnPAdaptiveScopeProperty [-Key] <String> [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet is used to remove a property bag value. Executing this cmdlet removes a value from the current web property bag just like  `Remove-PnPPropertyBagValue` would do, but also takes care of toggling the noscript value to allow for this to be possible in one cmdlet. Using this cmdlet does therefore require having the SharePoint Online Admin role or equivalent app permissions.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPAdaptiveScopeProperty -Key MyKey
```

This will remove the value with key MyKey from the current web property bag

### EXAMPLE 2

```powershell
Remove-PnPAdaptiveScopeProperty -Key MyKey -Force
```

This will remove the value with key MyKey from the current web property bag without prompting for confirmation

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

Key of the property bag value to be removed

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
- [Microsoft 365 Information Governance](https://learn.microsoft.com/en-us/microsoft-365/compliance/manage-information-governance?view=o365-worldwide)
- [Adaptive policy scopes](https://learn.microsoft.com/en-us/microsoft-365/compliance/retention?view=o365-worldwide#adaptive-or-static-policy-scopes-for-retention)
