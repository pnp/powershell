---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEntraIDGroup.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPEntraIDGroup
---
  
# Get-PnPEntraIDGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets one Entra ID group or a list of Entra ID groups. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Get-PnPEntraIDGroup [-Identity <EntraIDGroupPipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to retrieve a list of Entra ID groups. Those can be a security, distribution or Microsoft 365 group. By specifying `Identity` option it is possible to get a single group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEntraIDGroup
```

Retrieves all the Entra ID groups.

### EXAMPLE 2
```powershell
Get-PnPEntraIDGroup -Identity $groupId
```

Retrieves a specific Entra ID group based on its ID.

### EXAMPLE 3
```powershell
Get-PnPEntraIDGroup -Identity $groupDisplayName
```

Retrieves a specific Entra ID group that has the given DisplayName.

### EXAMPLE 4
```powershell
Get-PnPEntraIDGroup -Identity $groupSiteMailNickName
```

Retrieves a specific Entra ID group for which the email address equals the provided mail nickName.

### EXAMPLE 5
```powershell
Get-PnPEntraIDGroup -Identity $group
```

Retrieves a specific Entra ID group based on its group object instance.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the Entra ID group. Either specify an id, a display name, email address, or a group object.

```yaml
Type: EntraIDGroupPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
