---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpunifiedgroupmembers
schema: 2.0.0
title: Get-PnPUnifiedGroupMembers
---

# Get-PnPUnifiedGroupMembers

## SYNOPSIS
Gets members of a particular Office 365 Group (aka Unified Group). Requires the Azure Active Directory application permissions 'Group.Read.All' and 'User.Read.All'.

## SYNTAX

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUnifiedGroupMembers -Identity $groupId
```

Retrieves all the members of a specific Office 365 Group based on its ID

### EXAMPLE 2
```powershell
Get-PnPUnifiedGroupMembers -Identity $group
```

Retrieves all the members of a specific Office 365 Group based on the group's object instance

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)