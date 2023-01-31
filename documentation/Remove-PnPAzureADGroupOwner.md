---
Module Name: PnP.PowerShell
title: Remove-PnPAzureADGroupOwner
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADGroupOwner.html
---
 
# Remove-PnPAzureADGroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes owners from a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Remove-PnPAzureADGroupOwner -Identity <AzureADGroupPipeBind> -Users <String[]> [-Verbose]
```

## DESCRIPTION

Allows to remove owners from Azure Active Directory group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPAzureADGroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Removes the provided two users as owners from the Azure Active Directory group named "Project Team".

## PARAMETERS

### -Identity
The Identity of the Azure Active Directory group to remove owners from.

```yaml
Type: AzureADGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Users
The UPN(s) of the user(s) to remove as owners from the Azure Active Directory group.

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
[Microsoft Graph documentation](https://docs.microsoft.com/graph/api/group-delete-owners)
