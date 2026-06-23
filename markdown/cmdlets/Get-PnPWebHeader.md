---
Module Name: PnP.PowerShell
title: Get-PnPWebHeader
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPWebHeader.html
---
 
# Get-PnPWebHeader

## SYNOPSIS
Retrieves the current configuration regarding the "Change the look" Header of the current site

## SYNTAX

```powershell
Get-PnPWebHeader [-SiteLogoUrl <string>] [-HeaderLayout <HeaderLayoutType>] [-HeaderEmphasis <SPVariantThemeType>] [-HideTitleInHeader]
[-HeaderBackgroundImageUrl <string>] [-HeaderBackgroundImageFocalX <double>] [-HeaderBackgroundImageFocalY <double>] [-LogoAlignment <LogoAlignment>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Through this cmdlet the current configuration of the various options offered through "Change the look" Header can be retrieved.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPWebHeader
```

Retrieves all of the available configuration

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)