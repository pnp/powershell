---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Undo-PnPFileCheckedOut.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Undo-PnPFileCheckedOut
---

# Undo-PnPFileCheckedOut

## SYNOPSIS

Discards changes to a file.

## SYNTAX

### Default (Default)

```
Undo-PnPFileCheckedOut [-Url] <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet discards changes to a single file.

## EXAMPLES

### EXAMPLE 1

```powershell
Undo-PnPFileCheckedOut -Url "/sites/PnP/Shared Documents/Contract.docx"
```

Discards changes in the file "Contract.docx" in the "Documents" library

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

The server relative url of the file to discard changes.

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
