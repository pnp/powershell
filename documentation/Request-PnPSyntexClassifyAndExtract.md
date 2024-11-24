---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Request-PnPSyntexClassifyAndExtract.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Request-PnPSyntexClassifyAndExtract
---

# Request-PnPSyntexClassifyAndExtract

## SYNOPSIS

Requests for a file, folder or all files in a library to be classified and extracted via the published Microsoft Syntex models on the libraries hosting the files.

<a href="https://pnp.github.io/powershell/articles/batching.html">
<img src="https://raw.githubusercontent.com/pnp/powershell/gh-pages/images/batching/Batching.png" alt="Supports Batching">
</a>

## SYNTAX

### File

```
Request-PnPSyntexClassifyAndExtract -FileUrl <string> [-Batch <PnPBatch>]
 [-Connection <PnPConnection>]
```

### Folder

```
Request-PnPSyntexClassifyAndExtract -Folder <FolderPipeBind> [-Connection <PnPConnection>]
```

### List

```
Request-PnPSyntexClassifyAndExtract -List <ListPipeBind> [-OffPeak <SwitchParameter>]
 [-Force <SwitchParameter>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command requests for all files in a library, folder or individual files to be classified and extracted via the published Syntex models on the libraries hosting the files. When using with the `OffPeak` switch then the files are send to the off peak Syntex document processing queue, this way there's no need to enumerate all files in the library and submit them to the regular queue. When using the `Force` switch without setting OffPeak then all files are enumerated and sent to the regular queue, regardless of whether they were processed in the past.

When the list contains more than 5000 files or when using the folder parameter the cmdlet will use the off peak Syntex queue.

## EXAMPLES

### EXAMPLE 1

```powershell
Request-PnPSyntexClassifyAndExtract -FileUrl "/sites/finance/invoices/invoice1.docx"
```

Requests the classification and extraction of invoice1.docx in library "Invoices".

### EXAMPLE 2

```powershell
Request-PnPSyntexClassifyAndExtract -List "Invoices"
```

Requests the classification and extraction of all files in library "Invoices" that never were classified and extracted before.

### EXAMPLE 3

```powershell
Request-PnPSyntexClassifyAndExtract -Folder (Get-PnPFolder -Url "invoices/Q1/jan")
```

Requests the classification and extraction of all files in the folder "jan" in library "invoices" that never were classified and extracted before.

## PARAMETERS

### -Batch

The batch to add this file classification and extraction request to.

```yaml
Type: PnPBatch
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: File
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

### -FileUrl

The server relative URL of the file to be classified and extracted.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: File
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Folder

The folder holding the files to classify and extract. When using this parameter, files will be send to the off peak queue.

```yaml
Type: FolderPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Folder
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Force

If set, then all files (even if classified and extracted before) are classified and extracted. If the list contains more than 5000 items this option will not apply and off-peak processing is used.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: List
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

The name or list holding the files to classify and extract.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: List
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OffPeak

If set, then the files to classify are sent to the off peak queue without enumerating them. If the list contains more than 5000 items then off-peak processing is always used.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: List
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
