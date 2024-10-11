---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchExternalConnection.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPSearchExternalConnection
---
  
# Set-PnPSearchExternalConnection

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, 	ExternalConnection.ReadWrite.All

Updates a connection to an external datasource for Microsoft Search

## SYNTAX

```powershell
Set-PnPSearchExternalConnection -Identity <SearchExternalConnectionPipeBind> [-Name <String>] [-Description <String>] [-AuthorizedAppIds <String[]>] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to update an external datasource connection that is being indexed into Microsoft Search through a custom connector. Use [New-PnPSearchExternalConnection](New-PnPSearchExternalConnection.md) to create a new connector.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSearchExternalConnection -Identity "pnppowershell" -Name "PnP PowerShell Rocks"
```

This will update just the name of the external connection with the provided identity to the value provided. The description will remain unchanged.

### EXAMPLE 2
```powershell
Set-PnPSearchExternalConnection -Identity "pnppowershell" -Name "PnP PowerShell Rocks" -Description "External content ingested using PnP PowerShell which rocks"
```

This will update the name and description of the external connection with the provided identity to the values provided.

### EXAMPLE 3
```powershell
Set-PnPSearchExternalConnection -Identity "pnppowershell" -AuthorizedAppIds "00000000-0000-0000-0000-000000000000","11111111-1111-1111-1111-111111111111"
```

This will replace the application registration identifiers of which the client Ids have been provided that can add items to the index for this connection.

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
Unique identifier or an instance of the external connection in Microsoft Search that needs to be updated.

```yaml
Type: SearchExternalConnectionPipeBind
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Name
The display name of the connection to be displayed in the Microsoft 365 admin center. Maximum length of 128 characters. Only provide when it needs to change.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Description of the connection displayed in the Microsoft 365 admin center. Only provide when it needs to change.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthorizedAppIds
The client Ids of the application registrations that are allowed to add items to the index for this connection. Only provide when it needs to change.

```yaml
Type: String[]
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