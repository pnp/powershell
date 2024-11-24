---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPCompatibleHubContentTypes.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPCompatibleHubContentTypes
---

# Get-PnPCompatibleHubContentTypes

## SYNOPSIS

**Required Permissions**

  * ViewPages permission on the current web.

Returns the list of content types present in content type hub site that can be added to the root web or a list on a target site.

## SYNTAX

### Default (Default)

```
 -WebUrl <String> [-ListUrl <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list of content types present in content type hub site that are possible to be added to the current site or given list.

## EXAMPLES

### EXAMPLE 1

```powershell
 Get-PnPCompatibleHubContentTypes -WebUrl 'https://contoso.sharepoint.com/web1'
```

This will return the list of content types present in content type hub site that can be added to the root web of the site to which the provided web belongs.

### EXAMPLE 2

```powershell
 Get-PnPCompatibleHubContentTypes -WebUrl 'https://contoso.sharepoint.com/web1' -ListUrl 'https://contoso.sharepoint.com/web1/Shared Documents'
```

This will return the list of content types present in content type hub site that can be added to the provided list.

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

### -ListUrl

The full URL to the list for which compatible content types need to be fetched, i.e. 'https://contoso.sharepoint.com/web1/Shared Documents'

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

### -WebUrl

The full URL of the web for which compatible content types need to be fetched. In case of a list this should be the url of the web which contains the given list. I.e. 'https://contoso.sharepoint.com/web1'

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
