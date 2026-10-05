---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPWikiPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPWikiPage
---
  
# Add-PnPWikiPage

## SYNOPSIS
Adds a wiki page

## SYNTAX

### WithContent
```powershell
Add-PnPWikiPage -ServerRelativePageUrl <String> -Content <String> 
 [-Connection <PnPConnection>] 
```

### WithLayout
```powershell
Add-PnPWikiPage -ServerRelativePageUrl <String> -Layout <WikiPageLayout> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a wiki page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPWikiPage -PageUrl '/sites/demo1/pages/wikipage.aspx' -Content 'New WikiPage'
```

Creates a new wiki page '/sites/demo1/pages/wikipage.aspx' and sets the content to 'New WikiPage'

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

### -Content

```yaml
Type: String
Parameter Sets: WithContent

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Layout

```yaml
Type: WikiPageLayout
Parameter Sets: WithLayout
Accepted values: OneColumn, OneColumnSideBar, TwoColumns, TwoColumnsHeader, TwoColumnsHeaderFooter, ThreeColumns, ThreeColumnsHeader, ThreeColumnsHeaderFooter, Custom

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativePageUrl
The server relative page URL

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



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


