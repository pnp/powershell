---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPFileCheckedIn.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPFileCheckedIn
---

# Set-PnPFileCheckedIn

## SYNOPSIS

Checks in a file.

## SYNTAX

### Default (Default)

```
Set-PnPFileCheckedIn [-Url] <String> [-CheckInType <CheckInType>] [-Comment <String>] [-Approve]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet checks in a single file, optionally with a comment.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPFileCheckedIn -Url "/Documents/Contract.docx"
```

Checks in the file "Contract.docx" in the "Documents" library

### EXAMPLE 2

```powershell
Set-PnPFileCheckedIn -Url "/Documents/Contract.docx" -CheckInType MinorCheckIn -Comment "Smaller changes"
```

Checks in the file "Contract.docx" in the "Documents" library as a minor version and adds the check in comment "Smaller changes"

## PARAMETERS

### -Approve

Approves the file.

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

### -CheckInType

The check in type to use. Defaults to MajorCheckIn.

```yaml
Type: CheckInType
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
AcceptedValues:
- MinorCheckIn
- MajorCheckIn
- OverwriteCheckIn
HelpMessage: ''
```

### -Comment

The check in comment.

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

The server relative url of the file to check in.

```yaml
Type: String
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
