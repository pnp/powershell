---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPDefaultColumnValues.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPDefaultColumnValues
---

# Set-PnPDefaultColumnValues

## SYNOPSIS

Sets default column values for a document library

## SYNTAX

### Default (Default)

```
Set-PnPDefaultColumnValues [-List] <ListPipeBind> -Field <FieldPipeBind> -Value <String[]>
 [-Folder <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets default column values for a document library, per folder, or for the root folder if the folder parameter has not been specified. Supports both text and taxonomy fields.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPDefaultColumnValues -List Documents -Field TaxKeyword -Value "Company|Locations|Stockholm"
```

Sets a default value for the enterprise keywords field on a library to a term called "Stockholm", located in the "Locations" term set, which is part of the "Company" term group

### EXAMPLE 2

```powershell
Set-PnPDefaultColumnValues -List Documents -Field TaxKeyword -Value "15c4c4e4-4b67-4894-a1d8-de5ff811c791"
```

Sets a default value for the enterprise keywords field on a library to a term with the id "15c4c4e4-4b67-4894-a1d8-de5ff811c791". You need to ensure the term is valid for the field.

### EXAMPLE 3

```powershell
Set-PnPDefaultColumnValues -List Documents -Field MyTextField -Value "DefaultValue" -Folder "My folder"
```

Sets a default value for the MyTextField text field on the folder "My folder" in a library to a value of "DefaultValue"

### EXAMPLE 4

```powershell
Set-PnPDefaultColumnValues -List Documents -Field MyPeopleField -Value "1;#Foo Bar"
```

Sets a default value for the MyPeopleField people field on a library to a value of "Foo Bar" using the id from the user information list.

### EXAMPLE 5

```powershell
$user = New-PnPUser -LoginName foobar@contoso.com
Set-PnPDefaultColumnValues -List Documents -Field MyPeopleField -Value "$($user.Id);#$($user.LoginName)"
```

Sets a default value for the MyPeopleField people field on a library to a value of "Foo Bar" using the id from the user information list.

### EXAMPLE 6

```powershell
$user1 = New-PnPUser -LoginName user1@contoso.com
$user2 = New-PnPUser -LoginName user2@contoso.com
Set-PnPDefaultColumnValues -List Documents -Field MyMultiPeopleField -Value "$($user1.Id);#$($user1.LoginName)","$($user2.Id);#$($user2.LoginName)"
```

Sets a default value for the MyMultiPeopleField people field on a library to a value of "User 1" and "User 2" using the id from the user information list.

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

The internal name, id or a reference to a field

```yaml
Type: FieldPipeBind
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

### -Folder

A library relative folder path, if not specified it will set the default column values on the root folder of the library ('/')

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

### -List

The ID, Name or Url of the list.

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

### -Value

A list of values. In case of a text field the values will be concatenated, separated by a semi-colon. In case of a taxonomy field multiple values will added. In case of people field multiple values will be added.

```yaml
Type: String[]
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
