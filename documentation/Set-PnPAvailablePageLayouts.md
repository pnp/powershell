---
Module Name: PnP.PowerShell
title: Set-PnPAvailablePageLayouts
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPAvailablePageLayouts.html
---
 
# Set-PnPAvailablePageLayouts

## SYNOPSIS
Sets the available page layouts for the current site

## SYNTAX

### SPECIFIC
```powershell
Set-PnPAvailablePageLayouts -PageLayouts <String[]> [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### ALL
```powershell
Set-PnPAvailablePageLayouts [-AllowAllPageLayouts] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### INHERIT
```powershell
Set-PnPAvailablePageLayouts [-InheritPageLayouts] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

## PARAMETERS

### -AllowAllPageLayouts
An array of page layout files to set as available page layouts for the site.

```yaml
Type: SwitchParameter
Parameter Sets: ALL

Required: True
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

### -InheritPageLayouts
Set the available page layouts to inherit from the parent site.

```yaml
Type: SwitchParameter
Parameter Sets: INHERIT

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageLayouts
An array of page layout files to set as available page layouts for the site.

```yaml
Type: String[]
Parameter Sets: SPECIFIC

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

