---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnpunifiedgroup
schema: 2.0.0
title: New-PnPUnifiedGroup
---

# New-PnPUnifiedGroup

## SYNOPSIS
Creates a new Office 365 Group (aka Unified Group). Requires the Azure Active Directory application permission 'Group.ReadWrite.All'.

## SYNTAX

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPUnifiedGroup -DisplayName $displayName -Description $description -MailNickname $nickname
```

Creates a public Office 365 Group with all the required properties

### EXAMPLE 2
```powershell
New-PnPUnifiedGroup -DisplayName $displayName -Description $description -MailNickname $nickname -Owners $arrayOfOwners -Members $arrayOfMembers
```

Creates a public Office 365 Group with all the required properties, and with a custom list of Owners and a custom list of Members

### EXAMPLE 3
```powershell
New-PnPUnifiedGroup -DisplayName $displayName -Description $description -MailNickname $nickname -IsPrivate
```

Creates a private Office 365 Group with all the required properties

### EXAMPLE 4
```powershell
New-PnPUnifiedGroup -DisplayName $displayName -Description $description -MailNickname $nickname -Owners $arrayOfOwners -Members $arrayOfMembers -IsPrivate
```

Creates a private Office 365 Group with all the required properties, and with a custom list of Owners and a custom list of Members

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)