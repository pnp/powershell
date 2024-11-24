---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPOrgAssetsLibrary.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPOrgAssetsLibrary
---

# Remove-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a given document library as an organizational asset source.

## SYNTAX

### Default (Default)

```
Remove-PnPOrgAssetsLibrary -LibraryUrl <String> [-ShouldRemoveFromCdn <Boolean>]
 [-CdnType <SPOTenantCdnType>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes a given document library as an organizational asset source based on its server relative URL in your SharePoint Online tenant. It will not remove the document library itself. It may take some time before this change will be reflected in the web interface.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPOrgAssetsLibrary -LibraryUrl "sites/branding/logos"
```

This example removes the document library "logos" residing in the site collection with the url "sites/branding" from the list with organizational assets keeping it as an Office 365 CDN source.

### EXAMPLE 2

```powershell
Remove-PnPOrgAssetsLibrary -LibraryUrl "sites/branding/logos" -ShouldRemoveFromCdn $true
```

This example removes the document library "logos" residing in the site collection with the url "sites/branding" from the list with organizational assets also removing it as a Public Office 365 CDN source.

### EXAMPLE 3

```powershell
Remove-PnPOrgAssetsLibrary -LibraryUrl "sites/branding/logos" -ShouldRemoveFromCdn $true -CdnType Private
```

This example removes the document library "logos" residing in the site collection with the url "sites/branding" from the list with organizational assets also removing it as a Private Office 365 CDN source.

## PARAMETERS

### -CdnType

Indicates what type of Office 365 CDN source the document library that will no longer be flagged as an organizational asset was of

```yaml
Type: SPOTenantCdnType
DefaultValue: Public
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

### -LibraryUrl

The server relative url of the document library flagged as organizational asset which you want to remove, i.e. "sites/branding/logos"

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

### -ShouldRemoveFromCdn

Boolean indicating if the document library that will no longer be flagged as an organizational asset also needs to be removed as an Office 365 CDN source.

```yaml
Type: Boolean
DefaultValue: False
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
