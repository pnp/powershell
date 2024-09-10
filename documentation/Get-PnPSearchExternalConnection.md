---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchExternalConnection.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSearchExternalConnection
---
  
# Get-PnPSearchExternalConnection

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, ExternalConnection.Read.All,	ExternalConnection.ReadWrite.All

Retrieves all connections to external datasources belonging to Microsoft Search

## SYNTAX

```powershell
Get-PnPSearchExternalConnection [-Identity <String>] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to retrieve all connections to external datasources that are being indexed into Microsoft Search through a custom connector. Use [Set-PnPSearchExternalItem](Set-PnPSearchExternalItem.md) to add items to the index for a connector.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchExternalConnection
```

This will return all connections to external datasources that are being indexed into Microsoft Search that exist within the tenant.

### EXAMPLE 2
```powershell
Get-PnPSearchExternalConnection -Identity "pnppowershell"
```

This will return the connection to the external datasource with the specified identity that is being indexed into Microsoft Search.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

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
Unique identifier of the external connection in Microsoft Search. If not provided, all connections will be returned.

```yaml
Type: String
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