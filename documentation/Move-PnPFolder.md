---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Move-PnPFolder.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Move-PnPFolder
---

# Move-PnPFolder

## SYNOPSIS

Move a folder to another location in the current web. If you want to move a folder to a different site collection, use the Move-PnPFile cmdlet instead, which also supports moving folders and also accross site collections. Move-PnPFolder can be used to move folders that are within the list view threshold, the commandlet will fail if the list view threshold is exceeded.

## SYNTAX

### Default (Default)

```
Move-PnPFolder -Folder <FolderPipeBind> -TargetFolder <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to move folder to another location in the current web. If you want to move a folder to a different site collection, use the Move-PnPFile cmdlet instead, which also supports moving folders and also across site collections. Move-PnPFolder can be used to move folders that are within the list view threshold, the commandlet will fail if the list view threshold is exceeded.

## EXAMPLES

### EXAMPLE 1

```powershell
Move-PnPFolder -Folder Documents/Reports -TargetFolder 'Archived Reports'
```

This will move the folder Reports in the Documents library to the 'Archived Reports' library

### EXAMPLE 2

```powershell
Move-PnPFolder -Folder 'Shared Documents/Reports/2016/Templates' -TargetFolder 'Shared Documents/Reports'
```

This will move the folder Templates to the new location in 'Shared Documents/Reports'

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

### -Folder

The folder to move

```yaml
Type: FolderPipeBind
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

### -TargetFolder

The new parent location to which the folder should be moved to

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
