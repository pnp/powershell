---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteArchiveState.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPSiteArchiveState
---

# Set-PnPSiteArchiveState

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the archived state of the site. Can be used to archive and reactivate sites.

## SYNTAX

```powershell
Set-PnPSiteArchiveState -Identity <SPOSitePipeBind> -ArchiveState <SPOArchiveState> [-NoWait] [-Force]
```

## DESCRIPTION

Use this cmdlet to change the archive status of the site. You must be a SharePoint Online administrator or Global administrator and be a site collection administrator to run the cmdlet.
Microsoft 365 Archive needs to be enabled for the organization to be able to use this feature.

## EXAMPLES

### Example 1

```powershell
Set-PnPSiteArchiveState https://contoso.sharepoint.com/sites/Marketing -ArchiveState Archived
```

This example marks the site as Archived. For seven days after the operation, the site will remain in a "RecentlyArchived" state, where any reactivations will be free and instantaneous. If a site is reactivated after seven days, any reactivations will be charged and will take time.

### Example 2

```powershell
Set-PnPSiteArchiveState https://contoso.sharepoint.com/sites/Marketing -ArchiveState Active
```

This example triggers the reactivation of a site. If the site is reactivated from the "RecentlyArchived" state, it will become available instantaneously. If the site is reactivated from the "FullyArchived" state, it may take time for it to be reactivated.

## PARAMETERS

### -Identity
Specifies the full URL of the SharePoint Online site collection that needs to be renamed.

```yaml
Type: SPOSitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -ArchiveState

Sets the archived state of the site. Valid values are Archived, Active.

```yaml
Type: SPOArchiveState
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWait
If specified the task will return immediately after creating the archive state site job.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
If provided, no confirmation will be asked for changing the archive state.

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
