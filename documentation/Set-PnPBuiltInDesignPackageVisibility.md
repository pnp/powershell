---
Module Name: PnP.PowerShell
title: Set-PnPBuiltInDesignPackageVisibility
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPBuiltInDesignPackageVisibility.html
---
 
# Set-PnPBuiltInDesignPackageVisibility

## SYNOPSIS
Sets the visibility of the available built-in Design Packages at the moment of site creation.

## SYNTAX

```powershell
Set-PnPBuiltInDesignPackageVisibility [-IsVisible] <Boolean> [-DesignPackage] <DesignPackageType>
 
```

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

### -IsVisible
Sets the visibility of the design package.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

