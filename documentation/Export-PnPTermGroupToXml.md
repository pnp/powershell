---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Export-PnPTermGroupToXml.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Export-PnPTermGroupToXml
---

# Export-PnPTermGroupToXml

## SYNOPSIS

Exports a taxonomy TermGroup to either the output or to an XML file.

## SYNTAX

### Default (Default)

```
Export-PnPTermGroupToXml [-Identity <TermGroupPipeBind>] [-Out <String>] [-FullTemplate]
 [-Encoding <Encoding>] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to export a taxonomy TermGroup to either the output or to an XML file.

## EXAMPLES

### EXAMPLE 1

```powershell
Export-PnPTermGroupToXml
```

Exports all term groups in the default site collection term store to the standard output

### EXAMPLE 2

```powershell
Export-PnPTermGroupToXml -Out output.xml
```

Exports all term groups in the default site collection term store to the file 'output.xml' in the current folder

### EXAMPLE 3

```powershell
Export-PnPTermGroupToXml -Out c:\output.xml -Identity "Test Group"
```

Exports the term group with the specified name to the file 'output.xml' located in the root folder of the C: drive.

### EXAMPLE 4

```powershell
$termgroup = Get-PnPTermGroup -Identity Test
$termgroup | Export-PnPTermGroupToXml -Out c:\output.xml
```

Retrieves a termgroup and subsequently exports that term group to a the file named 'output.xml'

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

### -Encoding

Defaults to Unicode

```yaml
Type: Encoding
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

Overwrites the output file if it exists.

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

### -FullTemplate

If specified, a full provisioning template structure will be returned

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

The ID or name of the termgroup

```yaml
Type: TermGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Out

File to export the data to.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
