---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPLibraryFileVersionBatchDeleteJob.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPLibraryFileVersionBatchDeleteJob
---

# Remove-PnPLibraryFileVersionBatchDeleteJob

## SYNOPSIS

Cancels further processing of a file version batch trim job for a document library.

## SYNTAX

### Default (Default)

```
Remove-PnPLibraryFileVersionBatchDeleteJob -Identity <ListPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Cancels further processing of a file version batch trim job for a document library.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents"
```

Cancels further processing of the file version batch trim job for the document library.

### EXAMPLE 2

```powershell
Remove-PnPLibraryFileVersionBatchDeleteJob -Identity "Documents" -DeleteBeforeDays 360 -Force
```

Cancels further processing of the file version batch trim job for the document library, without prompting the user for confirmation.

## PARAMETERS

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

The ID, name or Url (Lists/MyList) of the document library to stop further trimming on.

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
