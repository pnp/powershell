---
Module Name: PnP.PowerShell
title: Set-PnPHomePage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPHomePage.html
---
 
# Set-PnPHomePage

## SYNOPSIS
Sets the home page of the current web.

## SYNTAX

```powershell
Set-PnPHomePage [-RootFolderRelativeUrl] <String> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to set the home page of the current site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPHomePage -RootFolderRelativeUrl SitePages/Home.aspx
```

Sets the home page to the home.aspx file which resides in the SitePages library

### EXAMPLE 2
```powershell
Set-PnPHomePage -RootFolderRelativeUrl Lists/Sample/AllItems.aspx
```

Sets the home page to be the Sample list

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

### -RootFolderRelativeUrl
The root folder relative url of the homepage, e.g. 'sitepages/home.aspx'

```yaml
Type: String
Parameter Sets: (All)
Aliases: Path

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

