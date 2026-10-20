---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPJavaScriptLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPJavaScriptLink
---
  
# Get-PnPJavaScriptLink

## SYNOPSIS
Returns all or a specific custom action(s) with location type ScriptLink

## SYNTAX

```powershell
Get-PnPJavaScriptLink [[-Name] <String>] [-Scope <CustomActionScope>] [-ThrowExceptionIfJavaScriptLinkNotFound]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPJavaScriptLink
```

Returns all web scoped JavaScript links

### EXAMPLE 2
```powershell
Get-PnPJavaScriptLink -Scope All
```

Returns all web and site scoped JavaScript links

### EXAMPLE 3
```powershell
Get-PnPJavaScriptLink -Scope Web
```

Returns all Web scoped JavaScript links

### EXAMPLE 4
```powershell
Get-PnPJavaScriptLink -Scope Site
```

Returns all Site scoped JavaScript links

### EXAMPLE 5
```powershell
Get-PnPJavaScriptLink -Name Test
```

Returns the web scoped JavaScript link named Test

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
Name of the Javascript link. Omit this parameter to retrieve all script links

```yaml
Type: String
Parameter Sets: (All)
Aliases: Key

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scope
Scope of the action, either Web, Site or All to return both, defaults to Web

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

### -ThrowExceptionIfJavaScriptLinkNotFound
Switch parameter if an exception should be thrown if the requested JavaScriptLink does not exist (true) or if omitted, nothing will be returned in case the JavaScriptLink does not exist

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


