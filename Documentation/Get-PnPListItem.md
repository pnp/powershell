---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnplistitem
schema: 2.0.0
title: Get-PnPListItem
---

# Get-PnPListItem

## SYNOPSIS
Retrieves list items

## SYNTAX

### All Items (Default)
```
Get-PnPListItem [-List] <ListPipeBind> [-FolderServerRelativeUrl <String>] [-Fields <String[]>]
 [-PageSize <Int32>] [-ScriptBlock <ScriptBlock>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### By Id
```
Get-PnPListItem [-List] <ListPipeBind> [-Id <Int32>] [-Fields <String[]>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### By Unique Id
```
Get-PnPListItem [-List] <ListPipeBind> [-UniqueId <GuidPipeBind>] [-Fields <String[]>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### By Query
```
Get-PnPListItem [-List] <ListPipeBind> [-Query <String>] [-FolderServerRelativeUrl <String>]
 [-PageSize <Int32>] [-ScriptBlock <ScriptBlock>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListItem -List Tasks
```

Retrieves all list items from the Tasks list

### EXAMPLE 2
```powershell
Get-PnPListItem -List Tasks -Id 1
```

Retrieves the list item with ID 1 from the Tasks list

### EXAMPLE 3
```powershell
Get-PnPListItem -List Tasks -UniqueId bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3
```

Retrieves the list item with unique id bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3 from the tasks lists

### EXAMPLE 4
```powershell
(Get-PnPListItem -List Tasks -Fields "Title","GUID").FieldValues
```

Retrieves all list items, but only includes the values of the Title and GUID fields in the list item object

### EXAMPLE 5
```powershell
Get-PnPListItem -List Tasks -Query "<View><Query><Where><Eq><FieldRef Name='GUID'/><Value Type='Guid'>bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3</Value></Eq></Where></Query></View>"
```

Retrieves all available fields of list items based on the CAML query specified

### EXAMPLE 6
```powershell
Get-PnPListItem -List Tasks -Query "<View><ViewFields><FieldRef Name='Title'/><FieldRef Name='Modified'/></ViewFields><Query><Where><Geq><FieldRef Name='Modified'/><Value Type='DateTime'><Today/></Value></Eq></Where></Query></View>"
```

Retrieves all list items modified today, retrieving the columns 'Title' and 'Modified'. When you use -Query, you can add a <ViewFields> clause to retrieve specific columns (since you cannot use -Fields)

### EXAMPLE 7
```powershell
Get-PnPListItem -List Tasks -PageSize 1000
```

Retrieves all list items from the Tasks list in pages of 1000 items

### EXAMPLE 8
```powershell
Get-PnPListItem -List Tasks -PageSize 1000 -ScriptBlock { Param($items) $items.Context.ExecuteQuery() } | ForEach-Object { $_.BreakRoleInheritance($true, $true) }
```

Retrieves all list items from the Tasks list in pages of 1000 items and breaks permission inheritance on each item

### EXAMPLE 9
```powershell
Get-PnPListItem -List Samples -FolderServerRelativeUrl "/sites/contosomarketing/Lists/Samples/Demo"
```

Retrieves all list items from the Demo folder in the Samples list located in the contosomarketing site collection

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Fields
The fields to retrieve. If not specified all fields will be loaded in the returned list object.

```yaml
Type: String[]
Parameter Sets: All Items, By Id, By Unique Id
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FolderServerRelativeUrl
The server relative URL of a list folder from which results will be returned.

```yaml
Type: String
Parameter Sets: All Items, By Query
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The ID of the item to retrieve

```yaml
Type: Int32
Parameter Sets: By Id
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The list to query

```yaml
Type: ListPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PageSize
The number of items to retrieve per page request.

```yaml
Type: Int32
Parameter Sets: All Items, By Query
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Query
The CAML query to execute against the list

```yaml
Type: String
Parameter Sets: By Query
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScriptBlock
The script block to run after every page request.

```yaml
Type: ScriptBlock
Parameter Sets: All Items, By Query
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UniqueId
The unique id (GUID) of the item to retrieve

```yaml
Type: GuidPipeBind
Parameter Sets: By Unique Id
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)