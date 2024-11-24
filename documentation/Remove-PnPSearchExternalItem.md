---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPSearchExternalItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPSearchExternalItem
---

# Remove-PnPSearchExternalItem

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalItem.ReadWrite.OwnedBy, ExternalItem.ReadWrite.All as a delegate or application permission

Removes an external item from an external connector in Microsoft Search

## SYNTAX

### Default (Default)

```
Remove-PnPSearchExternalItem -ItemId <String> -ConnectionId <SearchExternalConnectionPipeBind>
 [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

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

The Connection ID or connection instance of the custom connector to use. This is the ID that was entered when registering the custom connector and will indicate for which custom connector this external item is being removed from the Microsoft Search index.

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

### -ItemId

Unique identifier of the external item in Microsoft Search that you wish to remove.

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/externalconnectors-externalitem-delete)
