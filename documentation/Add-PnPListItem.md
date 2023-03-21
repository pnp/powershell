---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPListItem.html
external help file: PnP.PowerShell.dll-Help.xml
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

```powershell
Add-PnPListItem [-List] <ListPipeBind> [-ContentType <ContentTypePipeBind>] [-Values <Hashtable>]
 [-Folder <String>] [-Label <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Batched

```powershell
Add-PnPListItem [-List] <ListPipeBind> -Batch <PnPBatch> [-ContentType <ContentTypePipeBind>] [-Values <Hashtable>]
 [-Folder <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

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
Parameter Sets: Batched
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentType

Specify either the name, ID or an actual content type.

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder

The list relative URL of a folder. E.g. "MyFolder" for a folder located in the root of the list, or "MyFolder/SubFolder" for a folder located in the MyFolder folder which is located in the root of the list.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Label

The name of the retention label.

```yaml
Type: String
Parameter Sets: Single

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List

The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Values

Use the internal names of the fields when specifying field names.

Single line of text: -Values @{"Title" = "Title New"}

Multiple lines of text: -Values @{"MultiText" = "New text\n\nMore text"}

Rich text: -Values @{"MultiText" = "&lt;strong&gt;New&lt;/strong&gt; text"}

Choice: -Values @{"Choice" = "Choice 1"}
Multi-Choice: -Values @{"MultiChoice" = "Choice 1","Choice 2"}

Number: -Values @{"Number" = "10"}

Currency: -Values @{"Currency" = "10"}

> [!NOTE]
> For numeric and currency fields, when using -Batch, provide the value using the comma and dots matching the regional setting of the site you're adding the listitem to. When not using batch, you must always provide the value in the American notation, so dot for decimals and comma for thousands separators.

Date and Time: -Values @{"DateAndTime" = "03/13/2015 14:16"}

Lookup (id of lookup value): -Values @{"Lookup" = "2"}

Multi value lookup (id of lookup values as array 1): -Values @{"MultiLookupField" = "1","2"}

Multi value lookup (id of lookup values as array 2): -Values @{"MultiLookupField" = 1,2}

Multi value lookup (id of lookup values as string): -Values @{"MultiLookupField" = "1,2"}

Yes/No: -Values @{"YesNo" = $false}

Person/Group (id of user/group in Site User Info List or email of the user, separate multiple values with a comma): -Values @{"Person" = "user1@domain.com","21"}

Managed Metadata (single value with path to term): -Values @{"MetadataField" = "CORPORATE|DEPARTMENTS|FINANCE"}

Managed Metadata (single value with id of term): -Values @{"MetadataField" = "fe40a95b-2144-4fa2-b82a-0b3d0299d818"} with Id of term

Managed Metadata (multiple values with paths to terms): -Values @{"MetadataField" = "CORPORATE|DEPARTMENTS|FINANCE","CORPORATE|DEPARTMENTS|HR"}

Managed Metadata (multiple values with ids of terms): -Values @{"MetadataField" = "fe40a95b-2144-4fa2-b82a-0b3d0299d818","52d88107-c2a8-4bf0-adfa-04bc2305b593"}

Hyperlink or Picture: -Values @{"Hyperlink" = "https://github.com/OfficeDev/, OfficePnP"}

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
