---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpnavigationnode
schema: 2.0.0
title: Get-PnPNavigationNode
---

# Get-PnPNavigationNode

## SYNOPSIS
Returns all or a specific navigation node

## SYNTAX

### All nodes by location (Default)
```
Get-PnPNavigationNode [-Location <NavigationType>] [-Tree] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### A single node by ID
```
Get-PnPNavigationNode [-Id <Int32>] [-Tree] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

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
Aliases:

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
Aliases:

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
Aliases:
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
Aliases:

Required: False
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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)