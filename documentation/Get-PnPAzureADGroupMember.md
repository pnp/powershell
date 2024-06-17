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

Gets members of a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Get-PnPAzureADGroupMember -Identity <AzureADGroupPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to list members from given Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAzureADGroupMember -Identity $groupId
```

Retrieves all the members of a specific Azure Active Directory group based on its ID.

### EXAMPLE 2
```powershell
Get-PnPAzureADGroupMember -Identity $group
```

Retrieves all the members of a specific Azure Active Directory group based on the group's object instance.

## PARAMETERS

### -Identity
The Identity of the Azure Active Directory group.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)