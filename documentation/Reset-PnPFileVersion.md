---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Reset-PnPFileVersion.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Reset-PnPFileVersion
---

# Reset-PnPFileVersion

## SYNOPSIS

Resets a file to its previous version

## SYNTAX

### Default (Default)

```
Reset-PnPFileVersion -ServerRelativeUrl <String> [-CheckinType <CheckinType>]
 [-CheckInComment <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to rollback a file to its previous version.

## EXAMPLES

### EXAMPLE 1

```powershell
Reset-PnPFileVersion -ServerRelativeUrl "/sites/test/office365.png"
```

### EXAMPLE 2

```powershell
Reset-PnPFileVersion -ServerRelativeUrl "/sites/test/office365.png" -CheckinType MajorCheckin -Comment "Restored to previous version"
```

## PARAMETERS

### -CheckInComment

The comment added to the check-in. Defaults to 'Restored to previous version'.

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

### -CheckinType

The check in type to use. Defaults to Major.

```yaml
Type: CheckinType
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

### -ServerRelativeUrl

The server relative URL of the file.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
