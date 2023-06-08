---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAvailableLanguage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAvailableLanguage
---
  
# Get-PnPAvailableLanguage

## SYNOPSIS
Returns the available languages on the current web

## SYNTAX

```powershell
Get-PnPAvailableLanguage [[-Identity] <WebPipeBind>] [-Connection <PnPConnection>] [-Includes <String[]>]
 
```

## DESCRIPTION

Allows to retrieve available languages from the current site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAvailableLanguage
```

This will return the available languages in the current web

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
The guid of the web or web object

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


