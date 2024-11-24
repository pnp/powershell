---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPList
---

# Get-PnPList

## SYNOPSIS

Returns lists from SharePoint

## SYNTAX

### Default (Default)

```
Get-PnPList [[-Identity] <ListPipeBind>] [-ThrowExceptionIfListNotFound]
 [-Connection <PnPConnection>] [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns lists in the current web.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPList
```

Returns all lists in the current web

### EXAMPLE 2

```powershell
Get-PnPList -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

Returns a list with the given id

### EXAMPLE 3

```powershell
Get-PnPList -Identity Lists/Announcements
```

Returns a list with the given url

### EXAMPLE 4

```powershell
Get-PnPList | Where-Object {$_.RootFolder.ServerRelativeUrl -like "/lists/*"}
```

This examples shows how to do wildcard searches on the list URL. It returns all lists whose URL starts with "/lists/" This could also be used to search for strings inside of the URL.

### EXAMPLE 5

```powershell
Get-PnPList -Includes HasUniqueRoleAssignments
```

This examples shows how to retrieve additional properties of the list.

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

### -Identity

The ID, name or Url (Lists/MyList) of the list

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Includes

List of properties

```yaml
Type: String[]
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

### -ThrowExceptionIfListNotFound

Switch parameter if an exception should be thrown if the requested list does not exist (true) or if omitted, nothing will be returned in case the list does not exist

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
