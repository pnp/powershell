---
Module Name: PnP.PowerShell
title: Get-PnPWebPartXml
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPWebPartXml.html
---
 
# Get-PnPWebPartXml

## SYNOPSIS
Returns the web part XML of a web part registered on a site

## SYNTAX

```powershell
Get-PnPWebPartXml -ServerRelativePageUrl <String> -Identity <WebPartPipeBind> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve web part XML defintion.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPWebPartXml -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Identity a2875399-d6ff-43a0-96da-be6ae5875f82
```

Returns the web part XML for a given web part on a page.

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

### -Identity
Id or title of the web part. Use Get-PnPWebPart to retrieve all web part Ids

```yaml
Type: WebPartPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativePageUrl
Full server relative url of the web part page, e.g. /sites/mysite/sitepages/home.aspx

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

