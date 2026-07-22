---
Module Name: PnP.PowerShell
title: Remove-PnPEntraIDGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPEntraIDGroup.html
---
 
# Remove-PnPEntraIDGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes one Entra ID group. This can be a security or Microsoft 365 group. Distribution lists are not currently supported by Graph API.

## SYNTAX

```powershell
Remove-PnPEntraIDGroup -Identity <EntraIDGroupPipeBind>  
```

## DESCRIPTION

Allows to remove Entra ID group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPEntraIDGroup -Identity $groupId
```

Removes an Entra ID group based on its ID

### EXAMPLE 2
```powershell
Remove-PnPEntraIDGroup -Identity $group
```

Removes the provided Entra ID group

## PARAMETERS

### -Identity
The Identity of the Entra ID group

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