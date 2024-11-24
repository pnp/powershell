---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPLibraryFileVersionBatchDeleteJobStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPLibraryFileVersionBatchDeleteJobStatus
---

# Get-PnPLibraryFileVersionBatchDeleteJobStatus

## SYNOPSIS

Get the progress of deleting existing file versions on the document library.

## SYNTAX

### Default (Default)

```
Get-PnPLibraryFileVersionBatchDeleteJobStatus -Identity <ListPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of the progress of deleting existing file versions on the document library.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPLibraryFileVersionBatchDeleteJobStatus -Identity "Documents"
```

Returns the progress of deleting existing file versions on the document library.

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

### -Identity

The ID, name or Url (Lists/MyList) of the document library to perform the trimming on.

```yaml
Type: ListPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
