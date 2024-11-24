---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Copy-PnPList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Copy-PnPList
---

# Copy-PnPList

## SYNOPSIS

Creates a copy of an existing list

## SYNTAX

### Copy a list to the same site by providing a list id, name or list instance

```
Copy-PnPList -Identity <ListPipeBind> -Title <String> [-WhatIf] [-Verbose]
 [-Connection <PnPConnection>]
```

### Copy a list to the same site by providing a list URL

```
Copy-PnPList -SourceListUrl <String> -Title <String> [-WhatIf] [-Verbose]
 [-Connection <PnPConnection>]
```

### Copy a list to another site by providing a list id, name or list instance

```
Copy-PnPList -Identity <ListPipeBind> -DestinationWebUrl <String> [-Title <String>] [-WhatIf]
 [-Verbose] [-Connection <PnPConnection>]
```

### Copy a list to another site by providing a list URL

```
Copy-PnPList -SourceListUrl <String> -DestinationWebUrl <String> [-Title <String>] [-WhatIf]
 [-Verbose] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows an existing list to be copied to either the same site or to another site (same tenant). It copies the fields, views and settings of the list. It does not copy along the list items in the list. If you wish to copy a list from one tenant to another, you will have to export the list as a provisioning template, and then apply the template to the target tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Copy-PnPList -Identity "My List" -Title "Copy of My List"
```

Copies the list "My List" located in the current site to "Copy of My List", also in the current site

### EXAMPLE 2

```powershell
Copy-PnPList -Identity "My List" -DestinationWebUrl https://contoso.sharepoint.com/sites/hrdepartment
```

Copies the list "My List" to the site with the provided URL keeping the same list name

### EXAMPLE 3

```powershell
Copy-PnPList -Identity "My List" -DestinationWebUrl https://contoso.sharepoint.com/sites/hrdepartment -Title "My copied list"
```

Copies the list "My List" to the site with the provided URL changing the list name to "My copied list"

### EXAMPLE 4

```powershell
$list = Get-PnPList -Identity "My List"
Copy-PnPList -Identity $list -Title "My copied list"
```

Copies the list "My List" to the site with the provided URL changing the list name to "My copied list"

### EXAMPLE 5

```powershell
Get-PnPList | ? Title -like "*Test*" | Copy-PnPList -DestinationWebUrl https://contoso.sharepoint.com/sites/hrdepartment
```

Copies all the lists on the current site having a title that contains "Test" to the site with the provided URL

### EXAMPLE 6

```powershell
Copy-PnPList -SourceListUrl https://contoso.sharepoint.com/sites/templates/lists/mylist -Verbose -DestinationWebUrl https://contoso.sharepoint.com/sites/hrdepartment\
```

Copies the list located at the provided URL through -SourceListUrl to the site provided through -DestinationWebUrl

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

### -DestinationWebUrl

Full SharePoint Online site URL to the site where the list should be copied to, i.e. https://contoso.sharepoint.com/sites/hrdepartment

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: LISTBYPIPE
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: LISTBYURL
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

List id, name or instance of a list you want to make a copy of

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: TOCURRENTSITEBYURL
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: LISTBYURL
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SourceListUrl

Full SharePoint Online list URL to the list you want to make a copy of, i.e. https://contoso.sharepoint.com/sites/hrdepartment/lists/mylist

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: TOCURRENTSITEBYURL
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: LISTBYURL
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Title

Title to give to the new list which will be created by copying an existing list

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: TOCURRENTSITEBYURL
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: TOCURRENTSITEBYPIPE
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: LISTBYPIPE
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: LISTBYURL
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -WhatIf

Switch parameter which executes the cmdlet but does not do the actual copy. Use in combination with -Verbose to see if all preconditions to be able to make a copy of the list are met without doing the actual copy.

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
