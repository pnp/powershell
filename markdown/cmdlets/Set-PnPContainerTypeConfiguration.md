---
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContainerTypeConfiguration.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPContainerTypeConfiguration
schema: 2.0.0
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
---
 
# Set-PnPContainerTypeConfiguration

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Sets or updates the configuration of a Container Type in SharePoint Embedded.

## SYNTAX

```powershell
Set-PnPContainerTypeConfiguration [-Identity] <Guid> [-DiscoverabilityDisabled <Boolean>] [-SharingRestricted <Boolean>] [-ApplicationRedirectUrl <String>] [-OverrideTenantWhoCanShareAnonymousAllowList <Boolean>] [-WhoCanShareAnonymousAllowList <Guid[]>] [-OverrideTenantWhoCanShareAuthenticatedGuestAllowList <Boolean>] [-WhoCanShareAuthenticatedGuestAllowList <Guid[]>] [-CopilotEmbeddedChatHosts <String[]>] [-AnonymousLinkExpirationInDays <Int32>] [-IsArchiveEnabled <Boolean>] [-UseLegacyItemWebUrl <Boolean>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Changes only the settings for the parameters that are passed in, on a Container Type created under a SharePoint Embedded application, and returns the resulting configuration. At least one setting has to be passed.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPContainerTypeConfiguration -Identity 4f0af585-8dcc-0000-223d-661eb2c604e4 -DiscoverabilityDisabled $false
```

Shows the content of Containers of this Container Type across Microsoft 365, such as on office.com, onedrive.com and in recommended files.

### EXAMPLE 2
```powershell
Set-PnPContainerTypeConfiguration -Identity 4f0af585-8dcc-0000-223d-661eb2c604e4 -SharingRestricted $false
```

Lets any member or guest of a Container with edit permission share its files.

### EXAMPLE 3
```powershell
Set-PnPContainerTypeConfiguration -Identity 4f0af585-8dcc-0000-223d-661eb2c604e4 -OverrideTenantWhoCanShareAnonymousAllowList $true -WhoCanShareAnonymousAllowList 3f0b8a1e-6c2d-4e5f-9a7b-1c2d3e4f5a6b
```

Only lets members of the specified security group share with anonymous users, regardless of the tenant level setting.

### EXAMPLE 4
```powershell
Set-PnPContainerTypeConfiguration -Identity 4f0af585-8dcc-0000-223d-661eb2c604e4 -OverrideTenantWhoCanShareAnonymousAllowList $true -WhoCanShareAnonymousAllowList $null -OverrideTenantWhoCanShareAuthenticatedGuestAllowList $true -WhoCanShareAuthenticatedGuestAllowList $null
```

Overrides both tenant level allow lists with an empty one, so sharing with external users is no longer limited to members of specific security groups.

### EXAMPLE 5
```powershell
Set-PnPContainerTypeConfiguration -Identity 4f0af585-8dcc-0000-223d-661eb2c604e4 -IsArchiveEnabled $true
```

Enables archiving and reactivating the Containers of this Container Type.

## PARAMETERS

### -AnonymousLinkExpirationInDays

The number of days after which anonymous links created from then on expire, from 1 to 730. Use -1 to follow the tenant level setting.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplicationRedirectUrl

The url of the application that files in Containers of this Container Type redirect to.

```yaml
Type: String
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
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -CopilotEmbeddedChatHosts

The hosts allowed to embed Copilot chat over the content of Containers of this Container Type.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DiscoverabilityDisabled

When `$true`, the content of Containers of this Container Type is hidden across Microsoft 365, such as on office.com, onedrive.com, in recommended files and in other intelligent file discovery features.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The id of the Container Type.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases: ContainerTypeId

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsArchiveEnabled

Enables archiving the Containers of this Container Type. Archiving moves their content to the cold tier, where it cannot be accessed until the Container is reactivated. Reactivation is immediate within the first seven days and can take up to 24 hours after that. A change can take up to 24 hours to reach the consuming tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OverrideTenantWhoCanShareAnonymousAllowList

Whether `-WhoCanShareAnonymousAllowList` replaces the tenant level list of security groups allowed to share with anonymous users. `-WhoCanShareAnonymousAllowList` can only be used with `-OverrideTenantWhoCanShareAnonymousAllowList $true`.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OverrideTenantWhoCanShareAuthenticatedGuestAllowList

Whether `-WhoCanShareAuthenticatedGuestAllowList` replaces the tenant level list of security groups allowed to share with authenticated guests. `-WhoCanShareAuthenticatedGuestAllowList` can only be used with `-OverrideTenantWhoCanShareAuthenticatedGuestAllowList $true`.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingRestricted

When `$true`, only owners and managers of a Container can share its files. When `$false`, any member or guest with edit permission can.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseLegacyItemWebUrl

Temporarily keeps using the legacy format of the item WebUrl, instead of the SharePoint Embedded one.

```yaml
Type: Boolean
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

### -WhoCanShareAnonymousAllowList

The object ids of up to 12 security groups whose members can share with anonymous users as well as with authenticated guests. Microsoft 365 Groups are not supported. Pass `$null` to no longer restrict sharing to specific security groups. Requires `-OverrideTenantWhoCanShareAnonymousAllowList $true`.

```yaml
Type: Guid[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhoCanShareAuthenticatedGuestAllowList

The object ids of up to 12 security groups whose members can share with authenticated guests. Microsoft 365 Groups are not supported. Pass `$null` to no longer restrict sharing to specific security groups. Requires `-OverrideTenantWhoCanShareAuthenticatedGuestAllowList $true`.

```yaml
Type: Guid[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[SharePoint Embedded Container Types](https://learn.microsoft.com/sharepoint/dev/embedded/concepts/app-concepts/containertypes)

