---
Module Name: PnP.PowerShell
title: Measure-PnPList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Measure-PnPList.html
---
 
# Measure-PnPList

## SYNOPSIS
Returns statistics on the list object. This may fail on lists larger than the list view threshold

## SYNTAX

```powershell
Measure-PnPList [-Identity] <ListPipeBind> [-ItemLevel] [-BrokenPermissions] 
 [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION

Allows to retrieve statistics on specified list. The command may fail on lists larger than the list view threshold.

## EXAMPLES

### EXAMPLE 1
```powershell
Measure-PnPList "Documents"
```

Gets statistics on Documents document library

### EXAMPLE 2
```powershell
Measure-PnPList "Documents" -BrokenPermissions -ItemLevel
```

Displays items and folders with broken permissions inside Documents library

## PARAMETERS

### -BrokenPermissions
Show items with broken permissions

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

### -Identity

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ItemLevel
Show item level statistics

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Includes
Optionally allows properties to be retrieved for the returned list which are not included in the response by default

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

