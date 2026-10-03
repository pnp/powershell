---
Module Name: PnP.PowerShell
title: Remove-PnPWebPart
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPWebPart.html
---
 
# Remove-PnPWebPart

## SYNOPSIS
Removes a web part from a page

## SYNTAX

### ID
```powershell
Remove-PnPWebPart -Identity <Guid> -ServerRelativePageUrl <String> 
 [-Connection <PnPConnection>] 
```

### NAME
```powershell
Remove-PnPWebPart -Title <String> -ServerRelativePageUrl <String> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove web part from a page.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPWebPart -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Identity a2875399-d6ff-43a0-96da-be6ae5875f82
```

This will remove the web part specified by ID to the specified page in the first row and the first column of the HTML table present on the page

### EXAMPLE 2
```powershell
Remove-PnPWebPart -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Title MyWebpart
```

This will remove the web part specified by title to the specified page in the first row and the first column of the HTML table present on the page

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
The Guid of the web part

```yaml
Type: Guid
Parameter Sets: ID

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativePageUrl
Server relative url of the web part page, e.g. /sites/demo/sitepages/home.aspx

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

### -Title
The name of the web part

```yaml
Type: String
Parameter Sets: NAME
Aliases: Name

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

