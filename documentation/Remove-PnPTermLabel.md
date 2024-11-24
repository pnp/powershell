---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTermLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTermLabel
---

# Remove-PnPTermLabel

## SYNOPSIS

Removes a single label/translation from a term.

## SYNTAX

### By Term Id

```
Remove-PnPTermLabel -Label <String> -Term <Guid> -Lcid <Int32> [-TermStore <Guid>]
 [-Connection <PnPConnection>] [-Confirm]
```

### By Name

```
Remove-PnPTermLabel -Label <String> -Term <String> -Lcid <Int32> -TermSet <Name|Guid>
 -TermGroup <Name|Guid> [-TermStore <Guid>] [-Connection <PnPConnection>] [-Confirm]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes a label/translation from a term.

## EXAMPLES

### Example 1

```powershell
Remove-PnPTermLabel -Label "Marknadsföring" -Lcid 1053 -Term 2d1f298b-804a-4a05-96dc-29b667adec62
```

Removes the Swedish label from the specified term.

### Example 2

```powershell
Remove-PnPTermLabel -Label "Marknadsföring" -Lcid 1053 -Term "Marketing" -TermSet "Departments" -TermGroup "Corporate"
```
Removes the Swedish label from the specified term.

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

### -Label

The label to remove.

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

### -Lcid

Language code identifier of the term label.

```yaml
Type: Int32
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

### -Term

Identity of term to update. Either a name or a GUID.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Term Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermGroup

The term group containing the term set.

```yaml
Type: Guid or String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Name
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermSet

The term set containing the term.

```yaml
Type: Guid or String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Name
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TermStore

The Term store containing the term group.

```yaml
Type: Guid
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
