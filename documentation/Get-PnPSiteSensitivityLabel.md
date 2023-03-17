---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteSensitivityLabel.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSiteSensitivityLabel
---
  
# Get-PnPSiteSensitivityLabel

## SYNOPSIS
Gets the Microsoft Purview sensitivity label that is set on the connected to site

## SYNTAX

```powershell
Get-PnPSiteSensitivityLabel [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet allows retrieval of the currently assigned Microsoft Purview sensitivity label to the currently connected to site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteSensitivityLabel
```

Returns the Microsoft Purview sensitivity label set on the currently connected to site

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
[Microsoft Docs documentation](https://learn.microsoft.com/sharepoint/dev/solution-guidance/modern-experience-site-classification#programmatically-read-the-classification-of-a-site)