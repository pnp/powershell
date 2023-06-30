---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPAzureADGroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Clear-PnPAzureADGroupOwner
---
  
# Clear-PnPAzureADGroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes all current owners from a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Clear-PnPAzureADGroupOwner -Identity <AzureADGroupPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to remove all current owners from specified Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPAzureADGroupOwner -Identity "Project Team"
```

Removes all the current owners from the Azure Active Directory group named "Project Team".

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
The Identity of the Azure Active Directory group to remove all owners from.

```yaml
Type: AzureADGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-owners)