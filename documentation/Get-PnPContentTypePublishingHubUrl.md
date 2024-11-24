---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPContentTypePublishingHubUrl.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPContentTypePublishingHubUrl
---

# Get-PnPContentTypePublishingHubUrl

## SYNOPSIS

Returns the url to Content Type Publishing Hub

## SYNTAX

### Default (Default)

```
Get-PnPContentTypePublishingHubUrl [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve the url of the Content Type Publishing Hub.

## EXAMPLES

### EXAMPLE 1

```powershell
$url = Get-PnPContentTypePublishingHubUrl
Connect-PnPOnline -Url $url
Get-PnPContentType
```

This will retrieve the url to the content type hub, connect to it, and then retrieve the content types form that site

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
