---
Module Name: PnP.PowerShell
title: Get-PnPNavigationNode
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPNavigationNode.html
---
 
# Get-PnPNavigationNode

## SYNOPSIS
Returns all or a specific navigation node

## SYNTAX

### All nodes by location (Default)
```powershell
Get-PnPNavigationNode [-Location <NavigationType>] [-Tree] [-Connection <PnPConnection>]
 
```

### A single node by ID
```powershell
Get-PnPNavigationNode [-Id <Int32>] [-Tree] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to retrieve all navigation nodes or a specific on by using `Id` option.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPNavigationNode
```

Returns all navigation nodes in the quicklaunch navigation

### EXAMPLE 2
```powershell
Get-PnPNavigationNode -Location QuickLaunch
```

Returns all navigation nodes in the quicklaunch navigation

### EXAMPLE 3
```powershell
Get-PnPNavigationNode -Location TopNavigationBar
```

Returns all navigation nodes in the top navigation bar

### EXAMPLE 4
```powershell
$node = Get-PnPNavigationNode -Id 2030
PS> $children = $node.Children
```

Returns the selected navigation node and retrieves any children

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

### -Id
The Id of the node to retrieve

```yaml
Type: Int32
Parameter Sets: A single node by ID

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Location
The location of the nodes to retrieve. Either TopNavigationBar, QuickLaunch, SearchNav or Footer.

```yaml
Type: NavigationType
Parameter Sets: All nodes by location
Accepted values: TopNavigationBar, QuickLaunch, SearchNav, Footer

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tree
Show a tree view of all navigation nodes

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

