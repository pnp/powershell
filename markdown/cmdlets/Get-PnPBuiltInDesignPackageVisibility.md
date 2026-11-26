---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPBuiltInDesignPackageVisibility.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPBuiltInDesignPackageVisibility
---
  
# Get-PnPBuiltInDesignPackageVisibility

## SYNOPSIS
Gets the visibility of the available built-in Design Packages.

## SYNTAX

```powershell
Get-PnPBuiltInDesignPackageVisibility [-DesignPackage <DesignPackageType>]
 
```

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
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


