---
Module Name: PnP.PowerShell
title: Get-PnPTheme
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTheme.html
---
 
# Get-PnPTheme

## SYNOPSIS
Returns the current theme/composed look of the current web.

## SYNTAX

```powershell
Get-PnPTheme [-DetectCurrentComposedLook] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to retrieve current theme/composed look of the current web.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTheme
```

Returns the current composed look of the current web.

### EXAMPLE 2
```powershell
Get-PnPTheme -DetectCurrentComposedLook
```

Returns the current composed look of the current web, and will try to detect the currently applied composed look based upon the actual site. Without this switch the cmdlet will first check for the presence of a property bag variable called _PnP_SiteTemplateComposedLookInfo that contains composed look information when applied through the provisioning engine or the Set-PnPTheme cmdlet.

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

### -DetectCurrentComposedLook
Specify this switch to not use the PnP Provisioning engine based composed look information but try to detect the current composed look as is.

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

