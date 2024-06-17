---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPField.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPField
---

# Get-PnPField

## SYNOPSIS

Returns a field from a list or site

## SYNTAX

```powershell
Get-PnPField [-List <ListPipeBind>] [[-Identity] <FieldPipeBind>] [-Group <String>] [-InSiteHierarchy]
 [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPField
```

Gets all the fields from the current site

### EXAMPLE 2

```powershell
Get-PnPField -List "Demo list" -Identity "Speakers"
```

Gets the speakers field from the list Demo list

### EXAMPLE 3

```powershell
Get-PnPField -Group "Custom Columns"
```

Gets all the fields for the group called Custom Columns for the site currently connected to

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

### -Group

Filter to the specified group

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The field object or name to get

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -InSiteHierarchy

Search site hierarchy for fields

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List

The list object or name where to get the field from

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
