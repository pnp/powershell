---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureADApp
---

# Get-PnPAzureADApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.Read.All

Returns Azure AD App registrations.

## SYNTAX

### Identity (Default)

```
Get-PnPAzureADApp [-Identity <AzureADAppPipeBind>] [-Connection <PnPConnection>]
```

### Filter

```
Get-PnPAzureADApp -Filter <string> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlets returns all app registrations, a specific one or ones matching a provided filter.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureADApp
```

This returns all Azure AD App registrations.

### EXAMPLE 2

```powershell
Get-PnPAzureADApp -Identity MyApp
```

This returns the Azure AD App registration with the display name as 'MyApp'.

### EXAMPLE 3

```powershell
Get-PnPAzureADApp -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

This returns the Azure AD App registration with the app id specified or the id specified.

### EXAMPLE 4

```powershell
Get-PnPAzureADApp -Filter "startswith(description, 'contoso')"
```

This returns the Azure AD App registrations with the description starting with "contoso". This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/graph/aad-advanced-queries?tabs=http#group-properties)

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

### -Filter

Specify the query to pass to Graph API in $filter.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Filter
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

Specify the display name, id or app id.

```yaml
Type: AzureADAppPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Identity
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
