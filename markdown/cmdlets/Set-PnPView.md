---
Module Name: PnP.PowerShell
title: Set-PnPView
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPView.html
---
 
# Set-PnPView

## SYNOPSIS
Change view properties.

## SYNTAX

```powershell
Set-PnPView [[-List] <ListPipeBind>] -Identity <ViewPipeBind> [-Values <Hashtable>] [-Fields <String[]>]
 [-Aggregations <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Sets one or more properties of an existing view, see here https://learn.microsoft.com/previous-versions/office/sharepoint-server/ee543328(v=office.15) for the list of view properties.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPView -List "Tasks" -Identity "All Tasks" -Values @{JSLink="hierarchytaskslist.js|customrendering.js";Title="My view"}
```

Updates the "All Tasks" view on list "Tasks" to use hierarchytaskslist.js and customrendering.js for the JSLink and changes the title of the view to "My view".

### EXAMPLE 2
```powershell
Get-PnPList -Identity "Tasks" | Get-PnPView | Set-PnPView -Values @{JSLink="hierarchytaskslist.js|customrendering.js"}
```

Updates all views on list "Tasks" to use hierarchytaskslist.js and customrendering.js for the JSLink.

### EXAMPLE 3
```powershell
Set-PnPView -List "Documents" -Identity "Corporate Documents" -Fields "Title","Created"
```

Updates the Corporate Documents view on the Documents library to have two fields.

### EXAMPLE 4
```powershell
Set-PnPView -List "Documents" -Identity "Corporate Documents" -Fields "Title","Created" -Aggregations "<FieldRef Name='Title' Type='COUNT'/>"
```

Updates the Corporate Documents view on the Documents library and sets the totals (aggregations) to Count on the Title field.


### EXAMPLE 5
```powershell
Set-PnPView -List "Documents" -Identity "Dept Documents" -Fields "Title,"Created" -Values @{Paged=$true;RowLimit=[UInt32]"100"}
```

Updates the Dept Documents view on the Documents library to show items paged in batches of 100, note the type casting on the value to prevent warnings. 

## PARAMETERS

### -Aggregations
A valid XML fragment containing one or more Aggregations.

```yaml
Type: String
Parameter Sets: (All)

Required: False
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

### -Fields
An array of fields to use in the view. Notice that specifying this value will remove the existing fields.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Id, Title or instance of the view.

```yaml
Type: ViewPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
The Id, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
Hashtable of properties to update on the view. Use the syntax @{property1="value";property2="value"}.

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

