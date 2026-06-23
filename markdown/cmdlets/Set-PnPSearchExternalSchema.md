---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchExternalSchema.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPSearchExternalSchema
---
  
# Set-PnPSearchExternalSchema

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, ExternalConnection.ReadWrite.All

Updates the schema set on a connection to an external datasource belonging to Microsoft Search

## SYNTAX

### By textual schema

```powershell
Set-PnPSearchExternalSchema -ConnectionId <SearchExternalConnectionPipeBind> -SchemaAsText <String> [-Verbose] [-Connection <PnPConnection>] 
```

### By schema instance

```powershell
Set-PnPSearchExternalSchema -ConnectionId <SearchExternalConnectionPipeBind> -Schema <Model.Graph.MicrosoftSearch.ExternalSchema> [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to initially set or update the current schema set on a connection to an external datasource that is being indexed into Microsoft Search through a custom connector. The URL returned can be queried in Microsoft Graph to check on the status of the schema update.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSearchExternalSchema -ConnectionId "pnppowershell" -SchemaAsText '{
   "baseType": "microsoft.graph.externalItem",
   "properties": [
     {
       "name": "ticketTitle",
       "type": "String",
       "isSearchable": "true",
       "isRetrievable": "true",
       "labels": [
         "title"
       ]
     },
     {
       "name": "priority",
       "type": "String",
       "isQueryable": "true",
       "isRetrievable": "true",
       "isSearchable": "false"
     },
     {
       "name": "assignee",
       "type": "String",
       "isRetrievable": "true"
     }
   ]
 }'
```

This will set the provided JSON schema to be used for the external search connection with the provided name

### EXAMPLE 2
```powershell
$schema = Get-PnPSearchExternalSchema -ConnectionId "pnppowershell1"
Set-PnPSearchExternalSchema -ConnectionId "pnppowershell2" -Schema $schema
```

This will take the current schema set on the external search connection named 'pnppowershell1' and sets the same schema on the external search connection named 'pnppowershell2'

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
Unique identifier or instance of the external connection in Microsoft Search to set the schema for

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -SchemaAsText
The textual representation of the schema to set on the external connection

```yaml
Type: String
Parameter Sets: By textual schema
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Schema
An instance of a schema to set on the external connection

```yaml
Type: String
Parameter Sets: By schema instance
Required: True
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