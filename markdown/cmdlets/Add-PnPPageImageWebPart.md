---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPageImageWebPart.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPageImageWebPart
---
  
# Add-PnPPageImageWebPart

## SYNOPSIS
Adds an image element to a client-side page.

## SYNTAX

### Default
```powershell
Add-PnPPageImageWebPart [-Page <PagePipeBind>] [-ImageUrl <String>] [-Order <Int32>] [-ImageUrl <String>] [-PageImageAlignment <PageImageAlignment>] [-ImageWidth <Int32>] [-ImageHeight <Int32>] [-Caption <String>] [-AlternativeText <String>] [-Link <String>] [-Connection <PnPConnection>]
```

### Positioned
```powershell
Add-PnPPageImageWebPart [-Page <PagePipeBind>] [-ImageUrl <String>] [-Section <Int32>] [-Column <Int32>] [-Order <Int32>] [-PageImageAlignment <PageImageAlignment>] [-ImageWidth <Int32>] [-ImageHeight <Int32>] [-Caption <String>] [-AlternativeText <String>] [-Link <String>] [-Connection <PnPConnection>]
```

## DESCRIPTION
Adds a new image element to a section on a client-side page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPageImageWebPart -Page "MyPage" -ImageUrl "/sites/contoso/siteassets/test.png"
```

Adds the image with specified URL to the Page 'MyPage'

### EXAMPLE 2
```powershell
Add-PnPPageImageWebPart -Page "MyPage" -ImageUrl "/sites/contoso/SiteAssets/test.png" -ImageWidth 400 -ImageHeight 200 -Caption "Caption text" -AlternativeText "Alt text" -Link "https://pnp.github.io"
```

Adds the image with specified URL to the Page 'MyPage' with width, height, caption, alt text and link parameters.


## PARAMETERS

### -Column
Sets the column where to insert the image control.

```yaml
Type: Int32
Parameter Sets: Positioned

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Order
Sets the order of the image control. (Default = 1)

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

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Section
Sets the section where to insert the image control.

```yaml
Type: Int32
Parameter Sets: Positioned

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ImageUrl
Specifies the image to be added.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageImageAlignment
Specifies the inline image's alignment. Available values are Center, Left and Right.

```yaml
Type: PageImageAlignment
Parameter Sets: (All)

Required: False
Position: Named
Default value: Center
Accept pipeline input: False
Accept wildcard characters: False
```

### -ImageWidth
Specifies the width of the inline image.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: 150
Accept pipeline input: False
Accept wildcard characters: False
```

### -ImageHeight
Specifies the height of the inline image.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: 150
Accept pipeline input: False
Accept wildcard characters: False
```

### -Caption
Specifies the caption text to display for the image.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AlternativeText
Specifies the alt text to display for the image.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Link
Specifies the clickable link to display for the image.

```yaml
Type: String
Parameter Sets: (All)

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)