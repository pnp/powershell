---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPEntraIDGroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Clear-PnPEntraIDGroupOwner
---
  
# Clear-PnPEntraIDGroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes all current owners from a particular Entra ID group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Clear-PnPEntraIDGroupOwner -Identity <EntraIDGroupPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to remove all current owners from specified Entra ID group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPEntraIDGroupOwner -Identity "Project Team"
```

Removes all the current owners from the Entra ID group named "Project Team".

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
The Identity of the Entra ID group to remove all owners from.

```yaml
Type: EntraIDGroupPipeBind
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