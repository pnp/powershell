---
Module Name: PnP.PowerShell
title: Remove-PnPTenantTheme
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantTheme.html
---
 
# Remove-PnPTenantTheme

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a theme.

## SYNTAX

```powershell
Remove-PnPTenantTheme [-Identity] <ThemePipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION
Removes the specified theme from the tenant configuration.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTenantTheme -Name "MyCompanyTheme"
```

Removes the specified theme.

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
The name of the theme to remove.

```yaml
Type: ThemePipeBind
Parameter Sets: (All)
Aliases: Name

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

