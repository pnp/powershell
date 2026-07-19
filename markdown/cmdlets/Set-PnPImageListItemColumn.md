---
Module Name: PnP.PowerShell
title: Set-PnPImageListItemColumn
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPImageListItemColumn.html
---
 
# Set-PnPImageListItemColumn

## SYNOPSIS

Updates the image column value of a list item.

## SYNTAX

### Upload an image and set it as thumbnail

```powershell
Set-PnPImageListItemColumn [-List <ListPipeBind>] -Identity <ListItemPipeBind> [-Field <FieldPipeBind>]
 [-Path <string>] [-UpdateType <UpdateType>] [-Connection <PnPConnection>] 
```

### Use an already uploaded image and set it as thumbnail

```powershell
Set-PnPImageListItemColumn [-List <ListPipeBind>] -Identity <ListItemPipeBind> [-Field <FieldPipeBind>]
 [-ServerRelativePath <string>] [-UpdateType <UpdateType>] [-Connection <PnPConnection>] 
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The ID of the list item, or actual ListItem object.

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

### -Field

The ID, Title or Internal name of the field.

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ServerRelativePath

Use the server relative path of an existing image in your SharePoint document library.

```yaml
Type: String
Parameter Sets: (ParameterSet_ASServerRelativeUrl)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

Use the path from the local file system.

```yaml
Type: String
Parameter Sets: (ParameterSet_ASPath)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UpdateType

Specifies the update type to use when updating the listitem. Possible values are "Update", "SystemUpdate", "UpdateOverwriteVersion".

* Update: Sets field values and creates a new version if versioning is enabled for the list
* SystemUpdate: Sets field values and does not create a new version. Any events on the list will trigger.
* UpdateOverwriteVersion: Sets field values and does not create a new version. No events on the list will trigger.

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
