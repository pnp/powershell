---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPMicrosoft365GroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPMicrosoft365GroupOwner
---
  
# Add-PnPMicrosoft365GroupOwner

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: All of Group.ReadWrite.All, User.ReadWrite.All

Adds users to a Microsoft 365 Group as Owners.

## SYNTAX

```powershell
Add-PnPMicrosoft365GroupOwner -Identity <Microsoft365GroupPipeBind> -Users <String[]> [-RemoveExisting] [<CommonParameters>]
```

## DESCRIPTION

Allows to add multiple users to Microsoft 365 Group as owners.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPMicrosoft365GroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Adds the provided two users as additional owners to the Microsoft 365 Group named "Project Team"

### EXAMPLE 2
```powershell
Add-PnPMicrosoft365GroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com" -RemoveExisting
```

Sets the provided two users as the only owners of the Microsoft 365 Group named "Project Team" by removing any current existing members first

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group to add owners to

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -RemoveExisting
If provided, all existing owners will be removed and only those provided through Users will become owners

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Users
The UPN(s) of the user(s) to add to the Microsoft 365 Group as an owner

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-post-members)
