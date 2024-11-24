---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Publish-PnPSyntexModel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Publish-PnPSyntexModel
---

# Publish-PnPSyntexModel

## SYNOPSIS

Publishes Microsoft Syntex models to a list.

This cmdlet only works when you've connected to a Syntex Content Center site.

<a href="https://pnp.github.io/powershell/articles/batching.html">
<img src="https://raw.githubusercontent.com/pnp/powershell/gh-pages/images/batching/Batching.png" alt="Supports Batching">
</a>

## SYNTAX

### Single

```
Publish-PnPSyntexModel -Model <SyntexModelPipeBind> -ListWebUrl <string> -List <ListPipeBind>
 [-PublicationViewOption <MachineLearningPublicationViewOption>] [-Connection <PnPConnection>]
```

### Batched

```
Publish-PnPSyntexModel -Model <SyntexModelPipeBind> -TargetSiteUrl <string>
 -TargetWebServerRelativeUrl <string> -TargetLibraryServerRelativeUrl <string> -Batch <PnPBatch>
 [-PublicationViewOption <MachineLearningPublicationViewOption>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command publishes Syntex document processing models to a list.

## EXAMPLES

### EXAMPLE 1

```powershell
Publish-PnPSyntexModel -Model "Invoice model" -ListWebUrl "https://contoso.sharepoint.com/sites/finance" -List "Documents"
```

Publishes the document processing model named "Invoice model" to the list named "Documents" in the /sites/finance web.

### EXAMPLE 2

```powershell
Publish-PnPSyntexModel -Model "Invoice model" -TargetSiteUrl "https://contoso.sharepoint.com/sites/finance" -TargetWebServerRelativeUrl "/sites/finance" -TargetLibraryServerRelativeUrl "/sites/finance/shared%20documents" -Batch $batch
```

Adds the publishing of the document processing model named "Invoice model" to the "Shared Documents" library into the PnPBatch $batch. Use `Invoke-PnPBatch -Batch $batch` to execute the batch, use `$batch = New-PnPBatch` to create a batch.

## PARAMETERS

### -Batch

The batch to add this publish request to.

```yaml
Type: PnPBatch
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Batched
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
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

### -List

The name or id of the list to publish the model to.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Single
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ListWebUrl

Url of the web hosting the list to publish the model to.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Single
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Model

The name or id of the Syntex model.

```yaml
Type: SyntexModelPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PublicationViewOption

The view options to apply when publishing the model to the list.

```yaml
Type: MachineLearningPublicationViewOption
DefaultValue: NewViewAsDefault
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
- NewView
- NewViewAsDefault
- NoNewView
HelpMessage: ''
```

### -TargetLibraryServerRelativeUrl

The server relative url of the library to publish the model to.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Batched
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TargetSiteUrl

The fully qualified URL of the site collection hosting the library to publish the model to.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Batched
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TargetWebServerRelativeUrl

The server relative url of the web hosting the library to publish the model to.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Batched
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
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
