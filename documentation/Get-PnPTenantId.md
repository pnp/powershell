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
Get-PnPTenantId -TenantUrl <String> [<CommonParameters>]
```

### By connection
```powershell
Get-PnPTenantId [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantId
```

Returns the current Tenant Id. A valid connection with Connect-PnPOnline is required either as a current connection or by providing it using the -Connection parameter.

### EXAMPLE 2
```powershell
Get-PnPTenantId -TenantUrl "https://contoso.sharepoint.com"
```

Returns the Tenant ID for the specified tenant. Can be executed without an active PnP Connection.

## PARAMETERS

### -TenantUrl

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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