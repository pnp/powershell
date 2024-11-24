---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAccessToken.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAccessToken
---

# Get-PnPAccessToken

## SYNOPSIS

Returns the current Microsoft Graph OAuth Access token.
If a Resource Type Name or Resource URL is specified, it will fetch the access token of the specified resource.

## SYNTAX

### Default (Default)

```
Get-PnPAccessToken [-ResourceTypeName] [-ResourceUrl] [-Decoded] [-Scopes]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Gets the OAuth 2.0 Access Token.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAccessToken
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API

### EXAMPLE 2

```powershell
Get-PnPAccessToken -Decoded
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API and shows the token with its content decoded

### EXAMPLE 3

```powershell
Get-PnPAccessToken -ResourceTypeName SharePoint
```

Gets the OAuth 2.0 Access Token to consume the SharePoint APIs and perform CSOM operations.

### EXAMPLE 4

```powershell
Get-PnPAccessToken -ResourceTypeName ARM
```

Gets the OAuth 2.0 Access Token to consume the Azure Resource Manager APIs and perform related operations. In PnP, you can use them in cmdlets related to Flow and PowerPlatform etc.

### EXAMPLE 5

```powershell
Get-PnPAccessToken -ResourceUrl "https://management.azure.com/.default"
```

Gets the OAuth 2.0 Access Token to consume the SharePoint APIs and perform CSOM operations.

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

### -Decoded

Returns the details from the access token in a decoded manner

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Resource Type Name (decoded)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Resource Url (decoded)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ResourceTypeName

Specify the Resource Type for which you want the access token. If not specified, it will by default return a Microsoft Graph access token.

```yaml
Type: ResourceTypeName
DefaultValue: Graph
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Resource Type Name
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Resource Type Name (decoded)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Graph
- SharePoint
- ARM
HelpMessage: ''
```

### -ResourceUrl

Specify the Resource URL for which you want the access token, i.e. https://graph.microsoft.com/.default. If not specified, it will by default return a Microsoft Graph access token.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Resource Url
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Resource Url (decoded)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scopes

The scopes to retrieve the token for. Defaults to AllSites.FullControl

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
