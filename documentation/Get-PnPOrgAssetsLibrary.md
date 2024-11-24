---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPOrgAssetsLibrary.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPOrgAssetsLibrary
---

# Get-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the list of all the configured organizational asset libraries

## SYNTAX

### Default (Default)

```
Get-PnPOrgAssetsLibrary [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list of all the configured organizational asset libraries.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPOrgAssetsLibrary
```

Returns the list of all the configured organizational asset sites

### EXAMPLE 2

```powershell
(Get-PnPOrgAssetsLibrary)[0].OrgAssetsLibraries[0].LibraryUrl.DecodedUrl
```

Returns the server relative url of the first document library which has been flagged as organizational asset library, i.e. "sites/branding/logos"

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
