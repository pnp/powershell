---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/get-pnpwikipagecontent
schema: 2.0.0
title: Get-PnPWikiPageContent
---

# Get-PnPWikiPageContent

## SYNOPSIS
Gets the contents/source of a wiki page

## SYNTAX

```powershell
Get-PnPWikiPageContent [-ServerRelativePageUrl] <String> [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPWikiPageContent -PageUrl '/sites/demo1/pages/wikipage.aspx'
```

Gets the content of the page '/sites/demo1/pages/wikipage.aspx'

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
The server relative URL for the wiki page

```yaml
Type: String
Parameter Sets: (All)
Aliases: PageUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)