---
Module Name: PnP.PowerShell
title: Set-PnPWebTheme
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPWebTheme.html
---
 
# Set-PnPWebTheme

## SYNOPSIS
Sets the theme of the current web.

## SYNTAX

```powershell
Set-PnPWebTheme [[-Theme] <ThemePipeBind>] [-WebUrl <String>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Sets the theme of the current web. * Requires SharePoint Online Administrator Rights *

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPWebTheme -Theme MyTheme
```

Sets the theme named "MyTheme" to the current web.

### EXAMPLE 2
```powershell
Get-PnPTenantTheme -Name "MyTheme" | Set-PnPWebTheme
```

Sets the theme named "MyTheme" to the current web.

### EXAMPLE 3
```powershell
Set-PnPWebTheme -Theme "MyCompanyTheme" -WebUrl https://contoso.sharepoint.com/sites/MyWeb
```

Sets the theme named "MyCompanyTheme" to MyWeb

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

### -Theme
Specifies the Color Palette Url based on the site or server relative url

```yaml
Type: ThemePipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



### -WebUrl
The URL of the web to apply the theme to. If not specified it will default to the current web based upon the URL specified with Connect-PnPOnline.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

