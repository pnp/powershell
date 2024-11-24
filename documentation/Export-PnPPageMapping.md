---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Export-PnPPageMapping.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Export-PnPPageMapping
---

# Export-PnPPageMapping

## SYNOPSIS

Get's the built-in mapping files or a custom mapping file for your publishing portal page layouts. These mapping files are used to tailor the page transformation experience.

## SYNTAX

### Default (Default)

```
Export-PnPPageMapping [-<SwitchParameter>] [-<SwitchParameter>] [-<SwitchParameter>]
 [-PublishingPage <PagePipeBind>] [-<SwitchParameter>] [-Folder <String>] [-<SwitchParameter>]
 [-<SwitchParameter>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION



## EXAMPLES

### EXAMPLE 1

```powershell
Export-PnPPageMapping -BuiltInPageLayoutMapping -CustomPageLayoutMapping -Folder c:\\temp -Overwrite
```

Exports the built in page layout mapping and analyzes the current site's page layouts and exports these to files in folder c:\temp

### EXAMPLE 2

```powershell
Export-PnPPageMapping -CustomPageLayoutMapping -PublishingPage mypage.aspx -Folder c:\\temp -Overwrite
```

Analyzes the page layout of page mypage.aspx and exports this to a file in folder c:\temp

### EXAMPLE 3

```powershell
Export-PnPPageMapping -BuiltInWebPartMapping -Folder c:\\temp -Overwrite
```

Exports the built in webpart mapping to a file in folder c:\temp. Use this a starting basis if you want to tailer the web part mapping behavior.

## PARAMETERS

### -AnalyzeOOBPageLayouts

Set this flag if you also want to analyze the OOB page layouts...typically these are covered via the default mapping, but if you've updated these page layouts you might want to analyze them again

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -BuiltInPageLayoutMapping

Exports the builtin pagelayout mapping file (only needed for publishing page transformation)

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -BuiltInWebPartMapping

Exports the builtin web part mapping file

```yaml
Type: SwitchParameter
DefaultValue: ''
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
DefaultValue: ''
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

### -CustomPageLayoutMapping

Analyzes the pagelayouts in the current publishing portal and exports them as a pagelayout mapping file

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -Folder

The folder to created the mapping file(s) in

```yaml
Type: String
DefaultValue: ''
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

### -Logging

Outputs analyzer logging to the console

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -Overwrite

Overwrites existing mapping files

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -PublishingPage

The name of the publishing page to export a page layout mapping file for

```yaml
Type: PagePipeBind
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
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
