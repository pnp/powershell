---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPNavigationNode.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPNavigationNode
---
  
# Add-PnPNavigationNode

## SYNOPSIS
Adds an item to a navigation element

## SYNTAX

```powershell
Add-PnPNavigationNode -Location <NavigationType> -Title <String> [-Url <String>] [-Parent <Int32>] [-First]
 [-External] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Adds a menu item to either the quicklaunch, top navigation, search navigation or the footer

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPNavigationNode -Title "Contoso" -Url "http://contoso.sharepoint.com/sites/contoso/" -Location "QuickLaunch"
```

Adds a navigation node to the quicklaunch. The navigation node will have the title "Contoso" and will link to the url "http://contoso.sharepoint.com/sites/contoso/"

### EXAMPLE 2
```powershell
Add-PnPNavigationNode -Title "Contoso USA" -Url "http://contoso.sharepoint.com/sites/contoso/usa/" -Location "QuickLaunch" -Parent 2012
```

Adds a navigation node to the quicklaunch. The navigation node will have the title "Contoso USA", will link to the url "http://contoso.sharepoint.com/sites/contoso/usa/" and will have the node with id 2012 as a parent navigation node.

### EXAMPLE 3
```powershell
Add-PnPNavigationNode -Title "Contoso" -Url "http://contoso.sharepoint.com/sites/contoso/" -Location "QuickLaunch" -First
```

Adds a navigation node to the quicklaunch, as the first item. The navigation node will have the title "Contoso" and will link to the url "http://contoso.sharepoint.com/sites/contoso/"

### EXAMPLE 4
```powershell
Add-PnPNavigationNode -Title "Contoso Pharmaceuticals" -Url "http://contoso.sharepoint.com/sites/contosopharma/" -Location "QuickLaunch" -External
```

Adds a navigation node to the quicklaunch. The navigation node will have the title "Contoso Pharmaceuticals" and will link to the external url "http://contoso.sharepoint.com/sites/contosopharma/"

### EXAMPLE 5
```powershell
Add-PnPNavigationNode -Title "Wiki" -Location "QuickLaunch" -Url "wiki/"
```

Adds a navigation node to the quicklaunch. The navigation node will have the title "Wiki" and will link to Wiki library on the selected Web.

### EXAMPLE 6
```powershell
Add-PnPNavigationNode -Title "Label" -Location "TopNavigationBar" -Url "http://linkless.header"
```

Adds a navigation node to the top navigation bar. The navigation node will be created as a label.

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

### -External
Indicates the destination URL is outside of the site collection

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -First
Add the new menu item to beginning of the collection

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Location
The location where to add the navigation node to. Either TopNavigationBar, QuickLaunch, SearchNav or Footer.

```yaml
Type: NavigationType
Parameter Sets: (All)
Accepted values: TopNavigationBar, QuickLaunch, SearchNav, Footer

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parent
The key of the parent. Leave empty to add to the top level

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the node to add

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The url to navigate to when clicking the new menu item. This can either be absolute or relative to the Web. Fragments are not supported.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


