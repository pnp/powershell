---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPageSection.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPageSection
---
  
# Add-PnPPageSection

## SYNOPSIS
Adds a new section to a page.

## SYNTAX

```powershell
Add-PnPPageSection [-Page] <PagePipeBind> -SectionTemplate <CanvasSectionTemplate>
 [-Order <Int32>] [-ZoneEmphasis <Int32>] [-VerticalZoneEmphasis <Int32>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to add a new section to a page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPageSection -Page "MyPage" -SectionTemplate OneColumn
```

Adds a new one-column section to the page 'MyPage'

### EXAMPLE 2
```powershell
Add-PnPPageSection -Page "MyPage" -SectionTemplate ThreeColumn -Order 10
```

Adds a new Three columns section to the page 'MyPage' with an order index of 10.

### EXAMPLE 3
```powershell
$page = Add-PnPPage -Name "MyPage"
Add-PnPPageSection -Page $page -SectionTemplate OneColumn
```

Adds a new one column section to the page 'MyPage'.

### EXAMPLE 4
```powershell
$page = Add-PnPPage -Name "MyPage"
Add-PnPPageSection -Page $page -SectionTemplate OneColumn -ZoneEmphasis 2
```

Adds a new one column section to the page 'MyPage' and sets the background to 2 (0 is no background, 3 is highest emphasis).

### EXAMPLE 5
```powershell
$page = Add-PnPPage -Name "MyPage"
Add-PnPPageSection -Page $page -SectionTemplate OneColumnVerticalSection -Order 1 -ZoneEmphasis 2 -VerticalZoneEmphasis 3
```

Adds a new one column with one vertical section to the page 'MyPage' and sets the zone emphasis to 2 for one column and vertical zone emphasis to 3 for the vertical column.


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

### -Order
Sets the order of the section. (Default = 1)

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
The name of the page or the page object.

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SectionTemplate
Specifies the columns template to use for the section.

```yaml
Type: CanvasSectionTemplate
Parameter Sets: (All)
Accepted values: OneColumn, OneColumnFullWidth, TwoColumn, ThreeColumn, TwoColumnLeft, TwoColumnRight, OneColumnVerticalSection, TwoColumnVerticalSection, ThreeColumnVerticalSection, TwoColumnLeftVerticalSection, TwoColumnRightVerticalSection

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ZoneEmphasis
Sets the background of the section (default = 0).

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VerticalZoneEmphasis
Sets the background of the vertical section (default = 0).
Works only for vertical column layouts, will be ignored for other layouts.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
