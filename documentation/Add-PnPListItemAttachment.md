---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPListItemAttachment.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPListItemAttachment
---

# Add-PnPListItemAttachment

## SYNOPSIS

Adds an attachment to the specified list item in the SharePoint list

## SYNTAX

### Upload attachment file from path

```
Add-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Path <String>]
 [-NewFileName <String>] [-Connection <PnPConnection>]
```

### Upload attachment file from stream

```
Add-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-FileName <String>]
 [-Stream <Stream>] [-Connection <PnPConnection>]
```

### Create attachment file from text

```
Add-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-FileName <String>]
 [-Content <text>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows adding a file as an attachment to a list item in a SharePoint Online list.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPListItemAttachment -List "Demo List" -Identity 1 -Path c:\temp\test.mp4
```

Adds a new attachment to the list item with Id "1" in the "Demo List" SharePoint list with file name as test.mp4 from the specified path.

### EXAMPLE 2

```powershell
Add-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName "test.txt" -Content '{ "Test": "Value" }'
```

Adds a new attachment to the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt and content as specified.

### EXAMPLE 3

```powershell
Add-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName "test.mp4" -Stream $fileStream
```

Adds a new attachment to the list item with Id "1" in the "Demo List" SharePoint list with file name as test.mp4 and content coming from a stream.

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

### -Content

Specify text of the attachment for the list item.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Upload file from text)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -FileName

Filename to give to the attachment file on SharePoint

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Upload file from stream
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Upload file from text)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The ID of the listitem, or actual ListItem object to add the attachment to.

```yaml
Type: ListItemPipeBind
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

### -List

The ID, Title or Url of the list. Note that when providing the name of the list, the name is case-sensitive.

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

### -NewFileName

Filename to give to the attachment file on SharePoint

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Upload file)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

The local file path

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Upload file)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Stream

Stream with the file contents

```yaml
Type: Stream
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Upload file from stream)
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
