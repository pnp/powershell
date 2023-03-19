---
Module Name: PnP.PowerShell
title: Measure-PnPWeb
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Measure-PnPWeb.html
---
 
# Measure-PnPWeb

## SYNOPSIS
Returns statistics on the web object

## SYNTAX

```powershell
Measure-PnPWeb [[-Identity] <WebPipeBind>] [-Recursive] [-IncludeHiddenList] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

Allows to retrieve statistics on specified web. By specifying `Recursive` it is possible to retrieve statistics on all subwebs as well.

## EXAMPLES

### EXAMPLE 1
```powershell
Measure-PnPWeb
```

Gets statistics on the current web

### EXAMPLE 2
```powershell
Measure-PnPWeb $web -Recursive
```

Gets statistics on the provided web including all its subwebs

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

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -IncludeHiddenList
Include hidden lists in statistics calculation

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recursive
Iterate all sub webs recursively

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

