---
Module Name: PnP.PowerShell
title: Set-PnPDefaultPageLayout
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPDefaultPageLayout.html
---
 
# Set-PnPDefaultPageLayout

## SYNOPSIS
Sets a specific page layout to be the default page layout for a publishing site

## SYNTAX

### TITLE
```powershell
Set-PnPDefaultPageLayout -Title <String> [-Connection <PnPConnection>]
 
```

### INHERIT
```powershell
Set-PnPDefaultPageLayout [-InheritFromParentSite] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to set the default page layout for a publishing site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPDefaultPageLayout -Title projectpage.aspx
```

Sets projectpage.aspx to be the default page layout for the current web

### EXAMPLE 2
```powershell
Set-PnPDefaultPageLayout -Title test/testpage.aspx
```

Sets a page layout in a folder in the Master Page & Page Layout gallery, such as _catalog/masterpage/test/testpage.aspx, to be the default page layout for the current web

### EXAMPLE 3
```powershell
Set-PnPDefaultPageLayout -InheritFromParentSite
```

Sets the default page layout to be inherited from the parent site

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

### -InheritFromParentSite
Set the default page layout to be inherited from the parent site.

```yaml
Type: SwitchParameter
Parameter Sets: INHERIT

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Title of the page layout

```yaml
Type: String
Parameter Sets: TITLE

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

