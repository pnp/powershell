---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSearchExternalItem.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPSearchExternalItem
---
  
# Remove-PnPSearchExternalItem

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalItem.ReadWrite.OwnedBy, ExternalItem.ReadWrite.All as a delegate or application permission

Removes an external item from an external connector in Microsoft Search

## SYNTAX

```powershell
Remove-PnPSearchExternalItem -ItemId <String> -ConnectionId <SearchExternalConnectionPipeBind> [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to remove a specific external item from a Microsoft Search custom connector. The item will be removed from the index and will no longer be available for search results.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSearchExternalItem -ConnectionId "pnppowershell" -ItemId "12345"
```

This will remove the external item with the identifier "12345" from the external Microsoft Search index connector named "pnppowershell".

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

### -ItemId
Unique identifier of the external item in Microsoft Search that you wish to remove.

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConnectionId
The Connection ID or connection instance of the custom connector to use. This is the ID that was entered when registering the custom connector and will indicate for which custom connector this external item is being removed from the Microsoft Search index.

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/externalconnectors-externalitem-delete)