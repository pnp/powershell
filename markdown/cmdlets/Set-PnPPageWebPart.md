---
Module Name: PnP.PowerShell
title: Set-PnPPageWebPart
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPageWebPart.html
---
 
# Set-PnPPageWebPart

## SYNOPSIS
Sets web part properties.

## SYNTAX

```powershell
Set-PnPPageWebPart -Page <PagePipeBind> -Identity <WebPartPipeBind>
 [-Title <String>] [-PropertiesJson <String>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Sets specific client side web part properties. Notice that the title parameter will only set the -internal- title of web part. The title which is shown in the UI will, if possible, have to be set using the PropertiesJson parameter. Use Get-PnPPageComponent to retrieve the instance id and properties of a web part.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPPageWebPart -Page Home -Identity a2875399-d6ff-43a0-96da-be6ae5875f82 -PropertiesJson "`"Property1`"=`"Value1`""
```

Sets the properties of the client side web part.

### EXAMPLE 2
```powershell
Set-PnPPageWebPart -Page Home -Identity a2875399-d6ff-43a0-96da-be6ae5875f82 -PropertiesJson $myproperties
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
The identity of the web part. This can be the web part instance id or the title of a web part.

```yaml
Type: WebPartPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Page
The name of the page.

```yaml
Type: PagePipeBind
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



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

