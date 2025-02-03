---
Module Name: PnP.PowerShell
title: Get-PnPTenant
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenant.html
---
 
# Get-PnPTenant

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns organization-level site collection properties

## SYNTAX

```powershell
Get-PnPTenant [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns organization-level site collection properties such as StorageQuota, StorageQuotaAllocated, ResourceQuota,
ResourceQuotaAllocated, and SiteCreationMode.

If one or more properties cannot be retrieved, such as when a property is not available yet on the tenant due to a new feature rollout, a warning will be shown. Use -Verbose to see detailed information on properties that could not be retrieved.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenant
```

This example returns all tenant settings

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)