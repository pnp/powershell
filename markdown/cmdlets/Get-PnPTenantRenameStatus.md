---
title: Get-PnPTenantRenameStatus
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantRenameStatus.html
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
  
# Get-PnPTenantRenameStatus

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site
* SharePoint permissions: `Sites.FullControl.All` (application) or `AllSites.FullControl` (delegated)

Returns the status of the SharePoint Online tenant rename job.

## SYNTAX

```powershell
Get-PnPTenantRenameStatus [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns the status of the tenant rename job scheduled with `Start-PnPTenantRename`: its state, who requested it and when, the scheduled start time, the new domain name and how many sites it covers.

The site rename progress (`InProgress`, `Renamed` and `AttentionRequired`) is retrieved per site rename job, the same way `Get-SPOTenantRenameStatus` does. On a large tenant this can take a few minutes. If the job is returned but its site progress cannot be retrieved, the job is still written to the pipeline with those three properties empty, followed by a non-terminating error.

`RequestedAt` and `ScheduledAt` are returned as UTC date and time values. Use `ToLocalTime()` to show them in the local time zone, as in example 3.

When no tenant rename job exists, the cmdlet writes a non-terminating error.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantRenameStatus
```

Returns the status of the tenant rename job, including the progress of its site renames.

### EXAMPLE 2
```powershell
(Get-PnPTenantRenameStatus).Status
```

Returns only the state of the tenant rename job, for example `Queued` before it starts and `Success` once it has completed.

### EXAMPLE 3
```powershell
Get-PnPTenantRenameStatus | Select-Object NewDomainName, @{ Name = "ScheduledAtLocal"; Expression = { $_.ScheduledAt.ToLocalTime() } }
```

Returns the new domain name and the scheduled start time converted to the local time zone.

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

## OUTPUTS

### PnP.PowerShell.Commands.Model.TenantRenameStatus
Returns an object with `RenameJobId`, `Status`, `RequestedAt`, `RequestedBy`, `ScheduledAt`, `NewDomainName`, `TotalSites`, `InProgress`, `Renamed` and `AttentionRequired` properties. `AttentionRequired` is the number of site renames that failed or are suspended.

## RELATED LINKS

[Start-PnPTenantRename](Start-PnPTenantRename.md)

[Stop-PnPTenantRename](Stop-PnPTenantRename.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

