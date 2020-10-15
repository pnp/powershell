---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/move-pnpclientsidecomponent
schema: 2.0.0
title: Move-PnPClientSideComponent
---

# Move-PnPClientSideComponent

## SYNOPSIS
Moves a Client-Side Component to a different section/column

## SYNTAX

### Move to other section
```powershell
Move-PnPClientSideComponent [-Page] <ClientSidePagePipeBind> -InstanceId <Guid> -Section <Int32>
 [-Position <Int32>] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Move to other section and column
```powershell
Move-PnPClientSideComponent [-Page] <ClientSidePagePipeBind> -InstanceId <Guid> -Section <Int32>
 -Column <Int32> [-Position <Int32>] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Move to other column
```powershell
Move-PnPClientSideComponent [-Page] <ClientSidePagePipeBind> -InstanceId <Guid> -Column <Int32>
 [-Position <Int32>] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Move within a column
```powershell
Move-PnPClientSideComponent [-Page] <ClientSidePagePipeBind> -InstanceId <Guid> -Position <Int32>
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Moves a Client-Side Component to a different location on the page. Notice that the sections and or columns need to be present before moving the component.

## EXAMPLES

### EXAMPLE 1
```powershell
Move-PnPClientSideComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Section 1
```

Moves the specified component to the first section of the page.

### EXAMPLE 2
```powershell
Move-PnPClientSideComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Column 2
```

Moves the specified component to the second column of the current section.

### EXAMPLE 3
```powershell
Move-PnPClientSideComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Section 1 -Column 2
```

Moves the specified component to the first section of the page into the second column.

### EXAMPLE 4
```powershell
Move-PnPClientSideComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Section 1 -Column 2 -Position 2
```

Moves the specified component to the first section of the page into the second column and sets the column to position 2 in the list of webparts.

## PARAMETERS

### -Column
The column to move the web part to

```yaml
Type: Int32
Parameter Sets: Move to other section and column, Move to other column

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

### -InstanceId
The instance id of the control. Use Get-PnPClientSideControl retrieve the instance ids.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Page
The name of the page

```yaml
Type: ClientSidePagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Position
Change to order of the web part in the column

```yaml
Type: Int32
Parameter Sets: Move to other section, Move to other section and column, Move to other column

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: Int32
Parameter Sets: Move within a column

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Section
The section to move the web part to

```yaml
Type: Int32
Parameter Sets: Move to other section, Move to other section and column

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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)