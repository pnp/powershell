---
Module Name: PnP.PowerShell
title: Set-PnPField
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPField.html
---
 
# Set-PnPField

## SYNOPSIS
Changes one or more properties of a field in a specific list or for the whole site

## SYNTAX

```powershell
Set-PnPField [-List <ListPipeBind>] [-Identity <FieldPipeBind>] [-Values <Hashtable>] [-ShowInFiltersPane <ShowInFiltersPaneStatus>] [-UpdateExistingLists] [-NoCrawl] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to modify a field in a specific list or for the whole site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPField -Identity AssignedTo -Values @{JSLink="customrendering.js";Group="My fields"}
```

Updates the AssignedTo field on the current web to use customrendering.js for the JSLink and sets the group name the field is categorized in to "My Fields". Lists that are already using the AssignedTo field will not be updated.

### EXAMPLE 2
```powershell
Set-PnPField -Identity AssignedTo -Values @{JSLink="customrendering.js";Group="My fields"} -UpdateExistingLists
```

Updates the AssignedTo field on the current web to use customrendering.js for the JSLink and sets the group name the field is categorized in to "My Fields". Lists that are already using the AssignedTo field will also be updated.

### EXAMPLE 3
```powershell
Set-PnPField -List "Tasks" -Identity "AssignedTo" -Values @{JSLink="customrendering.js"}
```

Updates the AssignedTo field on the Tasks list to use customrendering.js for the JSLink

### EXAMPLE 5
```powershell
Set-PnPField -List "Tasks" -Identity "AssignedTo" -NoCrawl:$true
```

Prevents the field from being returned in search results


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
The field object, internal field name (case sensitive) or field id to update

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
The list object, name or id where to update the field. If omitted the field will be updated on the web.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -UpdateExistingLists
If provided, the field will be updated on existing lists that use it as well. If not provided or set to $false, existing lists using the field will remain unchanged but new lists will get the updated field.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
Hashtable of properties to update on the field. Use the syntax @{property1="value";property2="value"}.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowInFiltersPane
Allows configuring a field to either always be shown (Pinned), never be shown (Removed) or have SharePoint define if it should be shown (Auto = default).

```yaml
Type: Commands.Enums.ShowInFiltersPaneStatus
Parameter Sets: (All)
Accepted values: Auto, Removed, Pinned

Required: False
Position: Named
Default value: Auto
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoCrawl
Indicates if this field should not be returned in search results ($true) or if it should be ($false)

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