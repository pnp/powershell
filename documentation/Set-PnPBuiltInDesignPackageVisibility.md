---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPBuiltInDesignPackageVisibility.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPBuiltInDesignPackageVisibility
---

# Set-PnPBuiltInDesignPackageVisibility

## SYNOPSIS

Sets the visibility of the available built-in Design Packages at the moment of site creation.

## SYNTAX

### Default (Default)

```
Set-PnPBuiltInDesignPackageVisibility [-IsVisible] <Boolean> [-DesignPackage] <DesignPackageType>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets the visibility of the available built-in Design Packages.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPBuiltInDesignPackageVisibility -DesignPackage Showcase -IsVisible:$false
```

This example sets the visibility state of Showcase built-in design package to false.

### EXAMPLE 2

```powershell
Set-PnPBuiltInDesignPackageVisibility -DesignPackage TeamSite -IsVisible:$true
```

This example sets the visibility state of TeamSite design package to true.

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

### -DesignPackage

Name of the design package, available names are

* Topic
* Showcase
* Blank
* TeamSite

```yaml
Type: DesignPackageType
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

### -IsVisible

Sets the visibility of the design package.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
