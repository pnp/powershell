---
Module Name: PnP.PowerShell
title: Remove-PnPNavigationNode
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPNavigationNode.html
---
 
# Remove-PnPNavigationNode

## SYNOPSIS
Removes a menu item from either the quick launch or top navigation.

## SYNTAX

### Remove a node by ID (Default)
```powershell
Remove-PnPNavigationNode [-Identity] <NavigationNodePipeBind> [-Force] 
 [-Connection <PnPConnection>]
```

### Remove node by Title
```powershell
Remove-PnPNavigationNode [-Location] <NavigationType> -Title <String> [-Header <String>] [-Force]
 [-Connection <PnPConnection>]
```

### All Nodes
```powershell
Remove-PnPNavigationNode [-All] [-Force] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove menu item from either the quick launch or top navigation.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPNavigationNode -Identity 1032
```

Removes the navigation node with the specified id.

### EXAMPLE 2
```powershell
Get-PnPNavigationNode -Location Footer | Select-Object -First 1 | Remove-PnPNavigationNode -Force
```

Removes the first node of the footer navigation without asking for confirmation.

### EXAMPLE 3
```powershell
Remove-PnPNavigationNode -Title Recent -Location QuickLaunch
```

Removes the recent navigation node from the quick launch in the current web after confirmation has been given that it should be deleted.

### EXAMPLE 4
```powershell
Remove-PnPNavigationNode -Title Home -Location TopNavigationBar -Force
```

Removes the home navigation node from the top navigation bar in the current web without prompting for a confirmation.

### EXAMPLE 5
```powershell
Get-PnPNavigationNode -Location QuickLaunch | Remove-PnPNavigationNode -Force
```

Removes all the navigation nodes from the quick launch bar in the current web without prompting for a confirmation.

## PARAMETERS

### -All
Specifying the All parameter will remove all the nodes from specified Location.

```yaml
Type: SwitchParameter
Parameter Sets: All Nodes

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

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Header
Obsolete.

```yaml
Type: String
Parameter Sets: Remove node by Title

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Id or node object to delete.

```yaml
Type: NavigationNodePipeBind
Parameter Sets: Remove a node by ID

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Location
Obsolete.

```yaml
Type: NavigationType
Parameter Sets: Remove node by Title
Accepted values: TopNavigationBar, QuickLaunch, SearchNav, Footer

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Obsolete.

```yaml
Type: String
Parameter Sets: Remove node by Title

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

