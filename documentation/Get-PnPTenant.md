---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnptenant
schema: 2.0.0
title: Get-PnPTenant
---

# Get-PnPTenant

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns organization-level site collection properties

## SYNTAX

```powershell
Get-PnPTenant [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Returns organization-level site collection properties such as StorageQuota, StorageQuotaAllocated, ResourceQuota,
ResourceQuotaAllocated, and SiteCreationMode.

Currently, there are no parameters for this cmdlet.

You must have the SharePoint Online admin or Global admin role to run the cmdlet.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)