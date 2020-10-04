---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpunifiedgroup
schema: 2.0.0
title: Set-PnPUnifiedGroup
---

# Set-PnPUnifiedGroup

## SYNOPSIS
Sets Office 365 Group (aka Unified Group) properties. Requires the Azure Active Directory application permission 'Group.ReadWrite.All'.

## SYNTAX

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPUnifiedGroup -Identity $group -DisplayName "My Displayname"
```

Sets the display name of the group where $group is a Group entity

### EXAMPLE 2
```powershell
Set-PnPUnifiedGroup -Identity $groupId -Descriptions "My Description" -DisplayName "My DisplayName"
```

Sets the display name and description of a group based upon its ID

### EXAMPLE 3
```powershell
Set-PnPUnifiedGroup -Identity $group -GroupLogoPath ".\MyLogo.png"
```

Sets a specific Office 365 Group logo.

### EXAMPLE 4
```powershell
Set-PnPUnifiedGroup -Identity $group -IsPrivate:$false
```

Sets a group to be Public if previously Private.

### EXAMPLE 5
```powershell
Set-PnPUnifiedGroup -Identity $group -Owners demo@contoso.com
```

Sets demo@contoso.com as owner of the group.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)