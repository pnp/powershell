---
Module Name: PnP.PowerShell
title: Get-PnPListItemVersion
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListItemVersion.html
---
 
# Get-PnPListItemVersion

## SYNOPSIS
Retrieves the previous versions of a list item.

## SYNTAX

```powershell
Get-PnPListItemVersion -List <ListPipeBind> -Identity <ListItemPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet retrieves the version history of a list item.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListItemVersion -List "Demo List" -Identity 1
```

Retrieves the list item version history.

## PARAMETERS

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

### -Identity
The ID of the listitem, or actual ListItem object.

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Version
The ID or label of the version.

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)