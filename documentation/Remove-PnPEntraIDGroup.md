---
Module Name: PnP.PowerShell
title: Remove-PnPAzureADGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADGroup.html
---
 
# Remove-PnPAzureADGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes one Azure Active Directory group. This can be a security or Microsoft 365 group. Distribution lists are not currently supported by Graph API.

## SYNTAX

```powershell
Remove-PnPAzureADGroup -Identity <AzureADGroupPipeBind>  
```

## DESCRIPTION

Allows to remove Azure Active Directory group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPAzureADGroup -Identity $groupId
```

Removes an Azure Active Directory group based on its ID

### EXAMPLE 2
```powershell
Remove-PnPAzureADGroup -Identity $group
```

Removes the provided Azure Active Directory group

## PARAMETERS

### -Identity
The Identity of the Azure Active Directory group

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