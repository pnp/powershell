---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpclientsidewebpart
schema: 2.0.0
title: Set-PnPClientSideWebPart
---

# Set-PnPClientSideWebPart

## SYNOPSIS
Set Client-Side Web Part properties

## SYNTAX

```powershell
Set-PnPClientSideWebPart [-Page] <ClientSidePagePipeBind> -Identity <ClientSideWebPartPipeBind>
 [-Title <String>] [-PropertiesJson <String>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
Sets specific client side web part properties. Notice that the title parameter will only set the -internal- title of web part. The title which is shown in the UI will, if possible, have to be set using the PropertiesJson parameter. Use Get-PnPClientSideComponent to retrieve the instance id and properties of a web part.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPClientSideWebPart -Page Home -Identity a2875399-d6ff-43a0-96da-be6ae5875f82 -PropertiesJson $myproperties
```

Sets the properties of the client side web part given in the $myproperties variable.

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

### -Identity
The identity of the web part. This can be the web part instance id or the title of a web part

```yaml
Type: ClientSideWebPartPipeBind
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

### -PropertiesJson
Sets the properties as a JSON string.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Title
Sets the internal title of the web part. Notice that this will NOT set a visible title.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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