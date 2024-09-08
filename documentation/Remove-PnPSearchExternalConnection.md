---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSearchExternalConnection.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPSearchExternalConnection
---
  
# Remove-PnPSearchExternalConnection

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, ExternalConnection.ReadWrite.All

Removes a specific connection to external datasources belonging to Microsoft Search

## SYNTAX

```powershell
Remove-PnPSearchExternalConnection -Identity <SearchExternalConnectionPipeBind> [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to remove a connection to an external datasource that is being indexed into Microsoft Search through a custom connector.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSearchExternalConnection -Identity "pnppowershell"
```

This will remove the connection to the external datasource with the specified identity from Microsoft Search.

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
Unique identifier or an instance of the external connection in Microsoft Search that needs to be removed.

```yaml
Type: SearchExternalConnectionPipeBind
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: True
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