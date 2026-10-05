---
Module Name: PnP.PowerShell
title: Get-PnPSearchSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchSettings.html
---
 
# Get-PnPSearchSettings

## SYNOPSIS
Retrieves search settings for a site, including whether the site collection has opted out of Copilot Search.

## SYNTAX

```powershell
Get-PnPSearchSettings [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve search settings for a site. The returned object includes the site collection-level `CopilotSearchOptOut` setting. If the setting is not available yet on the tenant, its value is `$null` and a warning is shown.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchSettings
```

Retrieve search settings for the site

### EXAMPLE 2
```powershell
(Get-PnPSearchSettings).CopilotSearchOptOut
```

Returns whether the current site collection has opted out of Copilot Search.

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
