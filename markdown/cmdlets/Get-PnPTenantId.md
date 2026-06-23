---
Module Name: PnP.PowerShell
title: Get-PnPTenantId
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantId.html
---
 
# Get-PnPTenantId

## SYNOPSIS
Returns the Tenant ID

## SYNTAX

### By TenantUrl
```powershell
Get-PnPTenantId -TenantUrl <String> [-AzureEnvironment <AzureEnvironment>]
```

### By connection
```powershell
Get-PnPTenantId [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to retrieve id of tenant. This does not require an active connection to that tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantId
```

Returns the current Tenant Id. A valid connection with Connect-PnPOnline is required either as a current connection or by providing it using the -Connection parameter.

### EXAMPLE 2
```powershell
Get-PnPTenantId contoso
```

Returns the Tenant ID for the tenant contoso.sharepoint.com. Can be executed without an active PnP Connection.

### EXAMPLE 3
```powershell
Get-PnPTenantId -TenantUrl contoso.sharepoint.com
```

Returns the Tenant ID for the specified tenant. Can be executed without an active PnP Connection.

### EXAMPLE 4
```powershell
Get-PnPTenantId -TenantUrl contoso.sharepoint.us -AzureEnvironment USGovernment
```

Returns the Tenant ID for the specified US Government tenant. Can be executed without an active PnP Connection.

## PARAMETERS

### -TenantUrl
The name of the tenant to retrieve the id for. If not specified, the currently connected to tenant will be used. You can use either just the tenant name, i.e. contoso or the full SharePoint URL, i.e. contoso.sharepoint.com.

```yaml
Type: String
Parameter Sets: By URL

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -AzureEnvironment
The Azure environment to use for the tenant lookup. It defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
Parameter Sets: By URL
Accepted values: Production, PPE, China, Germany, USGovernment, USGovernmentHigh, USGovernmentDoD

Required: False
Position: Named
Default value: Production
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection. If not specified, the current connection will be used.

```yaml
Type: PnPConnection
Parameter Sets: From connection

Required: False
Position: Named
Default value: Current connection
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)