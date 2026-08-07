---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPJavaScriptLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPJavaScriptLink
---
  
# Add-PnPJavaScriptLink

## SYNOPSIS
Adds a link to a JavaScript file to a web or sitecollection, valid only for SharePoint classic site experience.

## SYNTAX

```powershell
Add-PnPJavaScriptLink -Name <String> -Url <String[]> [-Sequence <Int32>] [-Scope <CustomActionScope>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Creates a custom action that refers to a JavaScript file

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPJavaScriptLink -Name jQuery -Url https://code.jquery.com/jquery.min.js -Sequence 9999 -Scope Site
```

Injects a reference to the latest v1 series jQuery library to all pages within the current site collection under the name jQuery and at order 9999

### EXAMPLE 2
```powershell
Add-PnPJavaScriptLink -Name jQuery -Url https://code.jquery.com/jquery.min.js
```

Injects a reference to the latest v1 series jQuery library to all pages within the current web under the name jQuery

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

### -Name
Name under which to register the JavaScriptLink

```yaml
Type: String
Parameter Sets: (All)
Aliases: Key

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Defines if this JavaScript file will be injected to every page within the current site collection or web. All is not allowed in for this command. Default is web.

```yaml
Type: CustomActionScope
Parameter Sets: (All)
Accepted values: Web, Site, All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sequence
Sequence of this JavaScript being injected. Use when you have a specific sequence with which to have JavaScript files being added to the page. I.e. jQuery library first and then jQueryUI.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
URL to the JavaScript file to inject

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


