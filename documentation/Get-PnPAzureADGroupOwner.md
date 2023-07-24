---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADGroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAzureADGroupOwner
---
  
# Get-PnPAzureADGroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All, User.Read.All, User.ReadWrite.All

Gets owners of a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Get-PnPAzureADGroupOwner -Identity <AzureADGroupPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to list owners from a given Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAzureADGroupOwner -Identity $groupId
```

Retrieves all the owners of a specific Azure Active Directory group based on its ID.

### EXAMPLE 2
```powershell
Get-PnPAzureADGroupOwner -Identity $group
```

Retrieves all the owners of a specific Azure Active Directory group based on the group's object instance.

## PARAMETERS

### -Identity
The identity of the Azure Active Directory group.

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