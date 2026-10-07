---
Module Name: PnP.PowerShell
title: Invoke-PnPWebAction
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPWebAction.html
---
 
# Invoke-PnPWebAction

## SYNOPSIS
Executes operations on web, lists and list items.

## SYNTAX

```powershell
Invoke-PnPWebAction [-ListName <String>] [-Webs <Web[]>]
 [-WebAction <System.Action`1[Microsoft.SharePoint.Client.Web]>]
 [-ShouldProcessWebAction <System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]>]
 [-PostWebAction <System.Action`1[Microsoft.SharePoint.Client.Web]>]
 [-ShouldProcessPostWebAction <System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]>]
 [-WebProperties <String[]>] [-ListAction <System.Action`1[Microsoft.SharePoint.Client.List]>]
 [-ShouldProcessListAction <System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]>]
 [-PostListAction <System.Action`1[Microsoft.SharePoint.Client.List]>]
 [-ShouldProcessPostListAction <System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]>]
 [-ListProperties <String[]>] [-ListItemAction <System.Action`1[Microsoft.SharePoint.Client.ListItem]>]
 [-ShouldProcessListItemAction <System.Func`2[Microsoft.SharePoint.Client.ListItem,System.Boolean]>]
 [-ListItemProperties <String[]>] [-SubWebs] [-DisableStatisticsOutput] [-SkipCounting] 
 [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to execute operations on web, lists and list items.

## EXAMPLES

### EXAMPLE 1
```powershell
Invoke-PnPWebAction -ListAction ${function:ListAction}
```

This will call the function ListAction on all the lists located on the current web.

### EXAMPLE 2
```powershell
Invoke-PnPWebAction -ShouldProcessListAction ${function:ShouldProcessList} -ListAction ${function:ListAction}
```

This will call the function ShouldProcessList, if it returns true the function ListAction will then be called. This will occur on all lists located on the current web

## PARAMETERS

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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableStatisticsOutput
Will not output statistics after the operation

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListAction
Function to be executed on the list. There is one input parameter of type List

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.List]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListItemAction
Function to be executed on the list item. There is one input parameter of type ListItem

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.ListItem]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListItemProperties
The properties to load for list items.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListName
{{ Fill ListName Description }}

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListProperties
The properties to load for list.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PostListAction
Function to be executed on the list, this will trigger after list items have been processed. There is one input parameter of type List

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.List]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PostWebAction
Function to be executed on the web, this will trigger after lists and list items have been processed. There is one input parameter of type Web

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.Web]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldProcessListAction
Function to be executed on the web that would determine if ListAction should be invoked, There is one input parameter of type List and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldProcessListItemAction
Function to be executed on the web that would determine if ListItemAction should be invoked, There is one input parameter of type ListItem and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.ListItem,System.Boolean]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldProcessPostListAction
Function to be executed on the web that would determine if PostListAction should be invoked, There is one input parameter of type List and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.List,System.Boolean]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldProcessPostWebAction
Function to be executed on the web that would determine if PostWebAction should be invoked, There is one input parameter of type Web and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldProcessWebAction
Function to be executed on the web that would determine if WebAction should be invoked, There is one input parameter of type Web and the function should return a boolean value

```yaml
Type: System.Func`2[Microsoft.SharePoint.Client.Web,System.Boolean]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipCounting
Will skip the counting process; by doing this you will not get an estimated time remaining

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubWebs
Specify if sub webs will be processed

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



### -WebAction
Function to be executed on the web. There is one input parameter of type Web

```yaml
Type: System.Action`1[Microsoft.SharePoint.Client.Web]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebProperties
The properties to load for web.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Webs
Webs you want to process (for example different site collections), will use Web parameter if not specified

```yaml
Type: Web[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

