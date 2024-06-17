---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteFileVersionBatchDeleteJobStatus.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSiteFileVersionBatchDeleteJobStatus
---
  
# Get-PnPSiteFileVersionBatchDeleteJobStatus

## SYNOPSIS
Get the progress of deleting existing file versions on the site.

## SYNTAX

```powershell
Get-PnPSiteFileVersionBatchDeleteJobStatus [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows retrieval of the progress of deleting existing file versions on the site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteFileVersionBatchDeleteJobStatus
```

Returns the progress of deleting existing file versions on the site.

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
