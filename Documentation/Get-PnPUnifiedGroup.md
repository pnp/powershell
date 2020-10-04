---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpunifiedgroup
schema: 2.0.0
title: Get-PnPUnifiedGroup
---

# Get-PnPUnifiedGroup

## SYNOPSIS
Gets one Office 365 Group (aka Unified Group) or a list of Office 365 Groups. Requires the Azure Active Directory application permission 'Group.Read.All'.

## SYNTAX

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUnifiedGroup
```

Retrieves all the Office 365 Groups

### EXAMPLE 2
```powershell
Get-PnPUnifiedGroup -Identity $groupId
```

Retrieves a specific Office 365 Group based on its ID

### EXAMPLE 3
```powershell
Get-PnPUnifiedGroup -Identity $groupDisplayName
```

Retrieves a specific or list of Office 365 Groups that start with the given DisplayName

### EXAMPLE 4
```powershell
Get-PnPUnifiedGroup -Identity $groupSiteMailNickName
```

Retrieves a specific or list of Office 365 Groups for which the email starts with the provided mail nickName

### EXAMPLE 5
```powershell
Get-PnPUnifiedGroup -Identity $group
```

Retrieves a specific Office 365 Group based on its object instance

### EXAMPLE 6
```powershell
Get-PnPUnifiedGroup -IncludeIfHasTeam
```

Retrieves all the Office 365 Groups and checks for each of them if it has a Microsoft Team provisioned for it

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)