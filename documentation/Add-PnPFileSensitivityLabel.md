---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFileSensitivityLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFileSensitivityLabel
---

# Add-PnPFileSensitivityLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Files.ReadWrite.All, Sites.ReadWrite.All

Add the sensitivity label information for a file in SharePoint.

## SYNTAX

### Default (Default)

```
Add-PnPFileSensitivityLabel -Identity <String> -SensitivityLabelId <Guid> -AssignmentMethod <Enum>
 -JustificationText <string>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Add-PnPFileSensitivityLabel cmdlet adds the sensitivity label information for a file in SharePoint using Microsoft Graph. It takes a URL as input, decodes it, and specifically encodes the '+' character if it is part of the filename. It also takes the sensitivity label Id , assignment method and justification text values as input.

## EXAMPLES

### Example 1

This example adds the sensitivity label information for the file at the specified URL.

```powershell
Add-PnPFileSensitivityLabel -Identity "/sites/Marketing/Shared Documents/Report.pptx" -SensitivityLabelId "b5b11b04-05b3-4fe4-baa9-b7f5f65b8b64" -JustificationText "Previous label no longer applies" -AssignmentMethod Privileged
```

### Example 2

This example removes the sensitivity label information for the file at the specified URL.

```powershell
Add-PnPFileSensitivityLabel -Identity "/sites/Marketing/Shared Documents/Report.pptx" -SensitivityLabelId "" -JustificationText "Previous label no longer applies" -AssignmentMethod Privileged
```

## PARAMETERS

### -AssignmentMethod

The assignment method of the label on the document. Indicates whether the assignment of the label was done automatically, standard, or as a privileged operation (the equivalent of an administrator operation).

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
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Standard
- Privileged
- Auto
HelpMessage: ''
```

### -Identity

The server relative path to the file, the unique identifier of the file, the listitem representing the file, or the file object itself on which we are adding the sensitivity label.

```yaml
Type: FilePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -JustificationText

Justification text for audit purposes, and is required when downgrading/removing a label.

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
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SensitivityLabelId

ID of the sensitivity label to be assigned, or empty string to remove the sensitivity label.

```yaml
Type: string
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
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
