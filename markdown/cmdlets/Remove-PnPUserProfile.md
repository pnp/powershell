---
Module Name: PnP.PowerShell
title: Remove-PnPUserProfile
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPUserProfile.html
---
 
# Remove-PnPUserProfile

## SYNOPSIS
Removes a SharePoint User Profile from the tenant.

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

## SYNTAX

```powershell
Remove-PnPUserProfile -LoginName <String> 
```

## DESCRIPTION

Removes SharePoint User Profile data from the tenant.

> [!NOTE]
> The User must first be deleted from AAD before the user profile can be deleted. You can use the Azure AD cmdlet Remove-AzureADUser for this action.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPUserProfile -LoginName user@domain.com 
```

This removes user profile data with the email address user@domain.com.

## PARAMETERS

### -LoginName
Specifies the login name of the user to remove.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

