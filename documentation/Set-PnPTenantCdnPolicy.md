---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantCdnPolicy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTenantCdnPolicy
---

# Set-PnPTenantCdnPolicy

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the CDN Policies for the specified CDN (Public | Private).

## SYNTAX

### Default (Default)

```
Set-PnPTenantCdnPolicy -CdnType <SPOTenantCdnType> -PolicyType <SPOTenantCdnPolicyType>
 -PolicyValue <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets the CDN Policies for the specified CDN (Public | Private).

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTenantCdnPolicy -CdnType Public -PolicyType IncludeFileExtensions -PolicyValue "CSS,EOT,GIF,ICO,JPEG,JPG,JS,MAP,PNG,SVG,TTF,WOFF"
```

This example sets the IncludeFileExtensions policy to the specified value.

### EXAMPLE 2

```powershell
Set-PnPTenantCdnPolicy -CdnType Public -PolicyType ExcludeRestrictedSiteClassifications -PolicyValue "Confidential,Restricted"
```

This example sets the ExcludeRestrictedSiteClassifications policy for the selected CdnType to a policy value of listed excluded site classifications.

## PARAMETERS

### -CdnType

The type of cdn to set the policies for.

```yaml
Type: SPOTenantCdnType
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

### -PolicyType

The type of the policy to set.

```yaml
Type: SPOTenantCdnPolicyType
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
- IncludeFileExtensions
- ExcludeRestrictedSiteClassifications
- ExcludeIfNoScriptDisabled
HelpMessage: ''
```

### -PolicyValue

The value of the policy to set.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
