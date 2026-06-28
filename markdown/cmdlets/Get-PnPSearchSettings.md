---
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchSettings.html
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
applicable: SharePoint Online
title: Get-PnPSearchSettings
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
  
# Get-PnPSearchSettings

## SYNOPSIS
Retrieves search settings for a site

## SYNTAX

```powershell
Get-PnPSearchSettings [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve search settings for a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchSettings
```

Retrieve search settings for the site

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


