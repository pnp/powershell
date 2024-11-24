---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPBuiltInDesignPackageVisibility.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPBuiltInDesignPackageVisibility
---

# Get-PnPBuiltInDesignPackageVisibility

## SYNOPSIS

Gets the visibility of the available built-in Design Packages.

## SYNTAX

### Default (Default)

```
Get-PnPBuiltInDesignPackageVisibility [-DesignPackage <DesignPackageType>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Use this cmdlet to retrieve the current visibility state of each built-in design package.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPBuiltInDesignPackageVisibility -DesignPackage Showcase
```

This example retrieves the current visibility state of Showcase built-in design package.

### EXAMPLE 2

```powershell
Get-PnPBuiltInDesignPackageVisibility
```

This example retrieves the current visibility state of each built-in design package.

## PARAMETERS

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
