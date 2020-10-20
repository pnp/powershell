---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpmicrosoft365groupmembers
schema: 2.0.0
title: Get-PnPMicrosoft365GroupMembers
---

# Get-PnPMicrosoft365GroupMembers

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All, User.Read.All, User.ReadWrite.All

Gets members of a particular Microsoft 365 Group (aka Unified Group). Requires the Azure Active Directory application permissions 'Group.Read.All' and 'User.Read.All'.

## SYNTAX

```powershell
Get-PnPMicrosoft365GroupMembers -Identity <Microsoft365GroupPipeBind> [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365GroupMembers -Identity $groupId
```

Retrieves all the members of a specific Microsoft 365 Group based on its ID

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365GroupMembers -Identity $group
```

Retrieves all the members of a specific Microsoft 365 Group based on the group's object instance

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group

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