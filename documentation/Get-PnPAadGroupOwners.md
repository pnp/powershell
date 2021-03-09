---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAadGroupOwners.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAadGroupOwners
---
  
# Get-PnPAadGroupOwners

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All, User.Read.All, User.ReadWrite.All

Gets owners of a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Get-PnPAadGroupOwners -Identity <AadGroupPipeBind> [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAadGroupOwners -Identity $groupId
```

Retrieves all the owners of a specific Azure Active Directory group based on its ID

### EXAMPLE 2
```powershell
Get-PnPAadGroupOwners -Identity $group
```

Retrieves all the owners of a specific Azure Active Directory group based on the group's object instance

## PARAMETERS

### -Identity
The Identity of the Azure Active Directory group.

```yaml
Type: AadGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)