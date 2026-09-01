---
Module Name: PnP.PowerShell
title: Remove-PnPEntraIDGroupOwner
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPEntraIDGroupOwner.html
---
 
# Remove-PnPEntraIDGroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes owners from a particular Entra ID group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Remove-PnPEntraIDGroupOwner -Identity <EntraIDGroupPipeBind> -Users <String[]> [-Verbose]
```

```powershell
Remove-PnPEntraIDGroupOwner -Identity <EntraIDGroupPipeBind> -MemberObjectId <Guid[]> [-Verbose]
```

## DESCRIPTION

Allows to remove owners from Entra ID group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPEntraIDGroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Removes the provided two users as owners from the Entra ID group named "Project Team".

### EXAMPLE 2
```powershell
# Remove an owner by ObjectId
Remove-PnPEntraIDGroupOwner -Identity $groupId -MemberObjectId $ownerObjectId
```

Removes the owner (user or group) with ObjectId `$ownerObjectId` from the group identified by `$groupId`.

### EXAMPLE 3
```powershell
# Pipeline by property name (Id)
Get-PnPEntraIDGroupOwner -Identity $groupId | Where-Object { $_.Id -eq $ownerObjectId } | Remove-PnPEntraIDGroupOwner -Identity $groupId
```

Pipes an owner whose `Id` matches `$ownerObjectId` into the cmdlet and removes it.

## PARAMETERS

### -Identity
The Identity of the Entra ID group to remove owners from.

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
The UPN(s) of the user(s) to remove as owners from the Entra ID group.

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
The ObjectId(s) of directory object(s) (Users or Groups) to remove from the Entra ID group as owners. Use this to remove owners that do not have a UPN.

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-owners)
