---
Module Name: PnP.PowerShell
title: Revoke-PnPUserSession
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Revoke-PnPUserSession.html
---
 
# Revoke-PnPUserSession

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Provides IT administrators the ability to logout a user's O365 sessions across all their devices.

## SYNTAX

```powershell
Revoke-PnPUserSession -User <String>    
    [-Confirm]
```

## DESCRIPTION

User will be signed out of browser, desktop and mobile applications accessing Office 365 resources across all devices.

It is not applicable to guest users.

## EXAMPLES

### EXAMPLE 1
```powershell
Revoke-PnPUserSession -User user1@contoso.com
```

This example signs out user1 in the contoso tenancy from all devices.

## PARAMETERS

### -User
Specifies a user name. For example, user1@contoso.com

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

