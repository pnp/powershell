---
Module Name: PnP.PowerShell
title: Set-PnPTenantRestrictedSearchMode
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantRestrictedSearchMode.html
---
 
# Set-PnPTenantRestrictedSearchMode

## SYNOPSIS

**Required Permissions**

  *  Global Administrator or SharePoint Administrator 

Returns Restricted Search mode.

## SYNTAX

```powershell
Set-PnPTenantRestrictedSearchMode -Mode <RestrictedSearchMode> [-Connection <PnPConnection>]
```

## DESCRIPTION

Returns Restricted Search mode. Restricted SharePoint Search is disabled by default.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTenantRestrictedSearchMode -Mode Enabled
```

Sets or enables the Restricted Tenant Search mode for the tenant.

### EXAMPLE 2

```powershell
Set-PnPTenantRestrictedSearchMode -Mode Disabled
```

Disables the Restricted Tenant Search mode for the tenant.

## PARAMETERS

### -Mode

Sets the mode for the Restricted Tenant Search.

```yaml
Type: RestrictedSearchMode
Parameter Sets: (All)
Accepted values: Enabled, Disabled

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
