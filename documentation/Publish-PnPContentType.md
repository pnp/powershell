---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Publish-PnPContentType.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Publish-PnPContentType
---

# Publish-PnPContentType

## SYNOPSIS

**Required Permissions**

  * Fullcontrol permission on the content type hub site.

Publishes or republishes a content type present on content type hub site.

## SYNTAX

### Default (Default)

```
Publish-PnPContentType -ContentType <ContentTypePipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to publish a content type present on content type hub site. To check if a content type has been published already, you can use [Get-PnPContentTypePublishingStatus](Get-PnPContentTypePublishingStatus.md).

## EXAMPLES

### EXAMPLE 1

```powershell
 Publish-PnPContentType -ContentType 0x0101
```

This will publish the content type with the given id.

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

The content type object in the content type hub site which is to be published.

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
