---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPAzureADUser
---
  
# Remove-PnPAzureADUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: User.ReadWrite.All

Removes a user from Azure Active Directory / Microsoft Entra ID.

## SYNTAX

```powershell
Remove-PnPAzureADUser -Identity <AzureADUserPipeBind> [-WhatIf] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION

Allows a user to be removed from Azure Active Directory / Microsoft Entra ID. When the user is deleted, the user will be moved to the recycle bin and can be restored within 30 days. After 30 days the user will be permanently deleted.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPAzureADUser -Identity johndoe@contoso.onmicrosoft.com
```

The user with the provided UPN will be removed from Azure Active Directory.

### EXAMPLE 2
```powershell
Remove-PnPAzureADUser -Identity 5a4c547a-1440-4f64-9952-a0c6f1c9e7ea
```

The user with the provided guid will be removed from Azure Active Directory.

### EXAMPLE 3
```powershell
Get-PnPEntraIDUser | Where-Object { $_.OfficeLocation -eq "London" } | Remove-PnPAzureADUser
```

Removes all users that have their OfficeLocation set to London from Azure Active Directory.

### EXAMPLE 4
```powershell
Get-PnPEntraIDUser -Filter "accountEnabled eq false" | Remove-PnPAzureADUser
```

Removes all disabled user accounts from Azure Active Directory.

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
The identity of the user to remove. This can be the UPN, the GUID or an instance of the user.

```yaml
Type: AzureADUserPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
When used in combination with -Verbose, it will show what would happen if the cmdlet runs. The user will not be deleted.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)