---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteFileVersionBatchDeleteJob.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPSiteFileVersionBatchDeleteJob
---
  
# Remove-PnPSiteFileVersionBatchDeleteJob

## SYNOPSIS

Cancels further processing of a file version batch trim job for a site collection.


## SYNTAX

```powershell
Remove-PnPSiteFileVersionBatchDeleteJob -DeleteBeforeDays <int> [-Force]
```


## DESCRIPTION

Cancels further processing of a file version batch trim job for a site collection.


## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteFileVersionBatchDeleteJob
```

Cancels further processing of the file version batch trim job for the site collection.

### EXAMPLE 2
```powershell
Remove-PnPSiteFileVersionBatchDeleteJob -Force
```

Cancels further processing of the file version batch trim job for the site collection, without prompting the user for confirmation.


## PARAMETERS

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


