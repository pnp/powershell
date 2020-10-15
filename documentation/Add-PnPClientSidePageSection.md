---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpclientsidepagesection
schema: 2.0.0
title: Add-PnPClientSidePageSection
---

# Add-PnPClientSidePageSection

## SYNOPSIS
Adds a new section to a Client-Side page

## SYNTAX

```powershell
Add-PnPClientSidePageSection [-Page] <ClientSidePagePipeBind> -SectionTemplate <CanvasSectionTemplate>
 [-Order <Int32>] [-ZoneEmphasis <Int32>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPClientSidePageSection -Page "MyPage" -SectionTemplate OneColumn
```

Adds a new one-column section to the Client-Side page 'MyPage'

### EXAMPLE 2
```powershell
Add-PnPClientSidePageSection -Page "MyPage" -SectionTemplate ThreeColumn -Order 10
```

Adds a new Three columns section to the Client-Side page 'MyPage' with an order index of 10

### EXAMPLE 3
```powershell
$page = Add-PnPClientSidePage -Name "MyPage"
PS> Add-PnPClientSidePageSection -Page $page -SectionTemplate OneColumn
```

Adds a new one column section to the Client-Side page 'MyPage'

### EXAMPLE 4
```powershell
$page = Add-PnPClientSidePage -Name "MyPage"
PS> Add-PnPClientSidePageSection -Page $page -SectionTemplate OneColumn -ZoneEmphasis 2
```

Adds a new one column section to the Client-Side page 'MyPage' and sets the background to 2 (0 is no background, 3 is highest emphasis)

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

### -ZoneEmphasis
Sets the background of the section (default = 0)

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