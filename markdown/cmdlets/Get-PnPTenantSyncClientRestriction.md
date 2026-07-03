---
Module Name: PnP.PowerShell
title: Get-PnPTenantSyncClientRestriction
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantSyncClientRestriction.html
---
 
# Get-PnPTenantSyncClientRestriction

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns organization-level OneDrive synchronization restriction settings

## SYNTAX

```powershell
Get-PnPTenantSyncClientRestriction [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns organization-level OneDrive synchronization restriction properties such as BlockMacSync,
OptOutOfGrooveBlock, and TenantRestrictionEnabled.

Currently, there are no parameters for this cmdlet.

You must have the SharePoint Online admin or Global admin role to run the cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantSyncClientRestriction
```

This example returns all tenant OneDrive synchronization restriction settings

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

