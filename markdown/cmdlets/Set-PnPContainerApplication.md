---
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContainerApplication.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPContainerApplication
---
 
# Set-PnPContainerApplication

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Sets the configuration of a SharePoint Embedded application in the tenant.

## SYNTAX

```powershell
Set-PnPContainerApplication [-OwningApplicationId] <Guid> [-OverrideTenantSharingCapability <Boolean>] [-SharingCapability <SharingCapabilities>] [-CopilotEmbeddedChatHosts <String[]>] [-ItemMajorVersionLimit <Int32>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Changes only the settings for the parameters that are passed in, on the SharePoint Embedded application given with `-OwningApplicationId`. At least one setting has to be passed.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPContainerApplication -OwningApplicationId 2ce03211-353e-45d7-b487-8ac6981332cf -OverrideTenantSharingCapability $false
```

Makes the sharing of the application follow the sharing capability of the tenant.

### EXAMPLE 2
```powershell
Set-PnPContainerApplication -OwningApplicationId 2ce03211-353e-45d7-b487-8ac6981332cf -OverrideTenantSharingCapability $true -SharingCapability Disabled
```

Limits sharing files in the application to internal users, regardless of the sharing capability of the tenant.

### EXAMPLE 3
```powershell
Set-PnPContainerApplication -OwningApplicationId 2ce03211-353e-45d7-b487-8ac6981332cf -ItemMajorVersionLimit 1000
```

Keeps at most 1000 major versions of each file in the Containers of the application.

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

The hosts allowed to embed Copilot chat over the content of the application.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemMajorVersionLimit

The maximum number of major versions kept of each file in the Containers of the application, from 1 to 50000.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OverrideTenantSharingCapability

When `$true`, the application uses the sharing capability given with `-SharingCapability` instead of the one of the tenant, and `-SharingCapability` is required. When `$false`, the application follows the sharing capability of the tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OwningApplicationId

The id of the SharePoint Embedded application.

To configure the Microsoft Loop app, use OwningApplicationId: a187e399-0c36-4b98-8f04-1edc167a0996.
To configure the Microsoft Designer app, use OwningApplicationId: 5e2795e3-ce8c-4cfb-b302-35fe5cd01597

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingCapability

The sharing available in the application. Can only be used with `-OverrideTenantSharingCapability $true`.
- Disabled: external sharing is disabled.
- ExternalUserSharingOnly: sharing by email is enabled, guest link sharing is disabled.
- ExternalUserAndGuestSharing: sharing by email and guest link sharing are both enabled.
- ExistingExternalUserSharingOnly: only sharing with guests already in the directory of your organization.

Inviting people from outside your organization requires Microsoft Entra B2B integration, which can be enabled with `Set-PnPTenant -EnableAzureADB2BIntegration $true`.

```yaml
Type: SharingCapabilities
Parameter Sets: (All)
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Manage containers with PowerShell](https://learn.microsoft.com/sharepoint/dev/embedded/admin/manage-containers-powershell)

