---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADGroupMember.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAzureADGroupMember
---
  
# Get-PnPAzureADGroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All, User.Read.All, User.ReadWrite.All

Gets members of a particular Entra ID group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Get-PnPAzureADGroupMember -Identity <AzureADGroupPipeBind> [-Connection <PnPConnection>] [-Transitive]
```

## DESCRIPTION

Allows to list members from given Entra ID group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAzureADGroupMember -Identity $groupId
```

Retrieves all the direct members of a specific Entra ID group based on its ID.

### EXAMPLE 2
```powershell
Get-PnPAzureADGroupMember -Identity $group
```

Retrieves all the direct members of a specific Entra ID group based on the group's object instance.

### EXAMPLE 3
```powershell
Get-PnPAzureADGroupMember -Identity $group -Transitive
```

Retrieves all the direct and transitive members (members of groups inside groups) of a specific Entra ID group based on the group's object instance.

## PARAMETERS

### -Identity
The Identity of the Entra ID group.

```yaml
Type: AzureADGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

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

### -Transitive
If provided, the direct and transitive members (members of groups in the group) of a group will be returned. If not provided, only the members directly assigned to the group will be returned.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)