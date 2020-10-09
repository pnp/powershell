---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpclientsidetext
schema: 2.0.0
title: Add-PnPClientSideText
---

# Add-PnPClientSideText

## SYNOPSIS
Adds a text element to a client-side page.

## SYNTAX

### Default
```powershell
Add-PnPClientSideText [-Page] <ClientSidePagePipeBind> -Text <String> [-Order <Int32>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Positioned
```powershell
Add-PnPClientSideText [-Page] <ClientSidePagePipeBind> -Text <String> [-Order <Int32>] -Section <Int32>
 -Column <Int32> [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Adds a new text element to a section on a client-side page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPClientSideText -Page "MyPage" -Text "Hello World!"
```

Adds the text 'Hello World!' to the Client-Side Page 'MyPage'

## PARAMETERS

### -Column
Sets the column where to insert the text control.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: Int32
Parameter Sets: Positioned

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Order
Sets the order of the text control. (Default = 1)

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Page
The name of the page.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: ClientSidePagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Section
Sets the section where to insert the text control.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: Int32
Parameter Sets: Positioned

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Text
Specifies the text to display in the text area.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

Only applicable to: SharePoint Online, SharePoint Server 2019

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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)