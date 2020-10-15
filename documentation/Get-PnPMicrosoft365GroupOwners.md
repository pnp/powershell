---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpmicrosoft365groupowners
schema: 2.0.0
title: Get-PnPMicrosoft365GroupOwners
---

# Get-PnPMicrosoft365GroupOwners

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All, User.Read.All, User.ReadWrite.All

Gets owners of a particular Microsoft 365 Group

## SYNTAX

```powershell
Get-PnPMicrosoft365GroupOwners -Identity <Microsoft365GroupPipeBind> [-ByPassPermissionCheck]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365GroupOwners -Identity $groupId
```

Retrieves all the owners of a specific Microsoft 365 Group based on its ID

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365GroupOwners -Identity $group
```

Retrieves all the owners of a specific Microsoft 365 Group based on the group's object instance

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Identity of the Microsoft 365 Group.

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)