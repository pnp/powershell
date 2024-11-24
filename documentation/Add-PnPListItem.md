---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPListItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPListItem
---

# Add-PnPListItem

## SYNOPSIS

Adds an item to the list and sets the creation time to the current date and time. The author is set to the current authenticated user executing the cmdlet. In order to set the author to a different user, please refer to Set-PnPListItem.

<a href="https://pnp.github.io/powershell/articles/batching.html">
<img src="https://raw.githubusercontent.com/pnp/powershell/gh-pages/images/batching/Batching.png" alt="Supports Batching">
</a>

## SYNTAX

### Single

```
Add-PnPListItem [-List] <ListPipeBind> [-ContentType <ContentTypePipeBind>] [-Values <Hashtable>]
 [-Folder <String>] [-Label <String>] [-Connection <PnPConnection>]
```

### Batched

```
Add-PnPListItem [-List] <ListPipeBind> -Batch <PnPBatch> [-ContentType <ContentTypePipeBind>]
 [-Values <Hashtable>] [-Folder <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add an item to the list and sets the creation time to the current date and time. The author is set to the current authenticated user executing the cmdlet. In order to set the author to a different user, please refer to Set-PnPListItem.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPListItem -List "Demo List" -Values @{"Title" = "Test Title"; "Category"="Test Category"}
```

Adds a new list item to the "Demo List", and sets both the Title and Category fields with the specified values. Notice, use the internal names of fields.

### EXAMPLE 2

```powershell
Add-PnPListItem -List "Demo List" -ContentType "Company" -Values @{"Title" = "Test Title"; "Category"="Test Category"}
```

Adds a new list item to the "Demo List", sets the content type to "Company" and sets both the Title and Category fields with the specified values. Notice, use the internal names of fields.

### EXAMPLE 3

```powershell
Add-PnPListItem -List "Demo List" -Values @{"MultiUserField"="user1@domain.com","user2@domain.com"}
```

Adds a new list item to the "Demo List" and sets the user field called MultiUserField to 2 users. Separate multiple users with a comma.

### EXAMPLE 4

```powershell
Add-PnPListItem -List "Demo List" -Values @{"Title"="Sales Report"} -Folder "projects/europe"
```

Adds a new list item to the "Demo List". It will add the list item to the europe folder which is located in the projects folder. Folders will be created if needed.

### EXAMPLE 5

```powershell
Add-PnPListItem -List "Demo List" -Values @{"Title"="Sales Report"} -Label "Public"
```

Adds a new list item to the "Demo List". Sets the retention label to "Public" if it exists on the site.

### EXAMPLE 6

```powershell
$batch = New-PnPBatch
for($i=0;$i -lt 10;$i++)
{
    Add-PnPListItem -List "Demo List" -Values @{"Title"="Report $i"} -Batch $batch
}
Invoke-PnPBatch -Batch $batch
```

This creates 10 list items by using a batched approach.

## PARAMETERS

### -Batch

Optional batch object used to add items in a batched manner. See examples on how to use this.

```yaml
Type: PnPBatch
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Batched
  Position: Named
  IsRequired: true
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

### -ContentType

Specify either the name, ID or an actual content type.

```yaml
Type: ContentTypePipeBind
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

The list relative URL of a folder. E.g. "MyFolder" for a folder located in the root of the list, or "MyFolder/SubFolder" for a folder located in the MyFolder folder which is located in the root of the list.

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

### -Label

The name of the retention label.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Single
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

### -Values

Use the internal names of the fields when specifying field names.

Single line of text: ``` -Values @{"Title" = "Title New"} ```

Multiple lines of text:  ``` -Values @{"MultiText" = "New text\n\nMore text"} ```

Rich text: ``` -Values @{"MultiText" = "&lt;strong&gt;New&lt;/strong&gt; text"} ```

Choice: ``` -Values @{"Choice" = "Choice 1"} ```

Multi-Choice: ``` -Values @{"MultiChoice" = "Choice 1","Choice 2"} ```

Number: ``` -Values @{"Number" = "10"} ```

Currency: ``` -Values @{"Currency" = "10"} ```

> [!NOTE]
> For numeric and currency fields, when using -Batch, provide the value using the comma and dots matching the regional setting of the site you're adding the listitem to. When not using batch, you must always provide the value in the American notation, so dot for decimals and comma for thousands separators.

Date and Time: ``` -Values @{"DateAndTime" = "03/13/2015 14:16"} ```

Lookup (id of lookup value): ``` -Values @{"Lookup" = "2"} ```

Multi value lookup (id of lookup values as array 1): ``` -Values @{"MultiLookupField" = "1","2"} ```

Multi value lookup (id of lookup values as array 2): ``` -Values @{"MultiLookupField" = 1,2} ```

Multi value lookup (id of lookup values as string): ``` -Values @{"MultiLookupField" = "1,2"} ```

Yes/No: ``` -Values @{"YesNo" = $false} ```

Person/Group (id of user/group in Site User Info List or email of the user, separate multiple values with a comma): ``` -Values @{"Person" = "user1@domain.com","21"} ```

**If the user is not present, in the site user information list, you need to add that user using `New-PnPUser` cmdlet.**

Managed Metadata (single value with path to term): ``` -Values @{"MetadataField" = "CORPORATE|DEPARTMENTS|FINANCE"} ```

Managed Metadata (single value with id of term): ``` -Values @{"MetadataField" = "fe40a95b-2144-4fa2-b82a-0b3d0299d818"} with Id of term ```

Managed Metadata (multiple values with paths to terms): ``` -Values @{"MetadataField" = "CORPORATE|DEPARTMENTS|FINANCE","CORPORATE|DEPARTMENTS|HR"} ```

Managed Metadata (multiple values with ids of terms): ``` -Values @{"MetadataField" = "fe40a95b-2144-4fa2-b82a-0b3d0299d818","52d88107-c2a8-4bf0-adfa-04bc2305b593"} ```

Hyperlink or Picture: ``` -Values @{"Hyperlink" = "https://github.com/OfficeDev/, OfficePnP"} ```

```yaml
Type: Hashtable
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
