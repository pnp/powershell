---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpexternaluser
schema: 2.0.0
title: Remove-PnPExternalUser
---

# Remove-PnPExternalUser

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes one ore more external users from the tenant.

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

### -Scope
Defines which app catalog to use. Defaults to Tenant

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: AppCatalogScope
Parameter Sets: (All)
Accepted values: Tenant, Site

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)