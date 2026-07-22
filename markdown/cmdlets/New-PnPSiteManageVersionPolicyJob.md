---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSiteManageVersionPolicyJob.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPSiteManageVersionPolicyJob
---

# New-PnPSiteManageVersionPolicyJob

## SYNOPSIS

Starts a site-level version policy management job for a SharePoint Online site collection.

## SYNTAX

### TrimUseListPolicy

```powershell
New-PnPSiteManageVersionPolicyJob -Identity <SitePipeBind> -TrimUseListPolicy [-SyncListPolicy] [-FileTypes <String[]>] [-ExcludeDefaultPolicy] [-NoWait] [-Force] [-Connection <PnPConnection>]
```

### SyncListPolicy

```powershell
New-PnPSiteManageVersionPolicyJob -Identity <SitePipeBind> -SyncListPolicy [-FileTypes <String[]>] [-ExcludeDefaultPolicy] [-NoWait] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Starts a site-level job that can trim versions using existing list version policies, synchronize list version policies across libraries, or do both in a single operation. By default, the cmdlet waits for the tenant operation to complete. Use `-NoWait` to return immediately after the job has been queued.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSiteManageVersionPolicyJob -Identity "https://contoso.sharepoint.com/sites/project-x" -SyncListPolicy
```

Queues a site-level job that synchronizes list version policies across the target site collection.

### EXAMPLE 2
```powershell
New-PnPSiteManageVersionPolicyJob -Identity "https://contoso.sharepoint.com/sites/project-x" -TrimUseListPolicy -SyncListPolicy -Force
```

Queues a site-level job that trims versions using each library's list version policy and then synchronizes list version policies across the site collection, without prompting for confirmation.

### EXAMPLE 3
```powershell
New-PnPSiteManageVersionPolicyJob -Identity "https://contoso.sharepoint.com/sites/project-x" -TrimUseListPolicy -FileTypes "pdf","docx" -ExcludeDefaultPolicy -NoWait
```

Queues a site-level job that trims versions only for the specified file types, excludes the default policy, and returns immediately without waiting for the tenant operation to complete.

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

### -ExcludeDefaultPolicy
Excludes the default version policy from the site management job. This can be combined with `-FileTypes` to target only specific file types.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileTypes
The file types to include in the version policy management job. When omitted, all file types are included.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
When provided together with `-TrimUseListPolicy`, no confirmation prompt will be shown before creating the job.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The site collection on which to run the version policy management job.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -NoWait
Queues the version policy management job and returns immediately instead of waiting for the SharePoint Online tenant operation to complete.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SyncListPolicy
Synchronizes the list version policies across document libraries in the site collection.

```yaml
Type: SwitchParameter
Parameter Sets: TrimUseListPolicy, SyncListPolicy

Required: True (SyncListPolicy), False (TrimUseListPolicy)
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrimUseListPolicy
Trims file versions using the existing version policy configured on each document library in the site collection.

```yaml
Type: SwitchParameter
Parameter Sets: TrimUseListPolicy

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Get-PnPSiteFileVersionBatchDeleteJobStatus](Get-PnPSiteFileVersionBatchDeleteJobStatus.md)