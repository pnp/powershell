---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchExternalItem.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPSearchExternalItem
---
  
# Set-PnPSearchExternalItem

## SYNOPSIS
Adds or updates an external item in Microsoft Search

## SYNTAX

```powershell
Set-PnPSearchExternalItem -Id <String> -ConnectionId <String> -Properties <Hashtable> [-ContentValue <String>] [-ContentType <SearchExternalItemContentType>] [-GrantUsers <AzureADUserPipeBind[]>] [-GrantGroups <AzureADGroupPipeBind[]>] [-DenyUsers <AzureADUserPipeBind[]>] [-DenyGroups <AzureADGroupPipeBind[]>] [-GrantExternalGroups <String[]>] [-DenyExternalGroups <String[]>] [-GrantEveryone <SwitchParameter>] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to add or update an external item in Microsoft Search on custom connectors. The cmdlet will create a new external item if the item does not exist yet. If the item already exists, it will be updated.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSearchExternalItem -ConnectionId "pnppowershell" -Id "12345" -Properties @{ "Test1"= "Test van deze PnP PowerShell Connector"; "Test2" = "Red","Blue"; "Test3" = ([System.DateTime]::Now)} -ContentValue "Sample value" -ContentType Text -GrantEveryone
```

This will add an item in the external Microsoft Search index with the properties as provided and grants everyone access to find the item back through Microsoft Search.

### EXAMPLE 2
```powershell
Set-PnPSearchExternalItem -ConnectionId "pnppowershell" -Id "12345" -Properties @{ "Test1"= "Test van deze PnP PowerShell Connector"; "Test2" = "Red","Blue"; "Test3" = ([System.DateTime]::Now)} -ContentValue "Sample value" -ContentType Text -GrantUsers "user@contoso.onmicrosoft.com"
```

This will add an item in the external Microsoft Search index with the properties as provided and grants only the user with the specified UPN access to find the item back through Microsoft Search.

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

### -Id
Unique identifier of the external item in Microsoft Search. You can provide any identifier you want to identity this item. This identifier will be used to update the item if it already exists.

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
The Connection ID of the custom connector to use. This is the ID that was entered when registering the custom connector and will indicate for which custom connector this external item is being added to the Microsoft Search index.

```yaml
Type: String
Parameter Sets: (All)
Required: True
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Properties
A hashtable with all the managed properties you want to provide for this external item. The key of the hashtable is the name of the managed property, the value is the value you want to provide for this managed property. The value can be a string, a string array or a DateTime object.

```yaml
Type: Hashtable
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentValue
A summary of the content that is being indexed. Can be used to display in the search result.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentType
Defines the type of content used in the ContentValue attribue. Defaults to Text.

```yaml
Type: SearchExternalItemContentType
Parameter Sets: (All)
Accepted values: Text, Html
Required: False
Position: Named
Default value: Text
Accept pipeline input: False
Accept wildcard characters: False
```

### -GrantUsers
When provided, the external item will only be shown to the users provided through this parameter. It can contain one or multiple users by providing AzureADUser objects, user principal names or Entra user IDs.

```yaml
Type: AzureADUserPipeBind[]
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GrantGroups
When provided, the external item will only be shown to the users which are members of the groups provided through this parameter. It can contain one or multiple groups by providing AzureADGroup objects, group names or Entra group IDs.

```yaml
Type: AzureADGroupPipeBind[]
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenyUsers
When provided, the external item not be shown to the users provided through this parameter. It can contain one or multiple users by providing AzureADUser objects, user principal names or Entra user IDs.

```yaml
Type: AzureADUserPipeBind[]
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenyGroups
When provided, the external item will not be shown to the users which are members of the groups provided through this parameter. It can contain one or multiple groups by providing AzureADGroup objects, group names or Entra group IDs.

```yaml
Type: AzureADGroupPipeBind[]
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GrantExternalGroups
When provided, the external item will be shown to the groups provided through this parameter. It can contain one or multiple users by providing the external group identifiers.

```yaml
Type: String[]
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenyExternalGroups
When provided, the external item will not be shown to the groups provided through this parameter. It can contain one or multiple users by providing the external group identifiers.

```yaml
Type: String[]
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GrantEveryone
When provided, the external item will be shown to everyone.

```yaml
Type: SwitchParameter
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