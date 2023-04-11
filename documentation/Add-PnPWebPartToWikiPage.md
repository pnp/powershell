---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPWebPartToWikiPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPWebPartToWikiPage
---
  
# Add-PnPWebPartToWikiPage

## SYNOPSIS
Adds a web part to a wiki page in a specified table row and column

## SYNTAX

### XML
```powershell
Add-PnPWebPartToWikiPage -ServerRelativePageUrl <String> -Xml <String> -Row <Int32> -Column <Int32> [-AddSpace]
 [-Connection <PnPConnection>] 
```

### FILE
```powershell
Add-PnPWebPartToWikiPage -ServerRelativePageUrl <String> -Path <String> -Row <Int32> -Column <Int32>
 [-AddSpace] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a web part a wiki page. Use the `Row` and `Column` option to specify the location of the web part.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPWebPartToWikiPage -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Path "c:\myfiles\listview.webpart" -Row 1 -Column 1
```

This will add the web part as defined by the XML in the listview.webpart file to the specified page in the first row and the first column of the HTML table present on the page

### EXAMPLE 2
```powershell
Add-PnPWebPartToWikiPage -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -XML $webpart -Row 1 -Column 1
```

This will add the web part as defined by the XML in the $webpart variable to the specified page in the first row and the first column of the HTML table present on the page

## PARAMETERS

### -AddSpace
Must there be a extra space between the web part

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Column
Column number where the web part must be placed

```yaml
Type: Int32
Parameter Sets: (All)

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

### -Row
Row number where the web part must be placed

```yaml
Type: Int32
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativePageUrl
Full server relative url of the web part page, e.g. /sites/demo/sitepages/home.aspx

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


