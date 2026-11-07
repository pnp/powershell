---
Module Name: PnP.PowerShell
title: Start-PnPTenantRename
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Start-PnPTenantRename.html
---
 
# Start-PnPTenantRename

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site
* SharePoint permissions: `Sites.FullControl.All` (application) or `AllSites.FullControl` (delegated)

Schedules a rename of the SharePoint Online tenant domain name.

## SYNTAX

```powershell
Start-PnPTenantRename -DomainName <String> -ScheduledDateTime <DateTime> [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
Schedules a tenant rename job to change the SharePoint Online domain name for the organization. For example, this can change `contoso.sharepoint.com` to `fabrikam.sharepoint.com`.

The new domain name must already have been added successfully to Microsoft Entra ID. Specify only the domain prefix, without `.sharepoint.com` or `.onmicrosoft.com`.

The scheduled date and time must be at least 24 hours in the future and no more than 30 days in the future. Tenant rename can take several hours to days depending on the number of SharePoint sites and OneDrive accounts in the tenant.

Scheduling can take a few minutes. If SharePoint Online does not respond within 5 minutes, the cmdlet stops with an error, but the rename may still have been scheduled. Run `Get-PnPTenantRenameStatus` to check before running this cmdlet again. Use `Stop-PnPTenantRename` to cancel a scheduled rename.

## EXAMPLES

### EXAMPLE 1
```powershell
Start-PnPTenantRename -DomainName "fabrikam" -ScheduledDateTime "2026-05-30T22:00:00"
```

Schedules the SharePoint Online tenant domain rename to `fabrikam.sharepoint.com` for May 30, 2026 at 22:00.

### EXAMPLE 2
```powershell
Start-PnPTenantRename -DomainName "fabrikam" -ScheduledDateTime (Get-Date).AddDays(7) -WhatIf
```

Shows what would happen if the tenant rename were scheduled one week from now.

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

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainName
Specifies the new SharePoint Online domain prefix. Do not include `.sharepoint.com` or `.onmicrosoft.com`.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScheduledDateTime
Specifies the date and time at which the tenant rename job should start. SharePoint Online requires this to be at least 24 hours in the future and no more than 30 days in the future.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: True
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.String
Returns tenant rename warning and scheduling messages from SharePoint Online.

## RELATED LINKS

[Get-PnPTenantRenameStatus](Get-PnPTenantRenameStatus.md)

[Stop-PnPTenantRename](Stop-PnPTenantRename.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
