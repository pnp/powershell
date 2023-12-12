---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADGroup.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAzureADGroup
---
  
# Get-PnPAzureADGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets one Azure Active Directory group or a list of Azure Active Directory groups. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Get-PnPAzureADGroup [-Identity <AzureADGroupPipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to retrieve a list of Azure Active Directory groups. Those can be a security, distribution or Microsoft 365 group. By specifying `Identity` option it is possible to get a single group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAzureADGroup
```

Retrieves all the Azure Active Directory groups.

### EXAMPLE 2
```powershell
Get-PnPAzureADGroup -Identity $groupId
```

Retrieves a specific Azure Active Directory group based on its ID.

### EXAMPLE 3
```powershell
Get-PnPAzureADGroup -Identity $groupDisplayName
```

Retrieves a specific Azure Active Directory group that has the given DisplayName.

### EXAMPLE 4
```powershell
Get-PnPAzureADGroup -Identity $groupSiteMailNickName
```

Retrieves a specific Azure Active Directory group for which the email address equals the provided mail nickName.

### EXAMPLE 5
```powershell
Get-PnPAzureADGroup -Identity $group
```

Retrieves a specific Azure Active Directory group based on its group object instance.

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
The identity of the Azure Active Directory group. Either specify an id, a display name, email address, or a group object.

```yaml
Type: AzureADGroupPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
