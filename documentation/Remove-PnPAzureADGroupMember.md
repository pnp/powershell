---
Module Name: PnP.PowerShell
title: Remove-PnPAzureADGroupMember
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADGroupMember.html
---
 
# Remove-PnPAzureADGroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All, GroupMember.ReadWrite.All

Removes members from a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Remove-PnPAzureADGroupMember -Identity <AzureADGroupPipeBind> -Users <String[]>
```

```powershell
Remove-PnPAzureADGroupMember -Identity <AzureADGroupPipeBind> -MemberObjectId <Guid[]>
```

## DESCRIPTION

Allows to remove members from Azure Active Directory group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPAzureADGroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Removes the provided two users as members from the Azure Active Directory group named "Project Team"

### EXAMPLE 2
```powershell
# Remove a nested group by its ObjectId
Remove-PnPAzureADGroupMember -Identity $parentGroupId -MemberObjectId $childGroupId
```

Removes the group with ObjectId `$childGroupId` from the group identified by `$parentGroupId`.

### EXAMPLE 3
```powershell
# Pipeline by property name (Id)
Get-PnPAzureADGroupMember -Identity $parentGroupId | Where-Object { $_.Id -eq $childGroupId } | Remove-PnPAzureADGroupMember -Identity $parentGroupId
```

Pipes a member (group or user) whose `Id` matches `$childGroupId` into the cmdlet and removes it.

## PARAMETERS

### -Identity
The Identity of the Azure Active Directory group to remove members from

```yaml
Type: AzureADGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Users
The UPN(s) of the user(s) to remove as members from the Azure Active Directory group

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
The ObjectId(s) of directory object(s) (Users or Groups) to remove from the Azure Active Directory group. Use this to remove nested groups that do not have a UPN.

```yaml
Type: Guid[]
Parameter Sets: MemberObjectId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-members)