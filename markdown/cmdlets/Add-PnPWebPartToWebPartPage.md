---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPWebPartToWebPartPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPWebPartToWebPartPage
---
  
# Add-PnPWebPartToWebPartPage

## SYNOPSIS
Adds a web part to a web part page in a specified zone

## SYNTAX

### XML
```powershell
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl <String> -Xml <String> -ZoneId <String> -ZoneIndex <Int32>
 [-Connection <PnPConnection>] 
```

### FILE
```powershell
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl <String> -Path <String> -ZoneId <String> -ZoneIndex <Int32>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a web part to a web part page. Use the `ZoneIndex` option to specify the zone.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Path "c:\myfiles\listview.webpart" -ZoneId "Header" -ZoneIndex 1
```

This will add the web part as defined by the XML in the listview.webpart file to the specified page in the specified zone and with the order index of 1

### EXAMPLE 2
```powershell
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -XML $webpart -ZoneId "Header" -ZoneIndex 1
```

This will add the web part as defined by the XML in the $webpart variable to the specified page in the specified zone and with the order index of 1

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

### -Path
A path to a web part file on a the file system.

```yaml
Type: String
Parameter Sets: FILE

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativePageUrl
Server Relative Url of the page to add the web part to.

```yaml
Type: String
Parameter Sets: (All)
Aliases: PageUrl

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



### -Xml
A string containing the XML for the web part.

```yaml
Type: String
Parameter Sets: XML

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ZoneId
The Zone Id where the web part must be placed

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ZoneIndex
The Zone Index where the web part must be placed

```yaml
Type: Int32
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


