---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchExternalSchema.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSearchExternalSchema
---

# Set-PnPSearchExternalSchema

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, ExternalConnection.ReadWrite.All

Updates the schema set on a connection to an external datasource belonging to Microsoft Search

## SYNTAX

### By textual schema

```
Set-PnPSearchExternalSchema -ConnectionId <SearchExternalConnectionPipeBind> -SchemaAsText <String>
 [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

### By schema instance

```
Set-PnPSearchExternalSchema -ConnectionId <SearchExternalConnectionPipeBind>
 -Schema <Model.Graph.MicrosoftSearch.ExternalSchema> [-Verbose] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

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

Unique identifier or instance of the external connection in Microsoft Search to set the schema for

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Schema

An instance of a schema to set on the external connection

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By schema instance
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SchemaAsText

The textual representation of the schema to set on the external connection

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By textual schema
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
