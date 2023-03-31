---
Module Name: PnP.PowerShell
title: Remove-PnPExternalUser
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPExternalUser.html
---
 
# Remove-PnPExternalUser

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes one or more external users from the tenant.

## SYNTAX

```powershell
Remove-PnPExternalUser -UniqueIDs <String[]> [-Confirm] [-WhatIf]
```

## DESCRIPTION

The Remove-PnPExternalUser cmdlet permanently removes a collection of external users from the tenant.

Users who are removed lose access to all tenant resources.

## EXAMPLES

### EXAMPLE 1
```powershell
$user = Get-PnPExternalUser -Filter someone@example.com
Remove-PnPExternalUser -UniqueIDs @($user.UniqueId)
```

This example removes a specific external user who has the address "someone@example.com". Organization members may still see the external user name displayed in the Shared With dialog, but the external user will not be able to sign in and will not be able to access any tenant resources.

## PARAMETERS

### -UniqueIDs

Specifies an ID that can be used to identify an external user based on their Windows Live ID.

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

