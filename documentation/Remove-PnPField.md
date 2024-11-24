---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPField.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPField
---

# Remove-PnPField

## SYNOPSIS

Removes a field from a list or a site.

## SYNTAX

### Default (Default)

```
Remove-PnPField [-Identity] <FieldPipeBind> [[-List] <ListPipeBind>] [-Force]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a field from a list or a site.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPField -Identity "Speakers"
```

Removes the "Speakers" field from the site columns.

### EXAMPLE 2

```powershell
Remove-PnPField -List "Demo list" -Identity "Speakers"
```

Removes the speakers field from the list "Demo list".

### EXAMPLE 3

```powershell
$batch = New-PnPBatch
Remove-PnPField -List "Demo list" -Identity "Speakers" -Batch $batch
Remove-PnPField -List "Demo list" -Identity "Sponsors" -Batch $batch
Remove-PnPField -List "Demo list" -Identity "Organizers" -Batch $batch
Remove-PnPField -Identity "Test" -Batch $batch
Invoke-PnPBatch $batch
```

Removes the speakers, sponsors and organizers fields from the list "Demo list" as well as Test field from the web in a batch.

## PARAMETERS

### -Batch

Batch object used to remove fields in a batched manner. See above example on how to use this.

```yaml
Type: PnPBatch
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: named
  IsRequired: false
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

### -Force

Specifying the Force parameter will skip the confirmation question.

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

The field object or name to remove.

```yaml
Type: FieldPipeBind
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

### -List

The list object or name where to remove the field from.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
  IsRequired: false
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
