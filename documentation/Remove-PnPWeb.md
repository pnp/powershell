---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPWeb.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPWeb
---

# Remove-PnPWeb

## SYNOPSIS

Removes a subsite.

## SYNTAX

### Default (Default)

```
Remove-PnPWeb -Identity <WebPipeBind> [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes the specified subsite.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPWeb -Identity projectA
```

Removes the subsite called projectA and will ask for confirmation before doing so.

### EXAMPLE 2

```powershell
Remove-PnPWeb -Identity 5fecaf67-6b9e-4691-a0ff-518fc9839aa0
```

Removes the subsite with the provided Id and will ask for confirmation before doing so.

### EXAMPLE 3

```powershell
Get-PnPSubWeb | Remove-PnPWeb -Force
```

Removes all subsites while not asking for confirmation to do so.

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

Do not ask for confirmation to delete the subweb.

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

The identifier of a subsite, the subsite instance or name of the subsite.

```yaml
Type: WebPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
