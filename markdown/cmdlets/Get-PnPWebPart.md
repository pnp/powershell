---
Module Name: PnP.PowerShell
title: Get-PnPWebPart
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPWebPart.html
---
 
# Get-PnPWebPart

## SYNOPSIS
Returns a web part definition object

## SYNTAX

```powershell
Get-PnPWebPart -ServerRelativePageUrl <String> [-Identity <WebPartPipeBind>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows retrieval of the definition of a webpart on a classic SharePoint Online page. 

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPWebPart -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx"
```

Returns all webparts defined on the given classic page.

### EXAMPLE 2
```powershell
Get-PnPWebPart -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Identity a2875399-d6ff-43a0-96da-be6ae5875f82
```

Returns a specific web part defined on the given classic page.

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
The identity of the web part, this can be the web part guid or a web part object

```yaml
Type: WebPartPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ServerRelativePageUrl
Full server relative URL of the web part page, e.g. /sites/mysite/sitepages/home.aspx

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
