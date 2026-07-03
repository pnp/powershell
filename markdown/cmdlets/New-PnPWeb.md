---
Module Name: PnP.PowerShell
title: New-PnPWeb
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPWeb.html
---
 
# New-PnPWeb

## SYNOPSIS
Creates a new subweb under the current web

## SYNTAX

```powershell
New-PnPWeb -Title <String> -Url <String> [-Description <String>] [-Locale <Int32>] -Template <String>
 [-BreakInheritance] [-InheritNavigation] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to create new subweb under the current web.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPWeb -Title "Project A Web" -Url projectA -Description "Information about Project A" -Locale 1033 -Template "STS#0"
```

Creates a new subweb under the current web with URL projectA

## PARAMETERS

### -BreakInheritance
By default the subweb will inherit its security from its parent, specify this switch to break this inheritance

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Description
The description of the new web

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InheritNavigation
Specifies whether the site inherits navigation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Locale
The language id of the new web. default = 1033 for English

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
The site definition template to use for the new web, e.g. STS#0. Use Get-PnPWebTemplates to fetch a list of available templates

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the new web

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The URL of the new web

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

