---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantId.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantId
---

# Get-PnPTenantId

## SYNOPSIS

Returns the Tenant ID

## SYNTAX

### By TenantUrl

```
Get-PnPTenantId -TenantUrl <String> [-AzureEnvironment <AzureEnvironment>]
```

### By connection

```
Get-PnPTenantId [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve id of tenant. This does not require an active connection to that tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantId
```

Returns the current Tenant Id. A valid connection with Connect-PnPOnline is required either as a current connection or by providing it using the -Connection parameter.

### EXAMPLE 2

```powershell
Get-PnPTenantId contoso
```

Returns the Tenant ID for the tenant contoso.sharepoint.com. Can be executed without an active PnP Connection.

### EXAMPLE 3

```powershell
Get-PnPTenantId -TenantUrl contoso.sharepoint.com
```

Returns the Tenant ID for the specified tenant. Can be executed without an active PnP Connection.

### EXAMPLE 4

```powershell
Get-PnPTenantId -TenantUrl contoso.sharepoint.us -AzureEnvironment USGovernment
```

Returns the Tenant ID for the specified US Government tenant. Can be executed without an active PnP Connection.

## PARAMETERS

### -AzureEnvironment

The Azure environment to use for the tenant lookup. It defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
DefaultValue: Production
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By URL
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Production
- PPE
- China
- Germany
- USGovernment
- USGovernmentHigh
- USGovernmentDoD
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection. If not specified, the current connection will be used.

```yaml
Type: PnPConnection
DefaultValue: Current connection
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: From connection
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TenantUrl

The name of the tenant to retrieve the id for. If not specified, the currently connected to tenant will be used. You can use either just the tenant name, i.e. contoso or the full SharePoint URL, i.e. contoso.sharepoint.com.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By URL
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
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
