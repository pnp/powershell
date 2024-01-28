---
Module Name: PnP.PowerShell
title: Set-PnPListItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPListItem.html
---
 
# Set-PnPListItem

## SYNOPSIS

Updates a list item

<a href="https://pnp.github.io/powershell/articles/batching.html">
<img src="https://raw.githubusercontent.com/pnp/powershell/gh-pages/images/batching/Batching.png" alt="Supports Batching">
</a>

## SYNTAX

### Single

```powershell
Set-PnPListItem [-List <ListPipeBind>] -Identity <ListItemPipeBind> [-ContentType <ContentTypePipeBind>]
 [-Values <Hashtable>] [-UpdateType <UpdateType>] [-Label <String>] [-ClearLabel] [-Force] [-Connection <PnPConnection>] 
```

### Batched

```powershell
Set-PnPListItem [-List <ListPipeBind>] -Identity <ListItemPipeBind> -Batch <PnPBatch> [-ContentType <ContentTypePipeBind>]
 [-Values <Hashtable>] [-UpdateType <UpdateType>] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to modify a list item.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPListItem -List "Demo List" -Identity 1 -Values @{"Title" = "Test Title"; "Category"="Test Category"}
```

Sets fields value in the list item with ID 1 in the "Demo List". It sets both the Title and Category fields with the specified values. Notice, use the internal names of fields.

### EXAMPLE 2

```powershell
Set-PnPListItem -List "Demo List" -Identity 1 -ContentType "Company" -Values @{"Title" = "Test Title"; "Category"="Test Category"}
```

Sets fields value in the list item with ID 1 in the "Demo List". It sets the content type of the item to "Company" and it sets both the Title and Category fields with the specified values. Notice, use the internal names of fields.

### EXAMPLE 3

```powershell
Set-PnPListItem -List "Demo List" -Identity $item -Values @{"Title" = "Test Title"; "Category"="Test Category"}
```

Sets fields value in the list item which has been retrieved by for instance Get-PnPListItem. It sets the content type of the item to "Company" and it sets both the Title and Category fields with the specified values. Notice, use the internal names of fields.

### EXAMPLE 4

```powershell
Set-PnPListItem -List "Demo List" -Identity 1 -Label "Public"
```

Sets the retention label in the list item with ID 1 in the "Demo List".

### EXAMPLE 5

```powershell
$batch = New-PnPBatch
for($i=1;$i -lt 100;$i++)
{
    Set-PnPListItem -List "Demo List" -Identity $i -Values @{"Title"="Updated Title"} -Batch $batch
}
Invoke-PnPBatch -Batch $batch
```

This example updates the items with ids 0 to 100 with a new title in a batched manner.

### EXAMPLE 6

```powershell
Set-PnPListItem -List "Demo List" -Identity 1 -Values @{"Editor"="testuser@domain.com"} -UpdateType UpdateOverwriteVersion
```

This example updates the modified by value of the list item and does not increase the version number.

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

Specify either the name, ID or an actual content type

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The ID of the listitem, or actual ListItem object

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

### -UpdateType

Specifies the update type to use when updating the listitem. Possible values are "Update", "SystemUpdate", "UpdateOverwriteVersion".

* **Update**: Sets field values and creates a new version if versioning is enabled for the list. The "Modified By" and "Modified" fields will be updated to reflect the time of the update and the user who made the change.
* **SystemUpdate**: Sets field values and does not create a new version. Any events on the list will trigger. The "Modified By" and "Modified" fields not updated and can not be set.
* **UpdateOverwriteVersion**: Sets field values and does not create a new version. No events on the list will trigger. The "Modified By" and "Modified" fields are not updated but can be set by passing the field values in the update. HINT: use 'Editor' to set the "Modified By" field.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearLabel

Clears the retention label of the item.

```yaml
Type: SwitchParameter
Parameter Sets: Single

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values

Use the internal names of the fields when specifying field names.

Single line of text: ``` -Values @{"TextField" = "Title New"} ```

Multiple lines of text: ``` -Values @{"MultiTextField" = "New text\`n\`nMore text"} ```

Rich text: ``` -Values @{"MultiTextField" = "&lt;strong&gt;New&lt;/strong&gt; text"} ```

Choice: ``` -Values @{"ChoiceField" = "Value 1"} ```

Multi-Choice: ``` -Values @{"MultiChoice" = "Choice 1","Choice 2"} ```

Number: ``` -Values @{"NumberField" = "10"} ```

Currency: ``` -Values @{"CurrencyField" = "10"} ```

Date and Time: ``` -Values @{"DateAndTimeField" = "03/13/2015 14:16"} ```

Lookup (id of lookup value): ``` -Values @{"LookupField" = "2"} ```

Multi value lookup (id of lookup values as array 1): ``` -Values @{"MultiLookupField" = "1","2"} ```

Multi value lookup (id of lookup values as array 2): ``` -Values @{"MultiLookupField" = 1,2} ```

Multi value lookup (id of lookup values as string): ``` -Values @{"MultiLookupField" = "1,2"} ```

Yes/No: ``` -Values @{"YesNoField" = $false} ```

Person/Group (id of user/group in Site User Info List or email of the user, separate multiple values with a comma): ``` -Values @{"PersonField" = "user1@domain.com","21"} ```

**If the user is not present, in the site user information list, you need to add that user using `New-PnPUser` cmdlet.**

Managed Metadata (single value with path to term): ``` -Values @{"MetadataField" = "CORPORATE|DEPARTMENTS|FINANCE"} ```

Managed Metadata (single value with id of term): ``` -Values @{"MetadataField" = "fe40a95b-2144-4fa2-b82a-0b3d0299d818"} with Id of term ```

Managed Metadata (multiple values with paths to terms): ``` -Values @{"MetadataField" = ("CORPORATE|DEPARTMENTS|FINANCE","CORPORATE|DEPARTMENTS|HR")} ```

Managed Metadata (multiple values with ids of terms): ``` -Values @{"MetadataField" = ("fe40a95b-2144-4fa2-b82a-0b3d0299d818","52d88107-c2a8-4bf0-adfa-04bc2305b593")} ```

Hyperlink or Picture: ``` -Values @{"HyperlinkField" = "https://pnp.github.com/powershell, PnP PowerShell Home"} ```

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Forces update of the list item even if there are no value changes. This can be useful for triggering webhooks, event receivers, Flows, etc.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
