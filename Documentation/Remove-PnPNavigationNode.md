---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpnavigationnode
schema: 2.0.0
title: Remove-PnPNavigationNode
---

# Remove-PnPNavigationNode

## SYNOPSIS
Removes a menu item from either the quicklaunch or top navigation

## SYNTAX

### Remove a node by ID (Default)
```
Remove-PnPNavigationNode [-Identity] <NavigationNodePipeBind> [-Force] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Remove node by Title
```
Remove-PnPNavigationNode [-Location] <NavigationType> -Title <String> [-Header <String>] [-Force]
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### All Nodes
```
Remove-PnPNavigationNode [-All] [-Force] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPNavigationNode -Identity 1032
```

Removes the navigation node with the specified id

### EXAMPLE 2
```powershell
Get-PnPNavigationNode -Location Footer | Select-Object -First 1 | Remove-PnPNavigationNode -Force
```

Removes the first node of the footer navigation without asking for confirmation

### EXAMPLE 3
```powershell
Remove-PnPNavigationNode -Title Recent -Location QuickLaunch
```

Will remove the recent navigation node from the quick launch in the current web after confirmation has been given that it should be deleted

### EXAMPLE 4
```powershell
Remove-PnPNavigationNode -Title Home -Location TopNavigationBar -Force
```

Will remove the home navigation node from the top navigation bar without prompting for a confirmation in the current web

### EXAMPLE 5
```powershell
Get-PnPNavigationNode -Location QuickLaunch | Remove-PnPNavigationNode -Force
```

Will remove all the navigation nodes from the quick launch bar without prompting for a confirmation in the current web

## PARAMETERS

### -All
Specifying the All parameter will remove all the nodes from specified Location.

```yaml
Type: SwitchParameter
Parameter Sets: All Nodes
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

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
Aliases:

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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Header
{{ Fill Header Description }}

```yaml
Type: String
Parameter Sets: Remove node by Title
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Id or node object to delete

```yaml
Type: NavigationNodePipeBind
Parameter Sets: Remove a node by ID
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Location
{{ Fill Location Description }}

```yaml
Type: NavigationType
Parameter Sets: Remove node by Title
Aliases:
Accepted values: TopNavigationBar, QuickLaunch, SearchNav, Footer

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
{{ Fill Title Description }}

```yaml
Type: String
Parameter Sets: Remove node by Title
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

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

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)