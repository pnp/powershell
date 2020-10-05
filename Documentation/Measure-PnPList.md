---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/measure-pnplist
schema: 2.0.0
title: Measure-PnPList
---

# Measure-PnPList

## SYNOPSIS
Returns statistics on the list object. This may fail on lists larger than the list view threshold

## SYNTAX

```
Measure-PnPList [-Identity] <ListPipeBind> [-ItemLevel] [-BrokenPermissions] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [-Includes <String[]>] [<CommonParameters>]
```

## DESCRIPTION

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

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

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

### -Identity

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

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

### -ItemLevel
Show item level statistics

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
The web to apply the command to. Omit this parameter to use the current web.

Only applicable to: SharePoint Online, SharePoint Server 2019, SharePoint Server 2016

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