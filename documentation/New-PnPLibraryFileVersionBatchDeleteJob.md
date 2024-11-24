---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPLibraryFileVersionBatchDeleteJob.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPLibraryFileVersionBatchDeleteJob
---

# New-PnPLibraryFileVersionBatchDeleteJob

## SYNOPSIS

Starts a file version batch trim job for a document library.

## SYNTAX

### Default (Default)

```
New-PnPLibraryFileVersionBatchDeleteJob -Identity <ListPipeBind> [-DeleteBeforeDays <int>]
 [-MajorVersionLimit <int>] [-MajorWithMinorVersionsLimit <<int>][Automatic][-Force]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Starts a file version batch trim job for a document library.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -DeleteBeforeDays 360
```

Starts a file version batch trim job that will delete all file versions that are over 360 days old in the document library.

### EXAMPLE 2

```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -DeleteBeforeDays 360 -Force
```

Starts a file version batch trim job that will delete all file versions that are over 360 days old in the document library, without prompting the user for confirmation.

### EXAMPLE 3

```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -Automatic
```

Starts a file version batch trim job that will delete file versions that expired and set version expiration time for the ones not expired in the document library based on the backend algorithm.

### EXAMPLE 4

```powershell
New-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -MajorVersionLimit 30 -MajorWithMinorVersionsLimit 10
```

Starts a file version batch trim job that will delete file versions in the document library based on the version count limits.

## PARAMETERS

### -Automatic

Trim file version using automatic trim.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: AutomaticTrim
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DeleteBeforeDays

The minimum age of file versions to trim. In other words, all file versions that are older than this number of days will be deleted.

```yaml
Type: int
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: DeleteOlderThanDays
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Force

When provided, no confirmation prompts will be shown to the user.

```yaml
Type: SwitchParameter
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

### -MajorVersionLimit

Trim file version using version count limits. Need to specify MajorWithMinorVersionsLimit as well.

```yaml
Type: int
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: CountLimits
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MajorWithMinorVersionsLimit

Trim file version using version count limits. Need to specify MajorVersionLimit as well.

```yaml
Type: int
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: CountLimits
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
