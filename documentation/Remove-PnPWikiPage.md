---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/remove-pnpwikipage
schema: 2.0.0
title: Remove-PnPWikiPage
---

# Remove-PnPWikiPage

## SYNOPSIS
Removes a wiki page

## SYNTAX

### SERVER
```powershell
Remove-PnPWikiPage [-ServerRelativePageUrl] <String> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### SITE
```powershell
Remove-PnPWikiPage [-SiteRelativePageUrl] <String> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPWikiPage -PageUrl '/pages/wikipage.aspx'
```

Removes the page '/pages/wikipage.aspx'

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

```yaml
Type: String
Parameter Sets: SITE

Required: True
Position: 0
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