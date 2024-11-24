---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchExternalConnection.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSearchExternalConnection
---

# Get-PnPSearchExternalConnection

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, ExternalConnection.Read.All,	ExternalConnection.ReadWrite.All

Retrieves all connections to external datasources belonging to Microsoft Search

## SYNTAX

### Default (Default)

```
Get-PnPSearchExternalConnection [-Identity <String>] [-Verbose] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet can be used to retrieve all connections to external datasources that are being indexed into Microsoft Search through a custom connector. Use [Set-PnPSearchExternalItem](Set-PnPSearchExternalItem.md) to add items to the index for a connector.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSearchExternalConnection
```

This will return all connections to external datasources that are being indexed into Microsoft Search that exist within the tenant.

### EXAMPLE 2

```powershell
Get-PnPSearchExternalConnection -Identity "pnppowershell"
```

This will return the connection to the external datasource with the specified identity that is being indexed into Microsoft Search.

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

### -Identity

Unique identifier of the external connection in Microsoft Search. If not provided, all connections will be returned.

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
