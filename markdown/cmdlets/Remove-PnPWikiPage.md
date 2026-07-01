---
Module Name: PnP.PowerShell
title: Remove-PnPWikiPage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPWikiPage.html
---
 
# Remove-PnPWikiPage

## SYNOPSIS
Removes a wiki page.

## SYNTAX

### SERVER
```powershell
Remove-PnPWikiPage [-ServerRelativePageUrl] <String> [-Connection <PnPConnection>]
 
```

### SITE
```powershell
Remove-PnPWikiPage [-SiteRelativePageUrl] <String> [-Connection <PnPConnection>]
 
```

## DESCRIPTION
This cmdlet removes a single wiki page specified by server relative url or site relative url.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPWikiPage -PageUrl '/pages/wikipage.aspx'
```

Removes the page '/pages/wikipage.aspx'.

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

### -ServerRelativePageUrl
Specifies the wiki page url based on the server relative url.

```yaml
Type: String
Parameter Sets: SERVER
Aliases: PageUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteRelativePageUrl
Specifies the wiki page url based on the site relative url.

```yaml
Type: String
Parameter Sets: SITE

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

