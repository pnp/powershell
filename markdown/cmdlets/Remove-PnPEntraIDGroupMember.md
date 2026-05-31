---
Module Name: PnP.PowerShell
title: Remove-PnPEntraIDGroupMember
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPEntraIDGroupMember.html
---
 
# Remove-PnPEntraIDGroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All, GroupMember.ReadWrite.All

Removes members from a particular Entra ID group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Remove-PnPEntraIDGroupMember -Identity <EntraIDGroupPipeBind> -Users <String[]>
```

```powershell
Remove-PnPEntraIDGroupMember -Identity <EntraIDGroupPipeBind> -MemberObjectId <Guid[]>
```

## DESCRIPTION

Allows to remove members from Entra ID group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPEntraIDGroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Removes the provided two users as members from the Entra ID group named "Project Team"

### EXAMPLE 2
```powershell
# Remove a nested group by its ObjectId
Remove-PnPEntraIDGroupMember -Identity $parentGroupId -MemberObjectId $childGroupId
```

Removes the group with ObjectId `$childGroupId` from the group identified by `$parentGroupId`.

### EXAMPLE 3
```powershell
# Pipeline by property name (Id)
Get-PnPEntraIDGroupMember -Identity $parentGroupId | Where-Object { $_.Id -eq $childGroupId } | Remove-PnPEntraIDGroupMember -Identity $parentGroupId
```

Pipes a member (group or user) whose `Id` matches `$childGroupId` into the cmdlet and removes it.

## PARAMETERS

### -Identity
The Identity of the Entra ID group to remove members from

```yaml
Type: EntraIDGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Users
The UPN(s) of the user(s) to remove as members from the Entra ID group

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MemberObjectId
The ObjectId(s) of directory object(s) (Users or Groups) to remove from the Entra ID group. Use this to remove nested groups that do not have a UPN.

```yaml
Type: Guid[]
Parameter Sets: ByObjectId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-members)