---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFileVersion.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFileVersion
---

# Get-PnPFileVersion

## SYNOPSIS

Retrieves the previous versions of a file. Does not retrieve the current version of the file.

## SYNTAX

### Default (Default)

```
Get-PnPFileVersion -Url <String> [-UseVersionExpirationReport] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Retrieves the version history of a file, not including its current version. To get the current version use the MajorVersion and MinorVersion properties returned from Get-PnPFile.

It can optionally return the version expiration report, which contains the versions' SnapshotDate (or estimated SnapshotDate if it is not available) and estimated ExpirationDate based on the Automatic Version History Limits.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFileVersion -Url Documents/MyDocument.docx
```

Retrieves the file version information for the specified file.

### EXAMPLE 2

```powershell
Get-PnPFileVersion -Url "/sites/marketing/Shared Documents/MyDocument.docx"
```

Retrieves the file version information for the specified file by specifying the path to the site and the document library's URL.

### EXAMPLE 3

```powershell
Get-PnPFileVersion -Url "/sites/marketing/Shared Documents/MyDocument.docx" -UseVersionExpirationReport
```

Retrieves the version expiration report for the specified file.

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

### -Url



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

### -UseVersionExpirationReport

Returns the file version expiration report. The versions contained in the report has the SnapshotDate (or estimated SnapshotDate if it is not available) and estimated ExpirationDate based on the Automatic Version History Limits.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
