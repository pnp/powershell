---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSiteFileVersionBatchDeleteJob.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPSiteFileVersionBatchDeleteJob
---
  
# New-PnPSiteFileVersionBatchDeleteJob

## SYNOPSIS

Starts a file version batch trim job targeting all document libraries in a site collection.


## SYNTAX

```powershell
New-PnPSiteFileVersionBatchDeleteJob -DeleteBeforeDays <int> [-Force]
```


## DESCRIPTION

Starts a file version batch trim job targeting all document libraries in a site collection.


## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSiteFileVersionBatchDeleteJob -DeleteBeforeDays 360
```

Starts a file version batch trim job that will delete all file verions that are over 360 days old in all document libraries in the site collection.

### EXAMPLE 2
```powershell
New-PnPSiteFileVersionBatchDeleteJob -DeleteBeforeDays 360 -Force
```

Starts a file version batch trim job that will delete all file verions that are over 360 days old in all document libraries in the site collection, without prompting the user for confirmation.


## PARAMETERS

### -DeleteBeforeDays
The minimum age of file versions to trim. In other words, all file versions that are older than this number of days will be deleted.

```yaml
Type: int
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
When provided, no confirmation prompts will be shown to the user.

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


