---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPHomeSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPHomeSite
---

# Get-PnPHomeSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the SharePoint home sites for your tenant

## SYNTAX

### Basic (Default)

```
Get-PnPHomeSite [-IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled <SwitchParameter>]
 [-Connection <PnPConnection>]
```

### Detailed

```
Get-PnPHomeSite -Detailed <SwitchParameter> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet will return the SharePoint Home sites for your tenant. Depending on which parameters you provide, you will get returned either the default first Home Site URL or details on all the Home Sites that have been configured for your tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPHomeSite
```

Returns the URL of the first home site for your tenant

### EXAMPLE 2

```powershell
Get-PnPHomeSite -IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled
```

Returns whether Viva Connections landing experience is set to the SharePoint home site.

### EXAMPLE 3

```powershell
Get-PnPHomeSite -Detailed
```

Returns detailed information on all the home sites that have been configured for your tenant

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

### -Detailed

When provided, it returns detailed information on all the home sites configured on your tenant

```yaml
Type: SwitchParameter
DefaultValue: True
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Detailed
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled

When provided, it retrieves whether Viva Connections landing experience is set to the SharePoint home site.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Basic
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
