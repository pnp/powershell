---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPImageListItemColumn.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPImageListItemColumn
---

# Set-PnPImageListItemColumn

## SYNOPSIS

Updates the image column value of a list item.

## SYNTAX

### Upload an image and set it as thumbnail

```
Set-PnPImageListItemColumn -Identity <ListItemPipeBind> [-List <ListPipeBind>]
 [-Field <FieldPipeBind>] [-Path <string>] [-UpdateType <UpdateType>] [-Connection <PnPConnection>]
```

### Use an already uploaded image and set it as thumbnail

```
Set-PnPImageListItemColumn -Identity <ListItemPipeBind> [-List <ListPipeBind>]
 [-Field <FieldPipeBind>] [-ServerRelativePath <string>] [-UpdateType <UpdateType>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows setting the Image/Thumbnail column value of a list item.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPImageListItemColumn -List "Demo List" -Identity 1 -Field "Thumbnail" -ServerRelativePath "/sites/contoso/SiteAssets/test.png"
```

Sets the image/thumbnail field value in the list item with ID 1 in the "Demo List". Notice, use the internal names of fields.

### EXAMPLE 2

```powershell
Set-PnPImageListItemColumn -List "Demo List" -Identity 1 -Field "Thumbnail" -Path sample.png
```

Sets the image/thumbnail field value in the list item with ID 1 in the "Demo List". Notice, use the internal names of fields. Here, we upload the file to a folder in Site Assets library. In this scenario, ensure that the user has contribute rights to the Site Assets library.

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

### -Field

The ID, Title or Internal name of the field.

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

### -Identity

The ID of the list item, or actual ListItem object.

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -List

The ID, Title or Url of the list.

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

### -Path

Use the path from the local file system.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (ParameterSet_ASPath)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ServerRelativePath

Use the server relative path of an existing image in your SharePoint document library.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (ParameterSet_ASServerRelativeUrl)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UpdateType

Specifies the update type to use when updating the listitem. Possible values are "Update", "SystemUpdate", "UpdateOverwriteVersion".

* Update: Sets field values and creates a new version if versioning is enabled for the list
* SystemUpdate: Sets field values and does not create a new version. Any events on the list will trigger.
* UpdateOverwriteVersion: Sets field values and does not create a new version. No events on the list will trigger.

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
