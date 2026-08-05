---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPageTextPart.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPageTextPart
---
  
# Add-PnPPageTextPart

## SYNOPSIS
Adds a text element to a client-side page.

## SYNTAX

### Default
```powershell
Add-PnPPageTextPart -Page <PagePipeBind> -Text <String> [-Order <Int32>] [-ImageUrl <String>] [-PageImageAlignment <PageImageAlignment>] [-ImageWidth <Int32>] [-ImageHeight <Int32>] [-Connection <PnPConnection>]
```

### Positioned
```powershell
Add-PnPPageTextPart -Page <PagePipeBind> -Text <String> -Section <Int32> -Column <Int32> [-Order <Int32>] [-ImageUrl <String>]
[-PageImageAlignment <PageImageAlignment>] [-ImageWidth <Int32>] [-ImageHeight <Int32>] [-Connection <PnPConnection>]
```

## DESCRIPTION
Adds a new text element to a section on a client-side page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPageTextPart -Page "MyPage" -Text "Hello World!"
```

Adds the text 'Hello World!' to the Page 'MyPage'

### EXAMPLE 2
```powershell
Add-PnPPageTextPart -Page "MyPage" -Text "Hello World!" -ImageUrl "/sites/contoso/SiteAssets/test.png"
```

Adds the text 'Hello World!' to the Page 'MyPage' with specified image as inline image.

### EXAMPLE 3
```powershell
Add-PnPPageTextPart -Page "MyPage" -Text "Hello World!" -ImageUrl "/sites/contoso/SiteAssets/test.png" -TextBeforeImage "Text before" -TextAfterImage "Text after"
```

Adds the text 'Hello World!' to the Page 'MyPage' with specified image as inline image with text specified before and after the inline image.


## PARAMETERS

### -Column
Sets the column where to insert the text control.

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
Sets the order of the text control. (Default = 1)

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
Sets the section where to insert the text control.

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

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TextBeforeImage
Specifies the text to display before the inline image.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: 150
Accept pipeline input: False
Accept wildcard characters: False
```

### -ImageUrl
Specifies the inline image to be added. Image will be added after the text content.

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

### -TextAfterImage
Specifies the text to display after the inline image.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: 150
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