---
title: Get-PnPHomePage
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPHomePage.html
Module Name: PnP.PowerShell
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
---
   
# Get-PnPHomePage

## SYNOPSIS
Return the homepage

## SYNTAX

```powershell
Get-PnPHomePage [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns the URL to the page set as home page

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPHomePage
```

Will return the URL of the home page of the web.

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



