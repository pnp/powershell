---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEntraIDGroupMember.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPEntraIDGroupMember
---
  
# Get-PnPEntraIDGroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All, User.Read.All, User.ReadWrite.All

Gets members of a particular Entra ID group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Get-PnPEntraIDGroupMember -Identity <EntraIDGroupPipeBind> [-Transitive] [-Select <String[]>] [-UseBeta] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to list members from given Entra ID group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEntraIDGroupMember -Identity $groupId
```

Retrieves all the direct members of a specific Entra ID group based on its ID.

### EXAMPLE 2
```powershell
Get-PnPEntraIDGroupMember -Identity $group
```

Retrieves all the direct members of a specific Entra ID group based on the group's object instance.

### EXAMPLE 3
```powershell
Get-PnPEntraIDGroupMember -Identity $group -Transitive
```

Retrieves all the direct and transitive members (members of groups inside groups) of a specific Entra ID group based on the group's object instance.

### EXAMPLE 4
```powershell
Get-PnPEntraIDGroupMember -Identity $group -Select department,city
```

Retrieves all the direct members of a specific Entra ID group including the requested properties. Properties that are not part of the default output are available through the AdditionalProperties property.

### EXAMPLE 5
```powershell
Get-PnPEntraIDGroupMember -Identity $group -UseBeta
```

Retrieves all the direct members of a specific Entra ID group using the Microsoft Graph beta endpoint, which returns all available properties of each member. Properties that are not part of the default output are available through the AdditionalProperties property.

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
The Identity of the Entra ID group.

```yaml
Type: EntraIDGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Select
Specifies additional properties to retrieve from Microsoft Graph for each member, i.e. department, city, accountEnabled. The properties that are part of the output by default, such as UserType, UsageLocation and ProxyAddresses, are always retrieved. Properties that are not part of the default output are available through the AdditionalProperties property of each member, i.e. $member.AdditionalProperties.department.

```yaml
Type: String[]
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

### -UseBeta
Uses the Microsoft Graph beta endpoint instead of the v1.0 endpoint.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)