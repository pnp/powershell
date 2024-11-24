---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantCdnEnabled.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTenantCdnEnabled
---

# Set-PnPTenantCdnEnabled

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Enables or disables the public or private Office 365 Content Delivery Network (CDN).

## SYNTAX

### Default (Default)

```
Set-PnPTenantCdnEnabled -Enable <Boolean> -CdnType <CdnType> [-NoDefaultOrigins]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Enables or disables the public or private Office 365 Content Delivery Network (CDN) for the tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTenantCdnEnabled -CdnType Public -Enable $true
```

This example sets the Public CDN enabled.

### EXAMPLE 2

```powershell
Set-PnPTenantCdnEnabled -CdnType Private -Enable $false
```

This example disables the Private CDN for the tenant.

### EXAMPLE 3

```powershell
Set-PnPTenantCdnEnabled -CdnType Public -Enable $true -NoDefaultOrigins
```

This example enables the Public CDN for the tenant, but skips the provisioning of the default origins.

## PARAMETERS

### -CdnType

The type of CDN to enable or disable

```yaml
Type: CdnType
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
AcceptedValues:
- Public
- Private
- Both
HelpMessage: ''
```

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

### -Enable

Specify to enable or disable

```yaml
Type: Boolean
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

### -NoDefaultOrigins

If specified, the default origins for the specified CDN type will not be provisioned. See [Default CDN origins](https://learn.microsoft.com/microsoft-365/enterprise/use-microsoft-365-cdn-with-spo?view=o365-worldwide#default-cdn-origins) for information about the origins that are provisioned by default when you enable the Office 365 CDN, and the potential impact of skipping the setup of default origins.

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
