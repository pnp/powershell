---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchExternalItem.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSearchExternalItem
---
  
# Get-PnPSearchExternalItem

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ExternalItem.Read.All under a delegated context. Note that ExternalItem.ReadWrite.All will not work. Application context is not supported.

Returns the external items indexed for a specific connector in Microsoft Search

## SYNTAX

```powershell
Get-PnPSearchExternalItem -ConnectionId <SearchExternalConnectionPipeBind> [-Identity <String>] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to retrieve a list of indexed external items for a specific Microsoft Search external connector. The cmdlet will return all indexed external items for the specified connector. If you want to retrieve a specific external item, you can use the Identity parameter to specify the unique identifier of the external item. It uses a Microsoft Graph query in the background to retrieve the external items. This is why it will be unable to return the Access Control Lists (ACLs) information in the external items and the properties to contain more properties than you ingested yourself.

It is only possible to run this cmdlet under a delegated context, application context is not supported by the Microsoft Graph search API endpoint for this type of query.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchExternalItem -ConnectionId "pnppowershell" -ItemId "12345"
```

This will return the external item with the unique identifier "12345" for the custom connector with the Connection ID "pnppowershell".

### EXAMPLE 2
```powershell
Get-PnPSearchExternalItem -ConnectionId "pnppowershell"
```

This will return all external items for the custom connector with the Connection ID "pnppowershell".

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

### -Identity
Unique identifier of the external item in Microsoft Search. You can provide any identifier you want to retrieve or check for a specific item in the index. If you omit it, all external items for the specified connector will be returned.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConnectionId
The Connection ID or connection instance of the custom connector to use. This is the ID that was entered when registering the custom connector and will indicate for which custom connector the external items will be returned from the Microsoft Search index.

```yaml
Type: SearchExternalConnectionPipeBind
Parameter Sets: (All)
Required: True
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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/search-concept-custom-types#example-1-retrieve-items-using-azure-sql-built-in-connector)