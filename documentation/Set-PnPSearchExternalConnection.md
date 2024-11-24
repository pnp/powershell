---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchExternalConnection.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSearchExternalConnection
---

# Set-PnPSearchExternalConnection

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of ExternalConnection.ReadWrite.OwnedBy, 	ExternalConnection.ReadWrite.All

Updates a connection to an external datasource for Microsoft Search

## SYNTAX

### Default (Default)

```
Set-PnPSearchExternalConnection -Identity <SearchExternalConnectionPipeBind> [-Name <String>]
 [-Description <String>] [-AuthorizedAppIds <String[]>] [-Verbose] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

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

### -AuthorizedAppIds

The client Ids of the application registrations that are allowed to add items to the index for this connection. Only provide when it needs to change.

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

Description of the connection displayed in the Microsoft 365 admin center. Only provide when it needs to change.

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

Unique identifier or an instance of the external connection in Microsoft Search that needs to be updated.

```yaml
Type: SearchExternalConnectionPipeBind
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

### -Name

The display name of the connection to be displayed in the Microsoft 365 admin center. Maximum length of 128 characters. Only provide when it needs to change.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: ''
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
