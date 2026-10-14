---
Module Name: PnP.PowerShell
title: Restore-PnPListItemVersion
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Restore-PnPListItemVersion.html
---
 
# Restore-PnPListItemVersion

## SYNOPSIS
Restores a specific list item version.

## SYNTAX

```powershell
Restore-PnPListItemVersion -List <ListPipeBind> -Identity <ListItemPipeBind> -Version <ListItemVersionPipeBind> [-Force] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet restores a specific list item version.

## EXAMPLES

### EXAMPLE 1
```powershell
Restore-PnPListItemVersion -List "Demo List" -Identity 1 -Version 512
```

Restores the list item version with Id 512.

### EXAMPLE 2
```powershell
Restore-PnPListItemVersion -List "Demo List" -Identity 1 -Version "1.0"
```

Restores the list item version with version label "1.0".

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

### -Force
If provided, no confirmation will be requested and the action will be performed.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)