---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPAvailablePageLayouts.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPAvailablePageLayouts
---

# Set-PnPAvailablePageLayouts

## SYNOPSIS

Sets the available page layouts for the current site.

## SYNTAX

### SPECIFIC

```
Set-PnPAvailablePageLayouts -PageLayouts <String[]> [-Connection <PnPConnection>]
```

### ALL

```
Set-PnPAvailablePageLayouts [-AllowAllPageLayouts] [-Connection <PnPConnection>]
```

### INHERIT

```
Set-PnPAvailablePageLayouts [-InheritPageLayouts] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet sets the available page layouts for the current site. It requires NoScript feature to be disabled.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPAvailablePageLayouts -AllowAllPageLayouts
```

Allows all page layouts for the current site.

## PARAMETERS

### -AllowAllPageLayouts

Allows all page layout files to be available for the site.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: ALL
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

### -InheritPageLayouts

Sets the available page layouts to inherit from the parent site.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: INHERIT
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PageLayouts

An array of page layout files to set as available page layouts for the site.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SPECIFIC
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
