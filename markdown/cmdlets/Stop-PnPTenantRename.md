---
title: Stop-PnPTenantRename
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Stop-PnPTenantRename.html
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
  
# Stop-PnPTenantRename

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site
* SharePoint permissions: `Sites.FullControl.All` (application) or `AllSites.FullControl` (delegated)

Stops the scheduled SharePoint Online tenant rename job.

## SYNTAX

```powershell
Stop-PnPTenantRename [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
Cancels the tenant rename job scheduled with `Start-PnPTenantRename`. A tenant has at most one rename job, so the cmdlet takes no identity.

Like `Stop-SPOTenantRename`, it does not prompt for confirmation by default. Use `-Confirm` to be asked first, or `-WhatIf` to see what it would do.

`Stop-SPOTenantRename` requires `-Reason` and accepts `-Comment`, but sends neither to SharePoint Online. This cmdlet does not have them, so remove them when converting a script that uses `Stop-SPOTenantRename`.

To rename the tenant after stopping a job, schedule a new one with `Start-PnPTenantRename`. The new start time must again be at least 24 hours in the future.

When the request succeeds, the cmdlet writes a message with the time, in UTC, at which the stop request completed. Run `Get-PnPTenantRenameStatus` afterwards to check the state of the job.

## EXAMPLES

### EXAMPLE 1
```powershell
Stop-PnPTenantRename
```

Stops the scheduled tenant rename job.

### EXAMPLE 2
```powershell
Stop-PnPTenantRename -WhatIf
```

Shows which tenant the rename would be stopped for, without stopping it.

### EXAMPLE 3
```powershell
Stop-PnPTenantRename
Get-PnPTenantRenameStatus
```

Stops the scheduled tenant rename job and then returns its status.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.String
Returns a message with the time, in UTC, at which the stop request completed.

## RELATED LINKS

[Start-PnPTenantRename](Start-PnPTenantRename.md)

[Get-PnPTenantRenameStatus](Get-PnPTenantRenameStatus.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

