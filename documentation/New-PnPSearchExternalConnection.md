---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPSearchExternalConnection.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPSearchExternalConnection
---

# New-PnPSearchExternalConnection

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, 	ExternalConnection.ReadWrite.All

Creates a new connection to an external datasource for Microsoft Search

## SYNTAX

### Default (Default)

```
New-PnPSearchExternalConnection -Identity <String> -Name <String> -Description <String>
 [-AuthorizedAppIds <String[]>] [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet can be used to create a new connection to an external datasource that needs to be indexed into Microsoft Search through a custom connector. Use [Set-PnPSearchExternalItem](Set-PnPSearchExternalItem.md) to add items to the index for this connector.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPSearchExternalConnection -Identity "pnppowershell" -Name "PnP PowerShell" -Description "External content ingested using PnP PowerShell"
```

This will create a new external connection with the provided name and description. Any application registration with the proper permissions can add items to the index for this connection.

### EXAMPLE 2

```powershell
New-PnPSearchExternalConnection -Identity "pnppowershell" -Name "PnP PowerShell" -Description "External content ingested using PnP PowerShell" -AuthorizedAppIds "00000000-0000-0000-0000-000000000000","11111111-1111-1111-1111-111111111111"
```

This will create a new external connection with the provided name and description. Only the application registrations of which the client Ids have been provided can add items to the index for this connection.

## PARAMETERS

### -AuthorizedAppIds

The client Ids of the application registrations that are allowed to add items to the index for this connection. If not provided, any application registration with the proper permissions can add items to the index for this connection.

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

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

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

### -Description

Description of the connection displayed in the Microsoft 365 admin center

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

### -Identity

Unique identifier of the external connection in Microsoft Search. Must be unique within the tenant. Must be between 3 and 32 characters in length. Must only contain alphanumeric characters. Cannot begin with Microsoft or be one of the following values: None, Directory, Exchange, ExchangeArchive, LinkedIn, Mailbox, OneDriveBusiness, SharePoint, Teams, Yammer, Connectors, TaskFabric, PowerBI, Assistant, TopicEngine, MSFT_All_Connectors.

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

### -Name

The display name of the connection to be displayed in the Microsoft 365 admin center. Maximum length of 128 characters.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: ''
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
