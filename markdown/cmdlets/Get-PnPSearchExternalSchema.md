---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchExternalSchema.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSearchExternalSchema
---
  
# Get-PnPSearchExternalSchema

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, ExternalConnection.Read.All,	ExternalConnection.ReadWrite.All

Retrieves the schema set on a connection to an external datasource belonging to Microsoft Search

## SYNTAX

```powershell
Get-PnPSearchExternalSchema -ConnectionId <SearchExternalConnectionPipeBind> [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to retrieve the current schema set on a connection to an external datasource that is being indexed into Microsoft Search through a custom connector.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchExternalSchema -ConnectionId "pnppowershell"
```

This will return the current schema being used on the external Microsoft Search connection with the specified identity.

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

### -ConnectionId
Unique identifier or instance of the external connection in Microsoft Search to retrieve the schema for

```yaml
Type: String
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