---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchExternalItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSearchExternalItem
---

# Set-PnPSearchExternalItem

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalItem.ReadWrite.OwnedBy, ExternalItem.ReadWrite.All

Adds or updates an external item in Microsoft Search

## SYNTAX

### Default (Default)

```
Set-PnPSearchExternalItem -ItemId <String> -ConnectionId <SearchExternalConnectionPipeBind>
 -Properties <Hashtable> [-ContentValue <String>] [-ContentType <SearchExternalItemContentType>]
 [-GrantUsers <AzureADUserPipeBind[]>] [-GrantGroups <AzureADGroupPipeBind[]>]
 [-DenyUsers <AzureADUserPipeBind[]>] [-DenyGroups <AzureADGroupPipeBind[]>]
 [-GrantExternalGroups <String[]>] [-DenyExternalGroups <String[]>]
 [-GrantEveryone <SwitchParameter>] [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet can be used to add or update an external item in Microsoft Search on custom connectors. The cmdlet will create a new external item if the item does not exist yet. If the item already exists, it will be updated.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSearchExternalItem -ConnectionId "pnppowershell" -ItemId "12345" -Properties @{ "Test1"= "Test of this PnP PowerShell Connector"; "Test2" = "Red","Blue"; "Test3" = ([System.DateTime]::Now)} -ContentValue "Sample value" -ContentType Text -GrantEveryone
```

This will add an item in the external Microsoft Search index with the properties as provided and grants everyone access to find the item back through Microsoft Search. It shows three types of properties you can set for an external item in the index, being a simple text, an array and a date/time value.

### EXAMPLE 2

```powershell
Set-PnPSearchExternalItem -ConnectionId "pnppowershell" -ItemId "12345" -Properties @{ "Test1"= "Test of this PnP PowerShell Connector"; "Test2" = "Red","Blue"; "Test3" = ([System.DateTime]::Now)} -ContentValue "Sample value" -ContentType Text -GrantUsers "user@contoso.onmicrosoft.com"
```

This will add an item in the external Microsoft Search index with the properties as provided and grants only the user with the specified UPN access to find the item back through Microsoft Search. It shows three types of properties you can set for an external item in the index, being a simple text, an array and a date/time value.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ConnectionId

The Connection ID or connection instance of the custom connector to use. This is the ID that was entered when registering the custom connector and will indicate for which custom connector this external item is being added to the Microsoft Search index.

```yaml
Type: SearchExternalConnectionPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: ''
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ContentType

Defines the type of content used in the ContentValue attribue. Defaults to Text.

```yaml
Type: SearchExternalItemContentType
DefaultValue: Text
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Text
- Html
HelpMessage: ''
```

### -ContentValue

A summary of the content that is being indexed. Can be used to display in the search result.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DenyExternalGroups

When provided, the external item will not be shown to the groups provided through this parameter. It can contain one or multiple users by providing the external group identifiers.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DenyGroups

When provided, the external item will not be shown to the users which are members of the groups provided through this parameter. It can contain one or multiple groups by providing AzureADGroup objects, group names or Entra group IDs.

```yaml
Type: AzureADGroupPipeBind[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DenyUsers

When provided, the external item not be shown to the users provided through this parameter. It can contain one or multiple users by providing AzureADUser objects, user principal names or Entra user IDs.

```yaml
Type: AzureADUserPipeBind[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GrantEveryone

When provided, the external item will be shown to everyone.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GrantExternalGroups

When provided, the external item will be shown to the groups provided through this parameter. It can contain one or multiple users by providing the external group identifiers.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GrantGroups

When provided, the external item will only be shown to the users which are members of the groups provided through this parameter. It can contain one or multiple groups by providing AzureADGroup objects, group names or Entra group IDs.

```yaml
Type: AzureADGroupPipeBind[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GrantUsers

When provided, the external item will only be shown to the users provided through this parameter. It can contain one or multiple users by providing AzureADUser objects, user principal names or Entra user IDs.

```yaml
Type: AzureADUserPipeBind[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ItemId

Unique identifier of the external item in Microsoft Search. You can provide any identifier you want to identity this item. This identifier will be used to update the item if it already exists.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Properties

A hashtable with all the managed properties you want to provide for this external item. The key of the hashtable is the name of the managed property, the value is the value you want to provide for this managed property. The value can be a string, a string array or a DateTime object.

```yaml
Type: Hashtable
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
