---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPListItemAsRecord.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPListItemAsRecord
---

# Set-PnPListItemAsRecord

## SYNOPSIS

Declares a list item as a record, for more information, see https://learn.microsoft.com/sharepoint/governance/records-management-in-sharepoint-server

## SYNTAX

### Default (Default)

```
Set-PnPListItemAsRecord [-List] <ListPipeBind> -Identity <ListItemPipeBind>
 [-DeclarationDate <DateTime>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to set a list item as a record.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPListItemAsRecord -List "Documents" -Identity 4
```

Declares the document in the documents library with id 4 as a record.

### EXAMPLE 2

```powershell
Set-PnPListItemAsRecord -List "Documents" -Identity 4 -DeclarationDate $date
```

Declares the document in the documents library with id 4 as a record.

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

### -DeclarationDate

The declaration date.

```yaml
Type: DateTime
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

The ID of the listitem, or actual ListItem object.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
