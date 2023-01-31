---
Module Name: PnP.PowerShell
title: Remove-PnPMicrosoft365GroupOwner
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365GroupOwner.html
---
 
# Remove-PnPMicrosoft365GroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes owners from a particular Microsoft 365 Group

## SYNTAX

```powershell
Remove-PnPMicrosoft365GroupOwner -Identity <Microsoft365GroupPipeBind> -Users <String[]>
  [<CommonParameters>]
```

## DESCRIPTION

Allows to remove owners from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPMicrosoft365GroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Removes the provided two users as owners from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group to remove owners from

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Users
The UPN(s) of the user(s) to remove as owners from the Microsoft 365 Group

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-owners)