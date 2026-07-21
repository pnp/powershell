---
online version: https://pnp.github.io/powershell/cmdlets/Stop-PnPSiteContentMove.html
schema: 2.0.0
Module Name: PnP.PowerShell
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
title: Stop-PnPSiteContentMove
external help file: PnP.PowerShell.dll-Help.xml
---
 
# Stop-PnPSiteContentMove

## SYNOPSIS
Stops a SharePoint Online multi-geo site content move job.

## SYNTAX

```powershell
Stop-PnPSiteContentMove [-SourceSiteUrl] <String> [-Connection <PnPConnection>]
```

## DESCRIPTION
Stops a SharePoint Online multi-geo site content move job for the specified source site URL.

## EXAMPLES

### EXAMPLE 1

```powershell
Stop-PnPSiteContentMove -SourceSiteUrl https://contoso.sharepoint.com/sites/project
```

Stops the site content move job for the specified source site.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceSiteUrl
The URL of the source site whose site content move job should be stopped.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.String
Returns `The given move job has been stopped.` when the move job has been stopped.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

