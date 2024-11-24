---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPContentTypePublishingStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPContentTypePublishingStatus
---

# Get-PnPContentTypePublishingStatus

## SYNOPSIS

**Required Permissions**

  * Fullcontrol permission on the content type hub site.

Returns the publishing status of a content type present on content type hub site.

## SYNTAX

### Default (Default)

```
Get-PnPContentTypePublishingStatus -ContentType <ContentTypePipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve the publishing status of a content type present on content type hub site.

## EXAMPLES

### EXAMPLE 1

```powershell
 Get-PnPContentTypePublishingStatus -ContentType 0x0101
```

This will return `True` if content type is published in the content type hub site otherwise it will return `False`.

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

### -ContentType

The content type object in the content type hub site for which the publishing status needs to be fetched.

```yaml
Type: ContentType
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
